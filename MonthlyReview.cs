using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Daily_Tracker_ver2.Helpers;

namespace Daily_Tracker_ver2
{
    public partial class MonthlyReview : Form
    {
        // ── Data fields (populated from DB) ──────────────────────────────────
        private int _progressPct = 0;
        private int _longestStreak = 0;
        private string _streakHabitName = "";
        private int _activeHabits = 0;
        private int[] _dailyData;
        private List<(string Name, int Pct, Color Clr)> _habits;

        private readonly int _year;
        private readonly int _month;
        private const int TotalHabits = 6;   // matches MainDashboard

        private static readonly Color[] HabitColours =
        {
            Color.FromArgb(41, 128, 185),
            Color.FromArgb(142, 68, 173),
            Color.FromArgb(39, 174, 96),
            Color.FromArgb(230, 126, 34),
            Color.FromArgb(231, 76, 60),
            Color.FromArgb(26, 188, 156),
        };

        public MonthlyReview()
        {
            InitializeComponent();
            _year = DateTime.Now.Year;
            _month = DateTime.Now.Month;
        }

        // ── Load ─────────────────────────────────────────────────────────────
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadFromDatabase();
            PopulateCards();
            DrawBarChart();
            PopulateHabitRows();
            ThemeManager.ApplyTheme(this);
        }

        private void LoadFromDatabase()
        {
            int uid = SessionManager.UserId;

            // ── Overall progress % ───────────────────────────────────────────
            _progressPct = DatabaseHelper.GetMonthlyOverallPct(
                uid, _year, _month, TotalHabits);

            // ── Longest streak ───────────────────────────────────────────────
            var (len, name) = DatabaseHelper.GetLongestMonthlyStreak(uid, _year, _month);
            _longestStreak = len;
            _streakHabitName = string.IsNullOrEmpty(name) ? "–" : name;

            // ── Active habits count ──────────────────────────────────────────
            _activeHabits = DatabaseHelper.GetNewHabitsThisMonth(uid, _year, _month);

            // ── Daily bar-chart data ─────────────────────────────────────────
            int daysInMonth = DateTime.DaysInMonth(_year, _month);
            _dailyData = new int[daysInMonth];
            var dailyCounts = DatabaseHelper.GetMonthlyDailyCounts(uid, _year, _month);
            foreach (var kv in dailyCounts)
                if (kv.Key >= 1 && kv.Key <= daysInMonth)
                    _dailyData[kv.Key - 1] = kv.Value;

            // ── Per-habit rates ──────────────────────────────────────────────
            _habits = new List<(string, int, Color)>();
            var rates = DatabaseHelper.GetMonthlyHabitRates(uid, _year, _month);
            int colourIdx = 0;
            foreach (var (hName, pct) in rates)
            {
                Color clr = HabitColours[colourIdx % HabitColours.Length];
                _habits.Add((hName, pct, clr));
                colourIdx++;
            }

            // Fall back: if no data yet, show the 6 habits at 0 %
            if (_habits.Count == 0)
            {
                string[] defaultNames = {
                    "Morning Meditation", "Drink 2L Water", "Read Book",
                    "Coding", "Evening Walk", "Offer Prayer"
                };
                for (int i = 0; i < defaultNames.Length; i++)
                    _habits.Add((defaultNames[i], 0, HabitColours[i]));
            }
        }

        // ── Populate summary cards ────────────────────────────────────────────
        private void PopulateCards()
        {
            lblMonthTitle.Text = DateTime.Now.ToString("MMMM yyyy").ToUpper() + " PROGRESS";
            lblProgressValue.Text = _progressPct + "%";
            lblStreakValue.Text = "🔥 " + _longestStreak;
            lblStreakSub.Text = _longestStreak + " Days – " + _streakHabitName;
            lblNewHabitsValue.Text = "📋 " + _activeHabits;
            lblNewHabitsSub.Text = _activeHabits + " habit" + (_activeHabits == 1 ? "" : "s") + " active";
        }

        // ── Bar chart ─────────────────────────────────────────────────────────
        private void DrawBarChart()
        {
            int w = picChart.Width, h = picChart.Height;
            if (w <= 0 || h <= 0 || _dailyData == null) return;

            bool dark = ThemeManager.IsDark;
            Color bgClr = dark ? Color.FromArgb(36, 36, 50) : Color.White;
            Color barClr = dark ? Color.FromArgb(100, 140, 220) : Color.FromArgb(100, 160, 220);
            Color txtClr = dark ? Color.FromArgb(180, 185, 200) : Color.Gray;
            Color gridClr = dark ? Color.FromArgb(55, 55, 75) : Color.FromArgb(230, 230, 235);

            Bitmap bmp = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(bgClr);

                int padL = 38, padB = 28, padT = 14, padR = 12;
                int chartW = w - padL - padR;
                int chartH = h - padT - padB;
                int count = _dailyData.Length;
                int maxVal = _dailyData.Length > 0 && _dailyData.Max() > 0
                             ? _dailyData.Max() : 1;

                float slotW = (float)chartW / count;
                float barW = slotW * 0.55f;

                // Grid lines
                using (Pen gridPen = new Pen(gridClr, 1))
                    for (int i = 0; i <= 4; i++)
                    {
                        float y = padT + chartH - (float)i / 4 * chartH;
                        g.DrawLine(gridPen, padL, y, padL + chartW, y);
                    }

                // Y-axis labels
                using (Font tf = new Font("Segoe UI", 7f))
                using (SolidBrush txtBr = new SolidBrush(txtClr))
                    for (int i = 0; i <= 4; i++)
                    {
                        float y = padT + chartH - (float)i / 4 * chartH;
                        int val = (int)Math.Round((double)i / 4 * maxVal);
                        g.DrawString(val.ToString(), tf, txtBr, 2, y - 9);
                    }

                // Bars + X labels
                using (Font tf = new Font("Segoe UI", 7f))
                using (SolidBrush txtBr = new SolidBrush(txtClr))
                using (SolidBrush barBr = new SolidBrush(barClr))
                    for (int i = 0; i < count; i++)
                    {
                        float barH = (float)_dailyData[i] / maxVal * chartH;
                        float x = padL + i * slotW + (slotW - barW) / 2f;
                        float y = padT + chartH - barH;
                        if (barH > 0)
                            g.FillRectangle(barBr, x, y, barW, barH);

                        int day = i + 1;
                        if (day == 1 || day % 5 == 0)
                            g.DrawString(day.ToString(), tf, txtBr, x - 1, h - padB + 4);
                    }

                // Y-axis title
                using (Font af = new Font("Segoe UI", 7.5f))
                using (SolidBrush txtBr = new SolidBrush(txtClr))
                {
                    g.TranslateTransform(11, h / 2 + 20);
                    g.RotateTransform(-90);
                    g.DrawString("Habits Done", af, txtBr, 0, 0);
                    g.ResetTransform();
                }
            }
            picChart.Image = bmp;
        }

        // ── Habit rows ────────────────────────────────────────────────────────
        private void PopulateHabitRows()
        {
            panelHabitRows.Controls.Clear();
            bool dark = ThemeManager.IsDark;
            Color rowBg = dark ? Color.FromArgb(36, 36, 50) : Color.White;
            Color nameFg = dark ? Color.FromArgb(220, 225, 240) : Color.FromArgb(30, 58, 95);
            Color divClr = dark ? Color.FromArgb(55, 55, 75) : Color.FromArgb(230, 235, 245);

            int y = 0;
            foreach (var (hName, pct, clr) in _habits)
            {
                Panel row = new Panel
                {
                    Size = new Size(panelHabitRows.Width - 2, 52),
                    Location = new Point(0, y),
                    BackColor = rowBg
                };

                // Colour dot
                Color iconClr = clr;
                Panel icon = new Panel
                {
                    Size = new Size(32, 32),
                    Location = new Point(6, 10),
                    BackColor = Color.Transparent
                };
                icon.Paint += (s, pe) =>
                {
                    pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    using (SolidBrush sb = new SolidBrush(iconClr))
                        pe.Graphics.FillEllipse(sb, 0, 0, 31, 31);
                };

                Label lblName = new Label
                {
                    Text = hName,
                    Font = new Font("Segoe UI", 10f),
                    ForeColor = nameFg,
                    Location = new Point(46, 16),
                    AutoSize = true
                };

                // Colour the percentage by completion level
                Color pctClr = pct >= 80
                    ? Color.FromArgb(39, 174, 96)
                    : pct >= 50
                        ? Color.FromArgb(230, 126, 34)
                        : Color.FromArgb(231, 76, 60);

                Label lblPct = new Label
                {
                    Text = pct + "%",
                    Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                    ForeColor = pctClr,
                    Size = new Size(58, 28),
                    Location = new Point(row.Width - 66, 12),
                    TextAlign = ContentAlignment.MiddleRight
                };

                Panel divider = new Panel
                {
                    Size = new Size(row.Width, 1),
                    Location = new Point(0, 51),
                    BackColor = divClr
                };

                row.Controls.Add(icon);
                row.Controls.Add(lblName);
                row.Controls.Add(lblPct);
                row.Controls.Add(divider);
                panelHabitRows.Controls.Add(row);
                y += 54;
            }
        }

        // ── Card painting ─────────────────────────────────────────────────────
        private void PaintCard(object sender, PaintEventArgs e)
        {
            Panel p = (Panel)sender;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle r = new Rectangle(0, 0, p.Width - 1, p.Height - 1);
            using (GraphicsPath path = MakeRoundRect(r, 10))
            {
                using (SolidBrush fill = new SolidBrush(p.BackColor))
                    e.Graphics.FillPath(fill, path);
                Color borderClr = ThemeManager.IsDark
                    ? Color.FromArgb(60, 60, 85)
                    : Color.FromArgb(215, 225, 240);
                using (Pen border = new Pen(borderClr, 1))
                    e.Graphics.DrawPath(border, path);
            }
        }

        private GraphicsPath MakeRoundRect(Rectangle b, int r)
        {
            int d = r * 2;
            GraphicsPath p = new GraphicsPath();
            p.AddArc(b.X, b.Y, d, d, 180, 90);
            p.AddArc(b.Right - d, b.Y, d, d, 270, 90);
            p.AddArc(b.Right - d, b.Bottom - d, d, d, 0, 90);
            p.AddArc(b.X, b.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        private void lblNewHabitsHeader_Click(object sender, EventArgs e) { }
    }
}
