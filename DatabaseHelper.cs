using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Daily_Tracker_ver2.Helpers
{
    public static class DatabaseHelper
    {
        // ── Connection ────────────────────────────────────────────────────────
        public static MySqlConnection GetConnection()
        {
            var cs = ConfigurationManager.ConnectionStrings["DailyTrackerMySql"]?.ConnectionString;
            if (string.IsNullOrEmpty(cs))
                throw new InvalidOperationException(
                    "Connection string 'DailyTrackerMySql' not found in App.config.");
            return new MySqlConnection(cs);
        }

        // ── Ensure the habits table exists (call once at startup) ─────────────
        public static void EnsureHabitsTable()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    const string sql = @"
CREATE TABLE IF NOT EXISTS habits (
    id           INT          NOT NULL AUTO_INCREMENT PRIMARY KEY,
    user_id      INT          NOT NULL,
    name         VARCHAR(120) NOT NULL,
    duration     VARCHAR(60)  NOT NULL DEFAULT '',
    emoji        VARCHAR(10)  NOT NULL DEFAULT '⭐',
    colour       VARCHAR(20)  NOT NULL DEFAULT '#6495ED',
    created_at   DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UNIQUE KEY uq_user_habit (user_id, name)
)";
                    using (var cmd = new MySqlCommand(sql, conn))
                        cmd.ExecuteNonQuery();
                }
            }
            catch { /* non-fatal – app still works with legacy data */ }

            // Also ensure the completions table exists
            EnsureHabitCompletionsTable();
        }

        // ── Ensure the habit_completions table exists ─────────────────────────
        public static void EnsureHabitCompletionsTable()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    const string sql = @"
CREATE TABLE IF NOT EXISTS habit_completions (
    id              INT          NOT NULL AUTO_INCREMENT PRIMARY KEY,
    user_id         INT          NOT NULL,
    habit_name      VARCHAR(120) NOT NULL,
    completion_date DATE         NOT NULL,
    completed       TINYINT(1)   NOT NULL DEFAULT 0,
    UNIQUE KEY uq_completion (user_id, habit_name, completion_date)
)";
                    using (var cmd = new MySqlCommand(sql, conn))
                        cmd.ExecuteNonQuery();
                }
            }
            catch { /* non-fatal */ }
        }

        // ── Ensure profile_picture column exists in users table ───────────────
        public static void EnsureProfilePictureColumn()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    // Check if column already exists
                    const string checkSql = @"
SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_SCHEMA=DATABASE() AND TABLE_NAME='users' AND COLUMN_NAME='profile_picture'";
                    using (var cmd = new MySqlCommand(checkSql, conn))
                    {
                        var result = cmd.ExecuteScalar();
                        int count = result != null ? Convert.ToInt32(result) : 0;

                        // If column doesn't exist, add it
                        if (count == 0)
                        {
                            const string alterSql = "ALTER TABLE users ADD COLUMN profile_picture MEDIUMBLOB NULL";
                            using (var alterCmd = new MySqlCommand(alterSql, conn))
                                alterCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch { /* non-fatal – app still works without profile pictures */ }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  HABIT CRUD
        // ═══════════════════════════════════════════════════════════════════════

        public class HabitDef
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Duration { get; set; } = string.Empty;
            public string Emoji { get; set; } = "⭐";
            public string Colour { get; set; } = "#6495ED";
        }

        /// <summary>Load all habits for a user, ordered by creation date.</summary>
        public static List<HabitDef> GetHabits(int userId)
        {
            var list = new List<HabitDef>();
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    const string sql =
                        "SELECT id, name, duration, emoji, colour " +
                        "FROM habits WHERE user_id=@uid ORDER BY created_at ASC";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@uid", userId);
                        using (var r = cmd.ExecuteReader())
                            while (r.Read())
                                list.Add(new HabitDef
                                {
                                    Id = r.GetInt32(0),
                                    Name = r.IsDBNull(1) ? "" : r.GetString(1),
                                    Duration = r.IsDBNull(2) ? "" : r.GetString(2),
                                    Emoji = r.IsDBNull(3) ? "⭐" : r.GetString(3),
                                    Colour = r.IsDBNull(4) ? "#6495ED" : r.GetString(4),
                                });
                    }
                }
            }
            catch { /* return what we have */ }
            return list;
        }

        /// <summary>
        /// Add a new habit. Returns the new row id, or -1 on failure.
        /// Throws on duplicate name.
        /// </summary>
        public static int AddHabit(int userId, string name, string duration,
                                   string emoji, string colour)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                const string sql =
                    "INSERT INTO habits (user_id, name, duration, emoji, colour) " +
                    "VALUES (@uid, @name, @dur, @emoji, @colour)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@dur", duration);
                    cmd.Parameters.AddWithValue("@emoji", emoji);
                    cmd.Parameters.AddWithValue("@colour", colour);
                    cmd.ExecuteNonQuery();
                    return (int)cmd.LastInsertedId;
                }
            }
        }

        /// <summary>
        /// Delete a habit AND all its completion records for this user.
        /// </summary>
        public static void DeleteHabit(int userId, int habitId, string habitName)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        // Remove all completion history
                        const string delComp =
                            "DELETE FROM habit_completions " +
                            "WHERE user_id=@uid AND habit_name=@name";
                        using (var cmd = new MySqlCommand(delComp, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@uid", userId);
                            cmd.Parameters.AddWithValue("@name", habitName);
                            cmd.ExecuteNonQuery();
                        }

                        // Remove the habit definition
                        const string delHabit =
                            "DELETE FROM habits WHERE id=@id AND user_id=@uid";
                        using (var cmd = new MySqlCommand(delHabit, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", habitId);
                            cmd.Parameters.AddWithValue("@uid", userId);
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  PROFILE
        // ═══════════════════════════════════════════════════════════════════════

        public class UserProfile
        {
            public string FullName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Occupation { get; set; } = string.Empty;
            public string AboutSelf { get; set; } = string.Empty;
            public byte[] ProfilePicture { get; set; } = null;
        }

        public static UserProfile LoadProfile(int userId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "SELECT full_name, email, occupation, about_self, profile_picture " +
                        "FROM users WHERE id=@id LIMIT 1";
                    cmd.Parameters.AddWithValue("@id", userId);
                    using (var r = cmd.ExecuteReader())
                        if (r.Read())
                            return new UserProfile
                            {
                                FullName = r["full_name"] as string ?? "",
                                Email = r["email"] as string ?? "",
                                Occupation = r["occupation"] as string ?? "",
                                AboutSelf = r["about_self"] as string ?? "",
                                ProfilePicture = r["profile_picture"] as byte[],
                            };
                }
            }
            return new UserProfile();
        }



        public static void SaveProfile(int userId, UserProfile profile)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    // Only update picture if new bytes were provided
                    if (profile.ProfilePicture != null)
                    {
                        cmd.CommandText =
                            "UPDATE users SET full_name=@fn, email=@em, " +
                            "occupation=@oc, about_self=@ab, profile_picture=@pic WHERE id=@id";
                        cmd.Parameters.AddWithValue("@pic", profile.ProfilePicture);
                    }
                    else
                    {
                        cmd.CommandText =
                            "UPDATE users SET full_name=@fn, email=@em, " +
                            "occupation=@oc, about_self=@ab WHERE id=@id";
                    }

                    cmd.Parameters.AddWithValue("@fn", (object)profile.FullName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@em", (object)profile.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@oc", (object)profile.Occupation ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ab", (object)profile.AboutSelf ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //public static void SaveProfile(int userId, UserProfile profile)
        //{
        //    using (var conn = GetConnection())
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText =
        //                "UPDATE users SET full_name=@fn, email=@em, " +
        //                "occupation=@oc, about_self=@ab, profile_picture=@pic WHERE id=@id";
        //            cmd.Parameters.AddWithValue("@fn", (object)profile.FullName ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("@em", (object)profile.Email ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("@oc", (object)profile.Occupation ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("@ab", (object)profile.AboutSelf ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("@pic", (object)profile.ProfilePicture ?? DBNull.Value);
        //            cmd.Parameters.AddWithValue("@id", userId);
        //            if (cmd.ExecuteNonQuery() == 0)
        //            {
        //                cmd.CommandText =
        //                    "INSERT INTO users (id,full_name,email,occupation,about_self,profile_picture) " +
        //                    "VALUES (@id,@fn,@em,@oc,@ab,@pic)";
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}

        // ═══════════════════════════════════════════════════════════════════════
        //  HABIT COMPLETIONS
        // ═══════════════════════════════════════════════════════════════════════

        public static bool GetHabitCompleted(int userId, string habitName, DateTime date)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    const string sql =
                        "SELECT completed FROM habit_completions " +
                        "WHERE user_id=@uid AND habit_name=@name AND completion_date=@date LIMIT 1";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@uid", userId);
                        cmd.Parameters.AddWithValue("@name", habitName);
                        cmd.Parameters.AddWithValue("@date", date.Date);
                        var res = cmd.ExecuteScalar();
                        return res != null && res != DBNull.Value && Convert.ToInt32(res) != 0;
                    }
                }
            }
            catch { return false; }
        }

        public static void SetHabitCompleted(int userId, string habitName,
                                             DateTime date, bool completed)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    const string upd =
                        "UPDATE habit_completions SET completed=@c " +
                        "WHERE user_id=@uid AND habit_name=@name AND completion_date=@date";
                    using (var cmd = new MySqlCommand(upd, conn))
                    {
                        cmd.Parameters.AddWithValue("@c", completed ? 1 : 0);
                        cmd.Parameters.AddWithValue("@uid", userId);
                        cmd.Parameters.AddWithValue("@name", habitName);
                        cmd.Parameters.AddWithValue("@date", date.Date);
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            cmd.CommandText =
                                "INSERT INTO habit_completions " +
                                "(user_id,habit_name,completion_date,completed) " +
                                "VALUES (@uid,@name,@date,@c)";
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch { /* keep UI stable */ }
        }

        public static int GetCurrentStreak(int userId, string habitName)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    const string sql =
                        "SELECT completion_date FROM habit_completions " +
                        "WHERE user_id=@uid AND habit_name=@name AND completed=1 " +
                        "ORDER BY completion_date DESC";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@uid", userId);
                        cmd.Parameters.AddWithValue("@name", habitName);
                        using (var r = cmd.ExecuteReader())
                        {
                            int streak = 0;
                            DateTime expected = DateTime.Today;
                            while (r.Read())
                            {
                                DateTime d = r.GetDateTime(0).Date;
                                if (d == expected) { streak++; expected = expected.AddDays(-1); }
                                else if (d < expected) break;
                            }
                            return streak;
                        }
                    }
                }
            }
            catch { return 0; }
        }

        public static (int len, string habitName) GetLongestMonthlyStreak(
            int userId, int year, int month)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    const string sql = @"
SELECT habit_name, completion_date
FROM habit_completions
WHERE user_id=@uid AND completed=1
  AND YEAR(completion_date)=@year AND MONTH(completion_date)=@month
ORDER BY habit_name, completion_date ASC";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@uid", userId);
                        cmd.Parameters.AddWithValue("@year", year);
                        cmd.Parameters.AddWithValue("@month", month);
                        using (var r = cmd.ExecuteReader())
                        {
                            int bestLen = 0; string bestName = "";
                            string curHabit = null; DateTime? prev = null; int curLen = 0;
                            while (r.Read())
                            {
                                string nm = r.IsDBNull(0) ? "" : r.GetString(0);
                                DateTime d = r.GetDateTime(1).Date;
                                if (curHabit == null || nm != curHabit)
                                {
                                    if (curLen > bestLen) { bestLen = curLen; bestName = curHabit ?? ""; }
                                    curHabit = nm; prev = d; curLen = 1;
                                }
                                else if (prev.HasValue && d == prev.Value.AddDays(1)) { curLen++; prev = d; }
                                else if (prev.HasValue && d == prev.Value) { /* dup */ }
                                else { if (curLen > bestLen) { bestLen = curLen; bestName = curHabit ?? ""; } curLen = 1; prev = d; }
                            }
                            if (curLen > bestLen) { bestLen = curLen; bestName = curHabit ?? ""; }
                            return (bestLen, bestName);
                        }
                    }
                }
            }
            catch { return (0, ""); }
        }

        public static Dictionary<int, int> GetMonthlyDailyCounts(
            int userId, int year, int month)
        {
            var result = new Dictionary<int, int>();
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    const string sql = @"
SELECT DAY(completion_date) AS day, COUNT(*) AS cnt
FROM habit_completions
WHERE user_id=@uid AND completed=1
  AND YEAR(completion_date)=@year AND MONTH(completion_date)=@month
GROUP BY DAY(completion_date)";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@uid", userId);
                        cmd.Parameters.AddWithValue("@year", year);
                        cmd.Parameters.AddWithValue("@month", month);
                        using (var r = cmd.ExecuteReader())
                            while (r.Read())
                            {
                                int day = r.IsDBNull(0) ? 0 : Convert.ToInt32(r.GetValue(0));
                                int cnt = r.IsDBNull(1) ? 0 : Convert.ToInt32(r.GetValue(1));
                                if (day >= 1) result[day] = cnt;
                            }
                    }
                }
            }
            catch { }
            return result;
        }

        public static List<(string name, int pct)> GetMonthlyHabitRates(
            int userId, int year, int month)
        {
            var list = new List<(string, int)>();
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    const string sql = @"
SELECT habit_name, COUNT(DISTINCT DATE(completion_date)) AS cnt
FROM habit_completions
WHERE user_id=@uid AND completed=1
  AND YEAR(completion_date)=@year AND MONTH(completion_date)=@month
GROUP BY habit_name ORDER BY cnt DESC";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@uid", userId);
                        cmd.Parameters.AddWithValue("@year", year);
                        cmd.Parameters.AddWithValue("@month", month);
                        using (var r = cmd.ExecuteReader())
                        {
                            int days = DateTime.DaysInMonth(year, month);
                            while (r.Read())
                            {
                                string nm = r.IsDBNull(0) ? "" : r.GetString(0);
                                int cnt = r.IsDBNull(1) ? 0 : Convert.ToInt32(r.GetValue(1));
                                list.Add((nm, days > 0 ? (int)Math.Round((double)cnt / days * 100) : 0));
                            }
                        }
                    }
                }
            }
            catch { }
            return list;
        }

        public static void EnsureUsersColumns()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();

                    // Add full_name if missing
                    RunIfColumnMissing(conn, "users", "full_name",
                        "ALTER TABLE users ADD COLUMN full_name VARCHAR(150) NULL");

                    // Populate full_name from first_name + last_name where blank
                    using (var cmd = new MySqlCommand(
                        "UPDATE users SET full_name = CONCAT(first_name,' ',last_name) " +
                        "WHERE full_name IS NULL OR full_name = ''", conn))
                        cmd.ExecuteNonQuery();

                    // Add occupation if missing
                    RunIfColumnMissing(conn, "users", "occupation",
                        "ALTER TABLE users ADD COLUMN occupation VARCHAR(120) NULL");

                    // Add about_self if missing
                    RunIfColumnMissing(conn, "users", "about_self",
                        "ALTER TABLE users ADD COLUMN about_self TEXT NULL");

                    // Add profile_picture if missing
                    RunIfColumnMissing(conn, "users", "profile_picture",
                        "ALTER TABLE users ADD COLUMN profile_picture MEDIUMBLOB NULL");
                }
            }
            catch { /* non-fatal */ }
        }

        private static void RunIfColumnMissing(MySqlConnection conn,
            string table, string column, string alterSql)
        {
            const string check = @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_SCHEMA=DATABASE() AND TABLE_NAME=@t AND COLUMN_NAME=@c";
            using (var cmd = new MySqlCommand(check, conn))
            {
                cmd.Parameters.AddWithValue("@t", table);
                cmd.Parameters.AddWithValue("@c", column);
                if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                    using (var alter = new MySqlCommand(alterSql, conn))
                        alter.ExecuteNonQuery();
            }
        }



        public static int GetMonthlyOverallPct(int userId, int year, int month, int totalHabits)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    int expected = DateTime.DaysInMonth(year, month) * totalHabits;
                    const string sql = @"
SELECT COUNT(DISTINCT CONCAT(DAY(completion_date),'_',habit_name)) AS total_completions
FROM habit_completions
WHERE user_id=@uid AND completed=1
  AND YEAR(completion_date)=@year AND MONTH(completion_date)=@month";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@uid", userId);
                        cmd.Parameters.AddWithValue("@year", year);
                        cmd.Parameters.AddWithValue("@month", month);
                        var res = cmd.ExecuteScalar();
                        int actual = res != null && res != DBNull.Value ? Convert.ToInt32(res) : 0;
                        return expected <= 0 ? 0 : (int)Math.Round((double)actual / expected * 100);
                    }
                }
            }
            catch { return 0; }
        }

        public static int GetNewHabitsThisMonth(int userId, int year, int month)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    const string sql = @"
SELECT COUNT(DISTINCT habit_name) FROM (
    SELECT habit_name, MIN(completion_date) AS first_date
    FROM habit_completions WHERE user_id=@uid AND completed=1
    GROUP BY habit_name
) sub WHERE YEAR(first_date)=@year AND MONTH(first_date)=@month";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@uid", userId);
                        cmd.Parameters.AddWithValue("@year", year);
                        cmd.Parameters.AddWithValue("@month", month);
                        var res = cmd.ExecuteScalar();
                        return res != null && res != DBNull.Value ? Convert.ToInt32(res) : 0;
                    }
                }
            }
            catch { return 0; }
        }
    }
}