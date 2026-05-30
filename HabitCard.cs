using System;
using System.Drawing;
using System.Windows.Forms;

namespace Daily_Tracker_ver2
{
    public partial class HabitCard : UserControl
    {
        private Color habitColor = Color.LightGray;
        private EventHandler completionChanged;

        public HabitCard()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            if (panelCard != null) panelCard.BackColor = Color.White;
            if (checkBoxComplete != null)
                checkBoxComplete.CheckedChanged += CheckBoxComplete_CheckedChanged;

            // Stretch panelCard whenever the UserControl resizes
            this.Resize += HabitCard_Resize;
        }

        private void HabitCard_Resize(object sender, EventArgs e)
        {
            if (panelCard == null) return;
            panelCard.Width = this.Width - 6;
            panelCard.Height = this.Height - 6;

            // Reposition right-side controls relative to new width
            int right = panelCard.Width;
            if (labelStreak != null) labelStreak.Left = right - 120;
            if (labelStreakLabel != null) labelStreakLabel.Left = right - 140;
            if (pictureBoxChart != null) pictureBoxChart.Left = right - 290;
            if (checkBoxComplete != null) checkBoxComplete.Left = right - 430;
        }

        // ── Properties ───────────────────────────────────────────────────────
        public string HabitTitle
        {
            get => labelTitle?.Text ?? string.Empty;
            set { if (labelTitle != null) labelTitle.Text = value; }
        }

        public string HabitDuration
        {
            get => labelDuration?.Text ?? string.Empty;
            set { if (labelDuration != null) labelDuration.Text = value; }
        }

        public Image HabitIcon
        {
            get => pictureBoxIcon?.Image;
            set { if (pictureBoxIcon != null) pictureBoxIcon.Image = value; }
        }

        public bool IsCompleted
        {
            get => checkBoxComplete != null && checkBoxComplete.Checked;
            set { if (checkBoxComplete != null) checkBoxComplete.Checked = value; }
        }

        public string CurrentStreak
        {
            get => labelStreak?.Text ?? string.Empty;
            set { if (labelStreak != null) labelStreak.Text = value; }
        }

        public Image StreakChart
        {
            get => pictureBoxChart?.Image;
            set { if (pictureBoxChart != null) pictureBoxChart.Image = value; }
        }

        public Color HabitColor
        {
            get => habitColor;
            set
            {
                habitColor = value;
                if (pictureBoxIcon != null) pictureBoxIcon.BackColor = value;
                Refresh();
            }
        }

        public event EventHandler CompletionChanged
        {
            add { completionChanged += value; }
            remove { completionChanged -= value; }
        }

        // ── Internal handlers ─────────────────────────────────────────────────
        private void CheckBoxComplete_CheckedChanged(object sender, EventArgs e)
        {
            if (IsCompleted)
            {
                if (panelCard != null) panelCard.BorderStyle = BorderStyle.Fixed3D;
                if (labelTitle != null) labelTitle.ForeColor = Color.Green;
            }
            else
            {
                if (panelCard != null) panelCard.BorderStyle = BorderStyle.FixedSingle;
                if (labelTitle != null) labelTitle.ForeColor = Color.Black;
            }
            completionChanged?.Invoke(this, EventArgs.Empty);
        }

        // ── Designer stubs (keep to satisfy wired events) ─────────────────────
        private void labelStreak_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void label13_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label14_Click(object sender, EventArgs e) { }
        private void label15_Click(object sender, EventArgs e) { }
        private void label17_Click(object sender, EventArgs e) { }
        private void label18_Click(object sender, EventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void pictureBoxIcon_Click(object sender, EventArgs e) { }
        private void panelCard_Paint(object sender, PaintEventArgs e) { }
    }
}