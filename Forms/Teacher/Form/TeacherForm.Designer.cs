using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace CNPM.Forms.Teacher
{
    partial class TeacherForm
    {
        private Panel panelTabs;
        private Panel panelContent;
        private IconButton btnCourses;
        private IconButton btnSubmissions;
        private IconButton btnProfile;
        private IconButton btnLogout;

        private void InitializeComponent()
        {
            this.panelTabs = new Panel();
            this.panelContent = new Panel();
            this.btnCourses = new IconButton();
            this.btnSubmissions = new IconButton();
            this.btnProfile = new IconButton();
            this.btnLogout = new IconButton();

            // Form
            this.SuspendLayout();
            this.ClientSize = new Size(1200, 700);
            this.Text = "Hệ thống quản lý học tập - Giảng viên";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Load += new System.EventHandler(this.TeacherForm_Load);

            // panelTabs
            this.panelTabs.Dock = DockStyle.Left;
            this.panelTabs.Width = 220;
            this.panelTabs.BackColor = Color.FromArgb(30, 30, 60);
            this.panelTabs.Padding = new Padding(0, 100, 0, 0);

            // panelContent
            this.panelContent.Dock = DockStyle.Fill;
            this.panelContent.BackColor = Color.WhiteSmoke;

            // button common style
            void StyleButton(IconButton btn, string text, IconChar icon)
            {
                btn.Dock = DockStyle.Top;
                btn.Height = 70;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 45, 80); // Hover effect
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(25, 25, 50); // Click effect
                btn.Cursor = Cursors.Hand;
                btn.ForeColor = Color.White;
                btn.IconChar = icon;
                btn.IconColor = Color.White;
                btn.IconFont = IconFont.Auto;
                btn.IconSize = 32;
                btn.Text = text;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                btn.Padding = new Padding(15, 0, 0, 0);
            }

            // btnCourses
            StyleButton(btnCourses, "Khóa học", IconChar.Book);
            btnCourses.Click += btnCourses_Click;

            // btnSubmissions
            StyleButton(btnSubmissions, "Bài nộp", IconChar.FileAlt);
            btnSubmissions.Click += btnSubmissions_Click;

            // btnProfile
            StyleButton(btnProfile, "Thông tin cá nhân", IconChar.User);
            btnProfile.Click += btnProfile_Click;

            // btnLogout
            StyleButton(btnLogout, "Đăng xuất", IconChar.SignOutAlt);
            btnLogout.Click += btnLogout_Click;

            // Add buttons
            panelTabs.Controls.Add(btnLogout);
            panelTabs.Controls.Add(btnProfile);
            panelTabs.Controls.Add(btnSubmissions);
            panelTabs.Controls.Add(btnCourses);

            // Add to form
            this.Controls.Add(panelContent);
            this.Controls.Add(panelTabs);
            this.ResumeLayout(false);
        }
    }
}