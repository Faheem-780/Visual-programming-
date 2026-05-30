using System;
using System.Drawing;
using System.Windows.Forms;

namespace Daily_Tracker_ver2
{
    /// <summary>
    /// Central theme engine.  Call ApplyTheme(form) on any form that should be
    /// themed, then call ToggleTheme() from the dark-mode button.
    /// </summary>
    public static class ThemeManager
    {
        // ── State ────────────────────────────────────────────────────────────
        public static bool IsDark { get; private set; } = false;

        // ── Palette ──────────────────────────────────────────────────────────
        // Light
        private static readonly Color LightFormBg = Color.FromArgb(238, 242, 247);
        private static readonly Color LightSidebarBg = Color.Blue;
        private static readonly Color LightContentBg = Color.LightGray;
        private static readonly Color LightCardBg = Color.White;
        private static readonly Color LightFore = Color.FromArgb(30, 58, 95);
        private static readonly Color LightSubFore = Color.Gray;
        private static readonly Color LightInputBg = Color.White;
        private static readonly Color LightInputFore = Color.Black;

        // Dark
        private static readonly Color DarkFormBg = Color.FromArgb(18, 18, 24);
        private static readonly Color DarkSidebarBg = Color.FromArgb(28, 28, 38);
        private static readonly Color DarkContentBg = Color.FromArgb(22, 22, 30);
        private static readonly Color DarkCardBg = Color.FromArgb(36, 36, 50);
        private static readonly Color DarkFore = Color.FromArgb(220, 225, 240);
        private static readonly Color DarkSubFore = Color.FromArgb(140, 145, 165);
        private static readonly Color DarkInputBg = Color.FromArgb(44, 44, 60);
        private static readonly Color DarkInputFore = Color.FromArgb(220, 225, 240);

        // ── Public helpers ───────────────────────────────────────────────────
        public static void ToggleTheme()
        {
            IsDark = !IsDark;
            // Re-apply to every open form
            foreach (Form f in Application.OpenForms)
                ApplyTheme(f);
        }

        /// <summary>Apply current theme to a form and all its descendants.</summary>
        public static void ApplyTheme(Form form)
        {
            if (form == null) return;
            ApplyToControl(form, isRoot: true, formType: form.GetType().Name);
        }

        // ── Core recursive walker ────────────────────────────────────────────
        private static void ApplyToControl(Control ctrl, bool isRoot = false,
                                           string formType = "")
        {
            Color bg = IsDark ? DarkFormBg : LightFormBg;
            Color card = IsDark ? DarkCardBg : LightCardBg;
            Color fore = IsDark ? DarkFore : LightFore;
            Color sub = IsDark ? DarkSubFore : LightSubFore;
            Color inp = IsDark ? DarkInputBg : LightInputBg;
            Color inpF = IsDark ? DarkInputFore : LightInputFore;

            // ── SplitContainer panels (MainDashboard sidebar / content) ──────
            if (ctrl is SplitContainer sc)
            {
                sc.BackColor = bg;
                sc.Panel1.BackColor = IsDark ? DarkSidebarBg : LightSidebarBg;
                sc.Panel2.BackColor = IsDark ? DarkContentBg : LightContentBg;
                foreach (Control c in sc.Panel1.Controls) ApplyToControl(c);
                foreach (Control c in sc.Panel2.Controls) ApplyToControl(c);
                return; // children already handled above
            }

            // ── Form root ────────────────────────────────────────────────────
            if (isRoot && ctrl is Form f)
            {
                f.BackColor = bg;
            }

            // ── Individual control types ─────────────────────────────────────
            if (ctrl is Panel p)
            {
                // Sidebar panels (blue in light mode) stay as-is – handled by
                // the SplitContainer branch above.  All other panels get card colour.
                if (p.Parent is SplitContainer || p.Parent?.Parent is SplitContainer)
                {
                    // Already coloured by SplitContainer branch
                }
                else
                {
                    p.BackColor = card;
                }
            }
            else if (ctrl is FlowLayoutPanel flp)
            {
                flp.BackColor = IsDark ? DarkContentBg : LightContentBg;
            }
            else if (ctrl is Label lbl)
            {
                // Keep sidebar button-labels white in both modes
                bool onSidebar = IsOnSidebar(lbl);
                lbl.ForeColor = onSidebar ? Color.White : fore;
                lbl.BackColor = Color.Transparent;
            }
            else if (ctrl is Button btn)
            {
                bool onSidebar = IsOnSidebar(btn);
                if (onSidebar)
                {
                    // Sidebar nav buttons
                    btn.BackColor = IsDark ? DarkSidebarBg : LightSidebarBg;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = btn.BackColor;
                }
                else if (btn.Name == "button5") // dark-mode toggle button
                {
                    btn.BackColor = IsDark ? Color.FromArgb(80, 80, 110) : Color.DimGray;
                    btn.ForeColor = Color.White;
                    btn.Text = IsDark ? "☀ Light Mode" : "🌙 Dark Mode";
                }
                else
                {
                    // Keep accent buttons (red Profile, etc.) their own colour
                    // but update foreground only if it was white/dark system colour
                    if (btn.BackColor == Color.Blue ||
                        btn.BackColor == (IsDark ? LightSidebarBg : DarkSidebarBg))
                    {
                        btn.BackColor = IsDark ? DarkSidebarBg : LightSidebarBg;
                    }
                }
            }
            else if (ctrl is TextBox tb)
            {
                tb.BackColor = inp;
                tb.ForeColor = inpF;
            }
            else if (ctrl is CheckBox cb)
            {
                cb.BackColor = Color.Transparent;
                cb.ForeColor = fore;
            }
            else if (ctrl is GroupBox gb)
            {
                gb.BackColor = card;
                gb.ForeColor = fore;
            }
            else if (ctrl is DataGridView dgv)
            {
                dgv.BackgroundColor = IsDark ? DarkCardBg : Color.White;
                dgv.GridColor = IsDark ? Color.FromArgb(60, 60, 80) : Color.Silver;
                dgv.DefaultCellStyle.BackColor = IsDark ? DarkCardBg : Color.White;
                dgv.DefaultCellStyle.ForeColor = IsDark ? DarkFore : Color.Black;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = IsDark ? DarkSidebarBg : Color.Blue;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgv.EnableHeadersVisualStyles = false;
            }
            else if (ctrl is PictureBox)
            {
                // Leave picture boxes alone
            }
            else if (ctrl is DateTimePicker dtp)
            {
                dtp.BackColor = inp;
                dtp.ForeColor = inpF;
                dtp.CalendarMonthBackground = inp;
                dtp.CalendarForeColor = inpF;
            }

            // ── Recurse into children ────────────────────────────────────────
            foreach (Control child in ctrl.Controls)
                ApplyToControl(child);
        }

        // ── HabitCard theming (called explicitly from MainDashboard) ─────────
        public static void ApplyToHabitCard(Control habitCard)
        {
            Color card = IsDark ? DarkCardBg : Color.White;
            Color fore = IsDark ? DarkFore : Color.FromArgb(30, 58, 95);
            Color sub = IsDark ? DarkSubFore : Color.Gray;

            habitCard.BackColor = card;
            foreach (Control c in habitCard.Controls)
                ApplyToHabitCardControl(c, card, fore, sub);
        }

        private static void ApplyToHabitCardControl(Control c,
            Color card, Color fore, Color sub)
        {
            if (c is Panel p)
            {
                p.BackColor = card;
                foreach (Control cc in p.Controls)
                    ApplyToHabitCardControl(cc, card, fore, sub);
            }
            else if (c is Label lbl)
            {
                // Streak value stays its colour; other labels get fore/sub
                if (lbl.Font.Bold && lbl.Font.Size >= 12)
                    lbl.ForeColor = fore;
                else
                    lbl.ForeColor = sub;
                lbl.BackColor = Color.Transparent;
            }
            else if (c is CheckBox cb)
            {
                cb.BackColor = Color.Transparent;
            }
        }

        // ── Utility ──────────────────────────────────────────────────────────
        private static bool IsOnSidebar(Control ctrl)
        {
            Control cur = ctrl.Parent;
            while (cur != null)
            {
                // If this ancestor's parent is a SplitContainer, then this ancestor is one of its panels.
                var scParent = cur.Parent as SplitContainer;
                if (scParent != null && scParent.Panel1 == cur)
                {
                    // Panel1 is the sidebar
                    return true;
                }
                cur = cur.Parent;
            }
            return false;
        }
    }
}