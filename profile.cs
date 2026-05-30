using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using Daily_Tracker_ver2.Helpers;

namespace Daily_Tracker_ver2
{
    public partial class profile : Form
    {
        // Holds the new picture bytes chosen this session (null = unchanged).
        private byte[] _pendingPictureBytes = null;

        public profile()
        {
            InitializeComponent();
            this.Load += Profile_Load;
        }

        // ── Load ─────────────────────────────────────────────────────────────
        private void Profile_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);
            if (!SessionManager.IsLoggedIn) return;

            try
            {
                var data = DatabaseHelper.LoadProfile(SessionManager.UserId);
                textBox1.Text = data.FullName;
                textBox2.Text = data.Email;
                textBox3.Text = data.Occupation;
                textBox4.Text = data.AboutSelf;

                // Load profile picture – fall back to default avatar if none stored
                if (data.ProfilePicture != null && data.ProfilePicture.Length > 0)
                    SetAvatar(data.ProfilePicture);
                else
                    SetDefaultAvatar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load profile: " + ex.Message,
                    "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SetDefaultAvatar();
            }
        }

        // ── Change Photo button ───────────────────────────────────────────────
        private void btnChangePicture_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Choose a profile picture";
                dlg.Filter = "Image files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All files|*.*";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {
                    // Read the file into memory and display it
                    _pendingPictureBytes = File.ReadAllBytes(dlg.FileName);
                    SetAvatar(_pendingPictureBytes);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not load image: " + ex.Message,
                        "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // ── Save button ───────────────────────────────────────────────────────
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!SessionManager.IsLoggedIn)
            {
                MessageBox.Show("Not logged in.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var data = new DatabaseHelper.UserProfile
                {
                    FullName = textBox1.Text.Trim(),
                    Email = textBox2.Text.Trim(),
                    Occupation = textBox3.Text.Trim(),
                    AboutSelf = textBox4.Text.Trim(),
                    // Only pass bytes if the user chose a new picture this session.
                    // Passing null tells SaveProfile to leave the existing DB value alone.
                    ProfilePicture = _pendingPictureBytes,
                };

                DatabaseHelper.SaveProfile(SessionManager.UserId, data);

                if (!string.IsNullOrEmpty(data.FullName))
                    SessionManager.FullName = data.FullName;

                // Clear the pending flag now that it's been persisted
                _pendingPictureBytes = null;

                MessageBox.Show("Profile saved successfully!",
                    "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save profile: " + ex.Message,
                    "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ── Back ──────────────────────────────────────────────────────────────
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        /// <summary>Display raw image bytes in the avatar PictureBox.</summary>
        private void SetAvatar(byte[] bytes)
        {
            try
            {
                using (var ms = new MemoryStream(bytes))
                    picAvatar.Image = Image.FromStream(ms);
            }
            catch
            {
                SetDefaultAvatar();
            }
        }

        /// <summary>
        /// Draw a simple default avatar (circle with a person silhouette)
        /// so the PictureBox is never blank.
        /// </summary>
        private void SetDefaultAvatar()
        {
            int sz = picAvatar.Width > 0 ? picAvatar.Width : 120;
            var bmp = new Bitmap(sz, sz);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                // Background circle
                Color bgColor = Color.FromArgb(100, 149, 237); // cornflower blue
                using (var br = new SolidBrush(bgColor))
                    g.FillEllipse(br, 0, 0, sz - 1, sz - 1);

                // Head circle (white)
                int headR = sz / 5;
                int headX = sz / 2 - headR;
                int headY = sz / 4 - headR / 2;
                using (var br = new SolidBrush(Color.White))
                    g.FillEllipse(br, headX, headY, headR * 2, headR * 2);

                // Body arc (white)
                int bodyW = (int)(sz * 0.55f);
                int bodyH = (int)(sz * 0.35f);
                int bodyX = sz / 2 - bodyW / 2;
                int bodyY = (int)(sz * 0.52f);
                using (var br = new SolidBrush(Color.White))
                    g.FillEllipse(br, bodyX, bodyY, bodyW, bodyH);

                // Clip anything below the outer circle so the body doesn't spill out
                using (var clipPath = new GraphicsPath())
                {
                    clipPath.AddEllipse(0, 0, sz - 1, sz - 1);
                    g.SetClip(clipPath);
                }

                // Border circle
                using (var pen = new Pen(Color.FromArgb(180, 200, 240), 2))
                    g.DrawEllipse(pen, 1, 1, sz - 3, sz - 3);
            }
            picAvatar.Image = bmp;
        }

        // ── Designer stub handlers ────────────────────────────────────────────
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void groupBox1_Enter(object sender, EventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}