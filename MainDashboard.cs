using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Daily_Tracker_ver2.Helpers;

namespace Daily_Tracker_ver2
{
    public partial class MainDashboard : Form
    {
        // Loaded dynamically from the DB; rebuilt whenever habits change.
        private List<DatabaseHelper.HabitDef> _habitDefs = new List<DatabaseHelper.HabitDef>();

        // Fallback emoji map for habits that existed before the new habits table
        private static readonly Dictionary<string, string> FallbackEmoji =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Morning Meditation", "🧘" },
                { "Drink 2L Water",     "💧" },
                { "Read Book",          "📖" },
                { "Coding",             "💻" },
                { "Evening Walk",       "🚶" },
                { "Offer Prayer",       "🕌" },
            };

        public MainDashboard()
        {
            InitializeComponent();
            this.Load += MainDashboard_Load;
            this.Resize += MainDashboard_Resize;
        }

        // ── Load ─────────────────────────────────────────────────────────────
        private void MainDashboard_Load(object sender, EventArgs e)
        {
            DatabaseHelper.EnsureHabitsTable();
            ReloadHabits();
        }

        // ── Called by Habit form after add/delete ─────────────────────────────
        public void ReloadHabits()
        {
            _habitDefs = DatabaseHelper.GetHabits(SessionManager.UserId);

            // If the user has no habits in the new table yet, load nothing
            // (cards are hidden). If they have habits, show up to 6.
            RebuildHabitCards();
            ResizeCards();
        }

        // ── Rebuild the visible habit cards from _habitDefs ───────────────────
        private void RebuildHabitCards()
        {
            var cards = AllCards();
            DateTime today = DateTime.Today;
            int uid = SessionManager.UserId;

            for (int i = 0; i < cards.Length; i++)
            {
                var card = cards[i];

                if (i < _habitDefs.Count)
                {
                    var h = _habitDefs[i];
                    card.Visible = true;

                    card.HabitTitle = h.Name;
                    card.HabitDuration = h.Duration;

                    Color accent = HexToColor(h.Colour);
                    card.HabitColor = accent;

                    string emoji = string.IsNullOrWhiteSpace(h.Emoji)
                        ? (FallbackEmoji.TryGetValue(h.Name, out var fe) ? fe : "⭐")
                        : h.Emoji;
                    card.HabitIcon = DrawEmojiIcon(emoji, accent);

                    bool done = DatabaseHelper.GetHabitCompleted(uid, h.Name, today);
                    card.CompletionChanged -= Card_CompletionChanged;
                    card.IsCompleted = done;
                    card.CompletionChanged += Card_CompletionChanged;

                    int streak = DatabaseHelper.GetCurrentStreak(uid, h.Name);
                    card.CurrentStreak = streak + " DAY" + (streak == 1 ? "" : "S");
                }
                else
                {
                    // Hide unused card slots
                    card.Visible = false;
                    card.CompletionChanged -= Card_CompletionChanged;
                }
            }
        }

        // ── Resize: stretch cards to fill the right panel ─────────────────────
        private void MainDashboard_Resize(object sender, EventArgs e) => ResizeCards();

        private void ResizeCards()
        {
            int panelW = splitContainer1.Panel2.ClientSize.Width;
            int cardW = Math.Max(300, panelW - 20);

            flowLayoutPanel2.Width = panelW - 4;

            foreach (var card in AllCards())
            {
                card.Width = cardW;
                if (card.Controls.Count > 0 && card.Controls[0] is Panel inner)
                    inner.Width = cardW - 6;
            }
        }

        private HabitCard[] AllCards()
            => new[] { habitCard1, habitCard2, habitCard3,
                       habitCard4, habitCard5, habitCard6 };

        // ── Checkbox changed → save to DB + update streak ────────────────────
        private void Card_CompletionChanged(object sender, EventArgs e)
        {
            if (!(sender is HabitCard card)) return;

            string habitName = card.HabitTitle;
            bool completed = card.IsCompleted;
            int uid = SessionManager.UserId;

            try
            {
                DatabaseHelper.SetHabitCompleted(uid, habitName, DateTime.Today, completed);
                int streak = DatabaseHelper.GetCurrentStreak(uid, habitName);
                card.CurrentStreak = streak + " DAY" + (streak == 1 ? "" : "S");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save habit: " + ex.Message,
                    "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ── Dark / Light toggle ───────────────────────────────────────────────
        private void button4_Click(object sender, EventArgs e)
        {
            ThemeManager.ToggleTheme();
            foreach (var card in AllCards())
                ThemeManager.ApplyToHabitCard(card);

            button4.Text = ThemeManager.IsDark ? "☀ Light Mode" : "🌙 Dark Mode";
            button4.BackColor = ThemeManager.IsDark
                ? Color.FromArgb(80, 80, 115)
                : Color.FromArgb(64, 64, 90);
            this.Refresh();
        }

        // ── Navigation ────────────────────────────────────────────────────────
        private void button1_Click(object sender, EventArgs e)   // Profile
        {
            var p = new profile();
            ThemeManager.ApplyTheme(p);
            p.Show();
        }

        private void button2_Click(object sender, EventArgs e)   // Habits
        {
            var h = new Habit();
            ThemeManager.ApplyTheme(h);
            h.Show();
        }

        private void button3_Click(object sender, EventArgs e)   // Monthly Review
        {
            var review = new MonthlyReview();
            ThemeManager.ApplyTheme(review);
            review.Show();
        }

        // ── Helpers ───────────────────────────────────────────────────────────
        private static Image DrawEmojiIcon(string emoji, Color bg)
        {
            const int size = 64;
            var bmp = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                g.Clear(Color.Transparent);
                var rc = new Rectangle(2, 2, size - 4, size - 4);
                using (var path = RoundedRect(rc, 10))
                using (var fill = new SolidBrush(bg))
                    g.FillPath(fill, path);
                using (var f = new Font("Segoe UI Emoji", 26f, FontStyle.Regular, GraphicsUnit.Pixel))
                using (var sf = new StringFormat
                { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    g.DrawString(emoji, f, Brushes.White, new RectangleF(0, 0, size, size), sf);
            }
            return bmp;
        }

        private static GraphicsPath RoundedRect(Rectangle b, int r)
        {
            int d = r * 2;
            var p = new GraphicsPath();
            p.AddArc(b.X, b.Y, d, d, 180, 90);
            p.AddArc(b.Right - d, b.Y, d, d, 270, 90);
            p.AddArc(b.Right - d, b.Bottom - d, d, d, 0, 90);
            p.AddArc(b.X, b.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        private static Color HexToColor(string hex)
        {
            try { return ColorTranslator.FromHtml(hex); }
            catch { return Color.CornflowerBlue; }
        }

        // ── Designer stubs ────────────────────────────────────────────────────
        private void splitContainer1_Panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e) { }
        private void pictureBox3_Click(object sender, EventArgs e) { }
        private void habitCard1_Load(object sender, EventArgs e) { }
        private void Button4_Click_Legacy(object sender, EventArgs e) { }
    }
}