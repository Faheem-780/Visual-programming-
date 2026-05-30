using Daily_Tracker_ver2.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Daily_Tracker_ver2
{
    public partial class Habit : Form
    {
        // ── Available emoji choices shown in the picker ───────────────────────
        private static readonly string[] EmojiOptions =
        {
            "🧘","💧","📖","💻","🚶","🕌","🏋️","🥗","😴","✍️",
            "🎵","🧹","🐕","🌿","💊","📓","🎯","🛁","🤸","⭐"
        };

        // ── Available accent colours ──────────────────────────────────────────
        private static readonly (string Label, Color Value)[] ColourOptions =
        {
            ("Blue",   Color.FromArgb(100,149,237)),
            ("Teal",   Color.FromArgb( 26,188,156)),
            ("Green",  Color.FromArgb( 39,174, 96)),
            ("Orange", Color.FromArgb(230,126, 34)),
            ("Red",    Color.FromArgb(231, 76, 60)),
            ("Purple", Color.FromArgb(142, 68,173)),
            ("Sky",    Color.FromArgb( 52,152,219)),
            ("Gold",   Color.FromArgb(241,196, 15)),
        };

        private List<DatabaseHelper.HabitDef> _habits = new List<DatabaseHelper.HabitDef>();
        private int _selectedIndex = -1;   // row selected in the list

        public Habit()
        {
            InitializeComponent();
            this.Load += Habit_Load;
        }

        // ── Load ──────────────────────────────────────────────────────────────
        private void Habit_Load(object sender, EventArgs e)
        {
            // Populate emoji combo
            cmbEmoji.Items.Clear();
            foreach (var em in EmojiOptions) cmbEmoji.Items.Add(em);
            cmbEmoji.SelectedIndex = 0;

            // Populate colour combo
            cmbColour.Items.Clear();
            foreach (var (lbl, _) in ColourOptions) cmbColour.Items.Add(lbl);
            cmbColour.SelectedIndex = 0;

            ThemeManager.ApplyTheme(this);
            RefreshHabitList();
        }

        // ── Load habits from DB and redraw the list ───────────────────────────
        private void RefreshHabitList()
        {
            _habits = DatabaseHelper.GetHabits(SessionManager.UserId);
            _selectedIndex = -1;
            DrawHabitList();
            UpdateButtonStates();
        }

        private void DrawHabitList()
        {
            panelList.SuspendLayout();
            panelList.Controls.Clear();

            bool dark = ThemeManager.IsDark;
            Color rowBg = dark ? Color.FromArgb(36, 36, 50) : Color.White;
            Color altBg = dark ? Color.FromArgb(42, 42, 58) : Color.FromArgb(248, 249, 252);
            Color selBg = dark ? Color.FromArgb(55, 80, 130) : Color.FromArgb(214, 228, 255);
            Color nameFg = dark ? Color.FromArgb(220, 225, 240) : Color.FromArgb(30, 58, 95);
            Color subFg = dark ? Color.FromArgb(140, 145, 165) : Color.Gray;
            Color divClr = dark ? Color.FromArgb(55, 55, 75) : Color.FromArgb(230, 235, 245);

            int y = 0;
            for (int i = 0; i < _habits.Count; i++)
            {
                var h = _habits[i];
                int idx = i;   // capture for lambda

                // Parse stored colour hex → Color
                Color accentClr = HexToColor(h.Colour);

                Panel row = new Panel
                {
                    Size = new Size(panelList.Width - 2, 58),
                    Location = new Point(0, y),
                    BackColor = (i == _selectedIndex) ? selBg : (i % 2 == 0 ? rowBg : altBg),
                    Cursor = Cursors.Hand,
                    Tag = idx
                };

                // Colour dot / emoji badge
                Panel badge = new Panel
                {
                    Size = new Size(40, 40),
                    Location = new Point(8, 9),
                    BackColor = Color.Transparent
                };
                Color badgeClr = accentClr;
                string emoji = h.Emoji;
                badge.Paint += (s, pe) =>
                {
                    pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var path = RoundedRect(new Rectangle(0, 0, 39, 39), 8))
                    using (var br = new SolidBrush(badgeClr))
                        pe.Graphics.FillPath(br, path);
                    using (var f = new Font("Segoe UI Emoji", 18f, FontStyle.Regular, GraphicsUnit.Pixel))
                    using (var sf = new StringFormat
                    { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                        pe.Graphics.DrawString(emoji, f, Brushes.White, new RectangleF(0, 0, 39, 39), sf);
                };

                Label lblName = new Label
                {
                    Text = h.Name,
                    Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                    ForeColor = nameFg,
                    Location = new Point(56, 8),
                    Size = new Size(panelList.Width - 120, 22),
                    BackColor = Color.Transparent
                };

                Label lblDur = new Label
                {
                    Text = h.Duration,
                    Font = new Font("Segoe UI", 9f),
                    ForeColor = subFg,
                    Location = new Point(56, 32),
                    AutoSize = true,
                    BackColor = Color.Transparent
                };

                Panel divider = new Panel
                {
                    Size = new Size(row.Width, 1),
                    Location = new Point(0, 57),
                    BackColor = divClr
                };

                // Clicking anywhere on the row selects it
                EventHandler selectHandler = (s, ev) =>
                {
                    _selectedIndex = idx;
                    DrawHabitList();
                    UpdateButtonStates();
                };
                row.Click += selectHandler;
                badge.Click += selectHandler;
                lblName.Click += selectHandler;
                lblDur.Click += selectHandler;

                row.Controls.Add(badge);
                row.Controls.Add(lblName);
                row.Controls.Add(lblDur);
                row.Controls.Add(divider);
                panelList.Controls.Add(row);
                y += 59;
            }

            // Empty-state message
            if (_habits.Count == 0)
            {
                Label empty = new Label
                {
                    Text = "No habits yet.\nClick  ＋ Add Habit  to get started!",
                    Font = new Font("Segoe UI", 11f),
                    ForeColor = Color.Gray,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(panelList.Width, 120),
                    Location = new Point(0, 20)
                };
                panelList.Controls.Add(empty);
            }

            panelList.ResumeLayout();
        }

        private void UpdateButtonStates()
        {
            btnDelete.Enabled = _selectedIndex >= 0 && _selectedIndex < _habits.Count;
        }

        // ── Add Habit button ──────────────────────────────────────────────────
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string dur = txtDuration.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a habit name.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            string emoji = cmbEmoji.SelectedItem?.ToString() ?? "⭐";
            Color colour = ColourOptions[Math.Max(0,
                            Math.Min(cmbColour.SelectedIndex, ColourOptions.Length - 1))].Value;
            string hex = ColorToHex(colour);

            try
            {
                DatabaseHelper.AddHabit(SessionManager.UserId, name, dur, emoji, hex);
                txtName.Clear();
                txtDuration.Clear();
                cmbEmoji.SelectedIndex = 0;
                cmbColour.SelectedIndex = 0;
                RefreshHabitList();

                // Tell MainDashboard to reload cards if it's open
                NotifyDashboard();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
                when (ex.Number == 1062)   // duplicate key
            {
                MessageBox.Show($"A habit named \"{name}\" already exists.",
                    "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not add habit: " + ex.Message,
                    "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Delete Habit button ───────────────────────────────────────────────
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedIndex < 0 || _selectedIndex >= _habits.Count) return;

            var h = _habits[_selectedIndex];
            var result = MessageBox.Show(
                $"Delete \"{h.Name}\"?\n\nThis will permanently remove the habit " +
                "and ALL its completion history.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            try
            {
                DatabaseHelper.DeleteHabit(SessionManager.UserId, h.Id, h.Name);
                RefreshHabitList();
                NotifyDashboard();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not delete habit: " + ex.Message,
                    "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Notify MainDashboard to reload its habit cards ────────────────────
        private static void NotifyDashboard()
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is MainDashboard dash)
                {
                    dash.ReloadHabits();
                    break;
                }
            }
        }

        // ── Colour preview swatch ─────────────────────────────────────────────
        private void cmbColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = cmbColour.SelectedIndex;
            if (idx < 0 || idx >= ColourOptions.Length) return;
            panelColourPreview.BackColor = ColourOptions[idx].Value;
        }

        // ── Helpers ───────────────────────────────────────────────────────────
        private static string ColorToHex(Color c)
            => $"#{c.R:X2}{c.G:X2}{c.B:X2}";

        private static Color HexToColor(string hex)
        {
            try { return ColorTranslator.FromHtml(hex); }
            catch { return Color.CornflowerBlue; }
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
    }
}