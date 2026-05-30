using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using Daily_Tracker_ver2.Helpers;

namespace Daily_Tracker_ver2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += (s, e) => ThemeManager.ApplyTheme(this);
        }

        private void button2_Click(object sender, EventArgs e)   // Sign Up
        {
            Signupform signupform = new Signupform();
            ThemeManager.ApplyTheme(signupform);
            signupform.ShowDialog();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)   // Login
        {
            string email = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password.",
                    "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // Verify at least one account exists
                    using (var countCmd = new MySqlCommand("SELECT COUNT(*) FROM users", conn))
                    {
                        if (Convert.ToInt32(countCmd.ExecuteScalar()) == 0)
                        {
                            MessageBox.Show("No accounts found.\nPlease sign up first.",
                                "No Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    // Fetch user row
                    string loginQuery = @"SELECT id, first_name, last_name
                                          FROM users
                                          WHERE email    = @email
                                            AND password = @password
                                          LIMIT 1";

                    using (var cmd = new MySqlCommand(loginQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (var rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                // ── Set session ──────────────────────────────
                                SessionManager.UserId = rdr.GetInt32(0);
                                SessionManager.UserEmail = email;
                                SessionManager.FullName =
                                    rdr.GetString(1) + " " + rdr.GetString(2);

                                rdr.Close();

                                MainDashboard mainDashboard = new MainDashboard();
                                ThemeManager.ApplyTheme(mainDashboard);
                                mainDashboard.ShowDialog();
                                this.Hide();
                            }
                            else
                            {
                                rdr.Close();

                                // Check whether email exists to give a precise error
                                string emailCheck = "SELECT COUNT(*) FROM users WHERE email = @e";
                                using (var ec = new MySqlCommand(emailCheck, conn))
                                {
                                    ec.Parameters.AddWithValue("@e", email);
                                    int exists = Convert.ToInt32(ec.ExecuteScalar());
                                    if (exists == 0)
                                        MessageBox.Show(
                                            "No account found with this email.\nPlease sign up first.",
                                            "Account Not Found",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    else
                                        MessageBox.Show("Incorrect password. Please try again.",
                                            "Wrong Password",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}