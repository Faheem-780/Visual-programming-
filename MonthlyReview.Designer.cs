namespace Daily_Tracker_ver2
{
    partial class MonthlyReview
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
            this.panelContent = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.panelCardProgress = new System.Windows.Forms.Panel();
            this.lblMonthTitle = new System.Windows.Forms.Label();
            this.lblProgressValue = new System.Windows.Forms.Label();
            this.panelCardStreak = new System.Windows.Forms.Panel();
            this.lblStreakHeader = new System.Windows.Forms.Label();
            this.lblStreakValue = new System.Windows.Forms.Label();
            this.lblStreakSub = new System.Windows.Forms.Label();
            this.panelCardHabits = new System.Windows.Forms.Panel();
            this.lblNewHabitsHeader = new System.Windows.Forms.Label();
            this.lblNewHabitsValue = new System.Windows.Forms.Label();
            this.lblNewHabitsSub = new System.Windows.Forms.Label();
            this.panelChartOuter = new System.Windows.Forms.Panel();
            this.lblChartTitle = new System.Windows.Forms.Label();
            this.picChart = new System.Windows.Forms.PictureBox();
            this.panelRatesOuter = new System.Windows.Forms.Panel();
            this.lblRatesTitle = new System.Windows.Forms.Label();
            this.lblColLeft = new System.Windows.Forms.Label();
            this.lblColRight = new System.Windows.Forms.Label();
            this.panelHabitRows = new System.Windows.Forms.Panel();
            this.panelContent.SuspendLayout();
            this.panelCardProgress.SuspendLayout();
            this.panelCardStreak.SuspendLayout();
            this.panelCardHabits.SuspendLayout();
            this.panelChartOuter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picChart)).BeginInit();
            this.panelRatesOuter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(247)))));
            this.panelContent.Controls.Add(this.lblPageTitle);
            this.panelContent.Controls.Add(this.panelCardProgress);
            this.panelContent.Controls.Add(this.panelCardStreak);
            this.panelContent.Controls.Add(this.panelCardHabits);
            this.panelContent.Controls.Add(this.panelChartOuter);
            this.panelContent.Controls.Add(this.panelRatesOuter);
            this.panelContent.Location = new System.Drawing.Point(1, 3);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1199, 757);
            this.panelContent.TabIndex = 2;
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(95)))));
            this.lblPageTitle.Location = new System.Drawing.Point(18, 14);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(305, 45);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "MONTHLY REVIEW";
            // 
            // panelCardProgress
            // 
            this.panelCardProgress.BackColor = System.Drawing.Color.White;
            this.panelCardProgress.Controls.Add(this.lblMonthTitle);
            this.panelCardProgress.Controls.Add(this.lblProgressValue);
            this.panelCardProgress.Location = new System.Drawing.Point(18, 60);
            this.panelCardProgress.Name = "panelCardProgress";
            this.panelCardProgress.Size = new System.Drawing.Size(295, 110);
            this.panelCardProgress.TabIndex = 1;
            this.panelCardProgress.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintCard);
            // 
            // lblMonthTitle
            // 
            this.lblMonthTitle.AutoSize = true;
            this.lblMonthTitle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblMonthTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblMonthTitle.Location = new System.Drawing.Point(14, 12);
            this.lblMonthTitle.Name = "lblMonthTitle";
            this.lblMonthTitle.Size = new System.Drawing.Size(164, 21);
            this.lblMonthTitle.TabIndex = 0;
            this.lblMonthTitle.Text = "MAY 2026 PROGRESS";
            // 
            // lblProgressValue
            // 
            this.lblProgressValue.AutoSize = true;
            this.lblProgressValue.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblProgressValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(95)))));
            this.lblProgressValue.Location = new System.Drawing.Point(93, 32);
            this.lblProgressValue.Name = "lblProgressValue";
            this.lblProgressValue.Size = new System.Drawing.Size(145, 74);
            this.lblProgressValue.TabIndex = 1;
            this.lblProgressValue.Text = "81%";
            // 
            // panelCardStreak
            // 
            this.panelCardStreak.BackColor = System.Drawing.Color.White;
            this.panelCardStreak.Controls.Add(this.lblStreakHeader);
            this.panelCardStreak.Controls.Add(this.lblStreakValue);
            this.panelCardStreak.Controls.Add(this.lblStreakSub);
            this.panelCardStreak.Location = new System.Drawing.Point(328, 60);
            this.panelCardStreak.Name = "panelCardStreak";
            this.panelCardStreak.Size = new System.Drawing.Size(310, 110);
            this.panelCardStreak.TabIndex = 2;
            this.panelCardStreak.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintCard);
            // 
            // lblStreakHeader
            // 
            this.lblStreakHeader.AutoSize = true;
            this.lblStreakHeader.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblStreakHeader.ForeColor = System.Drawing.Color.Gray;
            this.lblStreakHeader.Location = new System.Drawing.Point(14, 12);
            this.lblStreakHeader.Name = "lblStreakHeader";
            this.lblStreakHeader.Size = new System.Drawing.Size(232, 21);
            this.lblStreakHeader.TabIndex = 0;
            this.lblStreakHeader.Text = "LONGEST STREAK THIS MONTH";
            // 
            // lblStreakValue
            // 
            this.lblStreakValue.AutoSize = true;
            this.lblStreakValue.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblStreakValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(95)))));
            this.lblStreakValue.Location = new System.Drawing.Point(159, 32);
            this.lblStreakValue.Name = "lblStreakValue";
            this.lblStreakValue.Size = new System.Drawing.Size(148, 60);
            this.lblStreakValue.TabIndex = 1;
            this.lblStreakValue.Text = "🔥 24";
            // 
            // lblStreakSub
            // 
            this.lblStreakSub.AutoSize = true;
            this.lblStreakSub.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblStreakSub.ForeColor = System.Drawing.Color.Gray;
            this.lblStreakSub.Location = new System.Drawing.Point(14, 84);
            this.lblStreakSub.Name = "lblStreakSub";
            this.lblStreakSub.Size = new System.Drawing.Size(201, 21);
            this.lblStreakSub.TabIndex = 2;
            this.lblStreakSub.Text = "24 Days - Morning Reading";
            // 
            // panelCardHabits
            // 
            this.panelCardHabits.BackColor = System.Drawing.Color.White;
            this.panelCardHabits.Controls.Add(this.lblNewHabitsHeader);
            this.panelCardHabits.Controls.Add(this.lblNewHabitsValue);
            this.panelCardHabits.Controls.Add(this.lblNewHabitsSub);
            this.panelCardHabits.Location = new System.Drawing.Point(653, 60);
            this.panelCardHabits.Name = "panelCardHabits";
            this.panelCardHabits.Size = new System.Drawing.Size(295, 110);
            this.panelCardHabits.TabIndex = 3;
            this.panelCardHabits.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintCard);
            // 
            // lblNewHabitsHeader
            // 
            this.lblNewHabitsHeader.AutoSize = true;
            this.lblNewHabitsHeader.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblNewHabitsHeader.ForeColor = System.Drawing.Color.Gray;
            this.lblNewHabitsHeader.Location = new System.Drawing.Point(20, 11);
            this.lblNewHabitsHeader.Name = "lblNewHabitsHeader";
            this.lblNewHabitsHeader.Size = new System.Drawing.Size(155, 21);
            this.lblNewHabitsHeader.TabIndex = 0;
            this.lblNewHabitsHeader.Text = "NEW HABITS ADDED";
            this.lblNewHabitsHeader.Click += new System.EventHandler(this.lblNewHabitsHeader_Click);
            // 
            // lblNewHabitsValue
            // 
            this.lblNewHabitsValue.AutoSize = true;
            this.lblNewHabitsValue.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblNewHabitsValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(95)))));
            this.lblNewHabitsValue.Location = new System.Drawing.Point(10, 32);
            this.lblNewHabitsValue.Name = "lblNewHabitsValue";
            this.lblNewHabitsValue.Size = new System.Drawing.Size(156, 74);
            this.lblNewHabitsValue.TabIndex = 1;
            this.lblNewHabitsValue.Text = "➕ 3";
            // 
            // lblNewHabitsSub
            // 
            this.lblNewHabitsSub.AutoSize = true;
            this.lblNewHabitsSub.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblNewHabitsSub.ForeColor = System.Drawing.Color.Gray;
            this.lblNewHabitsSub.Location = new System.Drawing.Point(14, 84);
            this.lblNewHabitsSub.Name = "lblNewHabitsSub";
            this.lblNewHabitsSub.Size = new System.Drawing.Size(65, 21);
            this.lblNewHabitsSub.TabIndex = 2;
            this.lblNewHabitsSub.Text = "3 habits";
            // 
            // panelChartOuter
            // 
            this.panelChartOuter.BackColor = System.Drawing.Color.White;
            this.panelChartOuter.Controls.Add(this.lblChartTitle);
            this.panelChartOuter.Controls.Add(this.picChart);
            this.panelChartOuter.Location = new System.Drawing.Point(18, 190);
            this.panelChartOuter.Name = "panelChartOuter";
            this.panelChartOuter.Size = new System.Drawing.Size(625, 448);
            this.panelChartOuter.TabIndex = 4;
            this.panelChartOuter.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintCard);
            // 
            // lblChartTitle
            // 
            this.lblChartTitle.AutoSize = true;
            this.lblChartTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblChartTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(95)))));
            this.lblChartTitle.Location = new System.Drawing.Point(14, 12);
            this.lblChartTitle.Name = "lblChartTitle";
            this.lblChartTitle.Size = new System.Drawing.Size(275, 28);
            this.lblChartTitle.TabIndex = 0;
            this.lblChartTitle.Text = "DAILY COMPLETION TREND";
            // 
            // picChart
            // 
            this.picChart.BackColor = System.Drawing.Color.White;
            this.picChart.Location = new System.Drawing.Point(10, 44);
            this.picChart.Name = "picChart";
            this.picChart.Size = new System.Drawing.Size(603, 386);
            this.picChart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picChart.TabIndex = 1;
            this.picChart.TabStop = false;
            // 
            // panelRatesOuter
            // 
            this.panelRatesOuter.BackColor = System.Drawing.Color.White;
            this.panelRatesOuter.Controls.Add(this.lblRatesTitle);
            this.panelRatesOuter.Controls.Add(this.lblColLeft);
            this.panelRatesOuter.Controls.Add(this.lblColRight);
            this.panelRatesOuter.Controls.Add(this.panelHabitRows);
            this.panelRatesOuter.Location = new System.Drawing.Point(658, 190);
            this.panelRatesOuter.Name = "panelRatesOuter";
            this.panelRatesOuter.Size = new System.Drawing.Size(358, 448);
            this.panelRatesOuter.TabIndex = 5;
            this.panelRatesOuter.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintCard);
            // 
            // lblRatesTitle
            // 
            this.lblRatesTitle.AutoSize = true;
            this.lblRatesTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRatesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(95)))));
            this.lblRatesTitle.Location = new System.Drawing.Point(14, 12);
            this.lblRatesTitle.Name = "lblRatesTitle";
            this.lblRatesTitle.Size = new System.Drawing.Size(225, 28);
            this.lblRatesTitle.TabIndex = 0;
            this.lblRatesTitle.Text = "HABIT SUCCESS RATES";
            // 
            // lblColLeft
            // 
            this.lblColLeft.AutoSize = true;
            this.lblColLeft.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblColLeft.ForeColor = System.Drawing.Color.Gray;
            this.lblColLeft.Location = new System.Drawing.Point(48, 44);
            this.lblColLeft.Name = "lblColLeft";
            this.lblColLeft.Size = new System.Drawing.Size(55, 20);
            this.lblColLeft.TabIndex = 1;
            this.lblColLeft.Text = "HABIT";
            // 
            // lblColRight
            // 
            this.lblColRight.AutoSize = true;
            this.lblColRight.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblColRight.ForeColor = System.Drawing.Color.Gray;
            this.lblColRight.Location = new System.Drawing.Point(252, 44);
            this.lblColRight.Name = "lblColRight";
            this.lblColRight.Size = new System.Drawing.Size(105, 20);
            this.lblColRight.TabIndex = 2;
            this.lblColRight.Text = "COMPLETION";
            // 
            // panelHabitRows
            // 
            this.panelHabitRows.AutoScroll = true;
            this.panelHabitRows.BackColor = System.Drawing.Color.White;
            this.panelHabitRows.Location = new System.Drawing.Point(4, 66);
            this.panelHabitRows.Name = "panelHabitRows";
            this.panelHabitRows.Size = new System.Drawing.Size(330, 364);
            this.panelHabitRows.TabIndex = 3;
            // 
            // MonthlyReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(1200, 760);
            this.Controls.Add(this.panelContent);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MonthlyReview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monthly Review";
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.panelCardProgress.ResumeLayout(false);
            this.panelCardProgress.PerformLayout();
            this.panelCardStreak.ResumeLayout(false);
            this.panelCardStreak.PerformLayout();
            this.panelCardHabits.ResumeLayout(false);
            this.panelCardHabits.PerformLayout();
            this.panelChartOuter.ResumeLayout(false);
            this.panelChartOuter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picChart)).EndInit();
            this.panelRatesOuter.ResumeLayout(false);
            this.panelRatesOuter.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Panel panelCardProgress;
        private System.Windows.Forms.Label lblMonthTitle;
        private System.Windows.Forms.Label lblProgressValue;
        private System.Windows.Forms.Panel panelCardStreak;
        private System.Windows.Forms.Label lblStreakHeader;
        private System.Windows.Forms.Label lblStreakValue;
        private System.Windows.Forms.Label lblStreakSub;
        private System.Windows.Forms.Panel panelCardHabits;
        private System.Windows.Forms.Label lblNewHabitsHeader;
        private System.Windows.Forms.Label lblNewHabitsValue;
        private System.Windows.Forms.Label lblNewHabitsSub;
        private System.Windows.Forms.Panel panelChartOuter;
        private System.Windows.Forms.Label lblChartTitle;
        private System.Windows.Forms.PictureBox picChart;
        private System.Windows.Forms.Panel panelRatesOuter;
        private System.Windows.Forms.Label lblRatesTitle;
        private System.Windows.Forms.Label lblColLeft;
        private System.Windows.Forms.Label lblColRight;
        private System.Windows.Forms.Panel panelHabitRows;
    }
}