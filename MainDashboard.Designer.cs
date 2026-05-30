using System;

namespace Daily_Tracker_ver2
{
    partial class MainDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDashboard));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button4 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.habitCard1 = new Daily_Tracker_ver2.HabitCard();
            this.habitCard2 = new Daily_Tracker_ver2.HabitCard();
            this.habitCard3 = new Daily_Tracker_ver2.HabitCard();
            this.habitCard4 = new Daily_Tracker_ver2.HabitCard();
            this.habitCard5 = new Daily_Tracker_ver2.HabitCard();
            this.habitCard6 = new Daily_Tracker_ver2.HabitCard();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Blue;
            this.splitContainer1.Panel1.Controls.Add(this.button4);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox2);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox3);
            this.splitContainer1.Panel1.Controls.Add(this.button3);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.LightGray;
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel2);
            this.splitContainer1.Panel2.Controls.Add(this.dateTimePicker1);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(1200, 700);
            this.splitContainer1.SplitterDistance = 350;
            this.splitContainer1.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(90)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(0, 660);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(196, 40);
            this.button4.TabIndex = 20;
            this.button4.Text = "🌙 Dark Mode";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(44, 306);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(30, 30);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 18;
            this.pictureBox2.TabStop = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Blue;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(55, 306);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(196, 40);
            this.button2.TabIndex = 17;
            this.button2.Text = "  Habit";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(42, 476);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(30, 30);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 16;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Blue;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(55, 471);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(196, 40);
            this.button3.TabIndex = 15;
            this.button3.Text = "  Monthly Review";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(55, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 37);
            this.button1.TabIndex = 5;
            this.button1.Text = "Profile";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(40, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(170, 110);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.AutoScrollMargin = new System.Drawing.Size(2, 3);
            this.flowLayoutPanel2.Controls.Add(this.habitCard1);
            this.flowLayoutPanel2.Controls.Add(this.habitCard2);
            this.flowLayoutPanel2.Controls.Add(this.habitCard3);
            this.flowLayoutPanel2.Controls.Add(this.habitCard4);
            this.flowLayoutPanel2.Controls.Add(this.habitCard5);
            this.flowLayoutPanel2.Controls.Add(this.habitCard6);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 44);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1791, 1240);
            this.flowLayoutPanel2.TabIndex = 12;
            // 
            // habitCard1
            // 
            this.habitCard1.BackColor = System.Drawing.Color.White;
            this.habitCard1.CurrentStreak = "0 DAYS";
            this.habitCard1.HabitColor = System.Drawing.Color.LightGray;
            this.habitCard1.HabitDuration = "10 min";
            this.habitCard1.HabitIcon = null;
            this.habitCard1.HabitTitle = "Morning Meditation";
            this.habitCard1.IsCompleted = false;
            this.habitCard1.Location = new System.Drawing.Point(3, 3);
            this.habitCard1.Name = "habitCard1";
            this.habitCard1.Size = new System.Drawing.Size(960, 100);
            this.habitCard1.StreakChart = null;
            this.habitCard1.TabIndex = 0;
            this.habitCard1.Load += new System.EventHandler(this.habitCard1_Load);
            // 
            // habitCard2
            // 
            this.habitCard2.BackColor = System.Drawing.Color.White;
            this.habitCard2.CurrentStreak = "0 DAYS";
            this.habitCard2.HabitColor = System.Drawing.Color.LightGray;
            this.habitCard2.HabitDuration = "2L";
            this.habitCard2.HabitIcon = null;
            this.habitCard2.HabitTitle = "Drink 2L Water";
            this.habitCard2.IsCompleted = false;
            this.habitCard2.Location = new System.Drawing.Point(3, 109);
            this.habitCard2.Name = "habitCard2";
            this.habitCard2.Size = new System.Drawing.Size(960, 100);
            this.habitCard2.StreakChart = null;
            this.habitCard2.TabIndex = 1;
            // 
            // habitCard3
            // 
            this.habitCard3.BackColor = System.Drawing.Color.White;
            this.habitCard3.CurrentStreak = "0 DAYS";
            this.habitCard3.HabitColor = System.Drawing.Color.LightGray;
            this.habitCard3.HabitDuration = "30 min";
            this.habitCard3.HabitIcon = null;
            this.habitCard3.HabitTitle = "Read Book";
            this.habitCard3.IsCompleted = false;
            this.habitCard3.Location = new System.Drawing.Point(3, 215);
            this.habitCard3.Name = "habitCard3";
            this.habitCard3.Size = new System.Drawing.Size(960, 100);
            this.habitCard3.StreakChart = null;
            this.habitCard3.TabIndex = 2;
            // 
            // habitCard4
            // 
            this.habitCard4.BackColor = System.Drawing.Color.White;
            this.habitCard4.CurrentStreak = "0 DAYS";
            this.habitCard4.HabitColor = System.Drawing.Color.LightGray;
            this.habitCard4.HabitDuration = "2 hrs";
            this.habitCard4.HabitIcon = null;
            this.habitCard4.HabitTitle = "Coding";
            this.habitCard4.IsCompleted = false;
            this.habitCard4.Location = new System.Drawing.Point(3, 321);
            this.habitCard4.Name = "habitCard4";
            this.habitCard4.Size = new System.Drawing.Size(960, 100);
            this.habitCard4.StreakChart = null;
            this.habitCard4.TabIndex = 3;
            // 
            // habitCard5
            // 
            this.habitCard5.BackColor = System.Drawing.Color.White;
            this.habitCard5.CurrentStreak = "0 DAYS";
            this.habitCard5.HabitColor = System.Drawing.Color.LightGray;
            this.habitCard5.HabitDuration = "30 min";
            this.habitCard5.HabitIcon = null;
            this.habitCard5.HabitTitle = "Evening Walk";
            this.habitCard5.IsCompleted = false;
            this.habitCard5.Location = new System.Drawing.Point(3, 427);
            this.habitCard5.Name = "habitCard5";
            this.habitCard5.Size = new System.Drawing.Size(960, 100);
            this.habitCard5.StreakChart = null;
            this.habitCard5.TabIndex = 4;
            // 
            // habitCard6
            // 
            this.habitCard6.BackColor = System.Drawing.Color.White;
            this.habitCard6.CurrentStreak = "0 DAYS";
            this.habitCard6.HabitColor = System.Drawing.Color.LightGray;
            this.habitCard6.HabitDuration = "5 Times a Day";
            this.habitCard6.HabitIcon = null;
            this.habitCard6.HabitTitle = "Offer Prayer";
            this.habitCard6.IsCompleted = false;
            this.habitCard6.Location = new System.Drawing.Point(3, 533);
            this.habitCard6.Name = "habitCard6";
            this.habitCard6.Size = new System.Drawing.Size(960, 100);
            this.habitCard6.StreakChart = null;
            this.habitCard6.TabIndex = 5;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(190, 8);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(300, 26);
            this.dateTimePicker1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(6, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "Today\'s Record:";
            // 
            // MainDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "MainDashboard";
            this.Text = "Daily Habit Tracker";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private HabitCard habitCard1;
        private HabitCard habitCard2;
        private HabitCard habitCard3;
        private HabitCard habitCard4;
        private HabitCard habitCard5;
        private HabitCard habitCard6;
        private System.Windows.Forms.Button button4;
    }
}