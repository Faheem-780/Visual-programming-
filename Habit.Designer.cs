namespace Daily_Tracker_ver2
{
    partial class Habit
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            // ── Left panel: Add-habit form ────────────────────────────────────
            this.panelLeft = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.lblEmoji = new System.Windows.Forms.Label();
            this.cmbEmoji = new System.Windows.Forms.ComboBox();
            this.lblColour = new System.Windows.Forms.Label();
            this.cmbColour = new System.Windows.Forms.ComboBox();
            this.panelColourPreview = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();

            // ── Right panel: habit list ───────────────────────────────────────
            this.panelRight = new System.Windows.Forms.Panel();
            this.lblListTitle = new System.Windows.Forms.Label();
            this.panelList = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();

            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();

            // ── panelLeft ─────────────────────────────────────────────────────
            this.panelLeft.BackColor = System.Drawing.Color.White;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(380, 660);
            this.panelLeft.Anchor = System.Windows.Forms.AnchorStyles.Top
                                      | System.Windows.Forms.AnchorStyles.Bottom
                                      | System.Windows.Forms.AnchorStyles.Left;
            this.panelLeft.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblPageTitle, this.lblName, this.txtName,
                this.lblDuration,  this.txtDuration,
                this.lblEmoji,     this.cmbEmoji,
                this.lblColour,    this.cmbColour, this.panelColourPreview,
                this.btnAdd
            });

            // Title
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 18F,
                System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.lblPageTitle.Location = new System.Drawing.Point(24, 24);
            this.lblPageTitle.Text = "➕  Add New Habit";

            // Name label + textbox
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10F,
                System.Drawing.FontStyle.Bold);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.lblName.Location = new System.Drawing.Point(24, 90);
            this.lblName.Text = "Habit Name *";

            this.txtName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtName.Location = new System.Drawing.Point(24, 116);
            this.txtName.Size = new System.Drawing.Size(330, 32);
            this.txtName.MaxLength = 100;
            this.txtName.Text = "e.g. Morning Run";
            this.txtName.ForeColor = System.Drawing.Color.Gray;

            // Duration label + textbox
            this.lblDuration.AutoSize = true;
            this.lblDuration.Font = new System.Drawing.Font("Segoe UI", 10F,
                System.Drawing.FontStyle.Bold);
            this.lblDuration.ForeColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.lblDuration.Location = new System.Drawing.Point(24, 168);
            this.lblDuration.Text = "Duration  (optional)";

            this.txtDuration.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDuration.Location = new System.Drawing.Point(24, 194);
            this.txtDuration.Size = new System.Drawing.Size(330, 32);
            this.txtDuration.MaxLength = 40;
            this.txtDuration.Text = "e.g. 30 min";
            this.txtDuration.ForeColor = System.Drawing.Color.Gray;

            // Emoji picker
            this.lblEmoji.AutoSize = true;
            this.lblEmoji.Font = new System.Drawing.Font("Segoe UI", 10F,
                System.Drawing.FontStyle.Bold);
            this.lblEmoji.ForeColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.lblEmoji.Location = new System.Drawing.Point(24, 248);
            this.lblEmoji.Text = "Icon";

            this.cmbEmoji.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmoji.Font = new System.Drawing.Font("Segoe UI Emoji", 14F);
            this.cmbEmoji.Location = new System.Drawing.Point(24, 274);
            this.cmbEmoji.Size = new System.Drawing.Size(100, 34);
            this.cmbEmoji.ItemHeight = 28;

            // Colour picker
            this.lblColour.AutoSize = true;
            this.lblColour.Font = new System.Drawing.Font("Segoe UI", 10F,
                System.Drawing.FontStyle.Bold);
            this.lblColour.ForeColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.lblColour.Location = new System.Drawing.Point(24, 330);
            this.lblColour.Text = "Accent Colour";

            this.cmbColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColour.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbColour.Location = new System.Drawing.Point(24, 356);
            this.cmbColour.Size = new System.Drawing.Size(200, 30);
            this.cmbColour.SelectedIndexChanged +=
                new System.EventHandler(this.cmbColour_SelectedIndexChanged);

            // Colour preview swatch
            this.panelColourPreview.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panelColourPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColourPreview.Location = new System.Drawing.Point(234, 356);
            this.panelColourPreview.Size = new System.Drawing.Size(120, 30);

            // Add button
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 12F,
                System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(24, 416);
            this.btnAdd.Size = new System.Drawing.Size(330, 48);
            this.btnAdd.Text = "＋  Add Habit";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // ── panelRight ────────────────────────────────────────────────────
            this.panelRight.BackColor = System.Drawing.Color.FromArgb(238, 242, 247);
            this.panelRight.Location = new System.Drawing.Point(382, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(518, 660);
            this.panelRight.Anchor = System.Windows.Forms.AnchorStyles.Top
                                      | System.Windows.Forms.AnchorStyles.Bottom
                                      | System.Windows.Forms.AnchorStyles.Left
                                      | System.Windows.Forms.AnchorStyles.Right;
            this.panelRight.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblListTitle, this.panelList, this.btnDelete
            });

            // List title
            this.lblListTitle.AutoSize = true;
            this.lblListTitle.Font = new System.Drawing.Font("Segoe UI", 14F,
                System.Drawing.FontStyle.Bold);
            this.lblListTitle.ForeColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.lblListTitle.Location = new System.Drawing.Point(16, 24);
            this.lblListTitle.Text = "My Habits";

            // Scrollable habit list
            this.panelList.AutoScroll = true;
            this.panelList.BackColor = System.Drawing.Color.White;
            this.panelList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelList.Location = new System.Drawing.Point(16, 64);
            this.panelList.Size = new System.Drawing.Size(486, 530);
            this.panelList.Anchor = System.Windows.Forms.AnchorStyles.Top
                                        | System.Windows.Forms.AnchorStyles.Bottom
                                        | System.Windows.Forms.AnchorStyles.Left
                                        | System.Windows.Forms.AnchorStyles.Right;

            // Delete button
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 11F,
                System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(16, 604);
            this.btnDelete.Size = new System.Drawing.Size(486, 44);
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom
                                               | System.Windows.Forms.AnchorStyles.Left
                                               | System.Windows.Forms.AnchorStyles.Right;
            this.btnDelete.Text = "🗑  Delete Selected Habit";
            this.btnDelete.Enabled = false;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // ── Form ──────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 660);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelRight);
            this.Name = "Habit";
            this.Text = "Manage Habits";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Label lblEmoji;
        private System.Windows.Forms.ComboBox cmbEmoji;
        private System.Windows.Forms.Label lblColour;
        private System.Windows.Forms.ComboBox cmbColour;
        private System.Windows.Forms.Panel panelColourPreview;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.Panel panelList;
        private System.Windows.Forms.Button btnDelete;
    }
}