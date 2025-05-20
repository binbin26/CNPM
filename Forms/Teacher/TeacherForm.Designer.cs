using System.Windows.Forms;
using FontAwesome.Sharp;
using System.Drawing;
using System;

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
            this.panelTabs = new System.Windows.Forms.Panel();
            this.btnCourses = new IconButton();
            this.btnSubmissions = new IconButton();
            this.btnProfile = new IconButton();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTabs
            // 
            this.panelTabs.BackColor = System.Drawing.Color.MidnightBlue;
            this.panelTabs.Padding = new Padding(0, 120, 0, 0);
            this.panelTabs.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTabs.Location = new System.Drawing.Point(0, 0);
            this.panelTabs.Name = "panelTabs";
            this.panelTabs.Size = new System.Drawing.Size(200, 600);
            this.panelTabs.TabIndex = 1;
            this.panelTabs.Controls.Add(this.btnLogout);
            this.panelTabs.Controls.Add(this.btnProfile);
            this.panelTabs.Controls.Add(this.btnSubmissions);
            this.panelTabs.Controls.Add(this.btnCourses);
            // 
            // btnSubmissions
            // 
            this.btnSubmissions.Dock = DockStyle.Top;
            this.btnSubmissions.Height = 90;
            this.btnSubmissions.Text = "Bài nộp";
            this.btnSubmissions.IconChar = IconChar.FileAlt;
            this.btnSubmissions.IconColor = Color.White;
            this.btnSubmissions.IconFont = IconFont.Auto;
            this.btnSubmissions.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnSubmissions.ForeColor = Color.White;
            this.btnSubmissions.FlatStyle = FlatStyle.Flat;
            this.btnSubmissions.FlatAppearance.BorderSize = 0;
            this.btnSubmissions.Click += new System.EventHandler(this.btnSubmissions_Click);
            // 
            // btnCourses
            // 
            this.btnCourses.Dock = DockStyle.Top;
            this.btnCourses.Height = 90;
            this.btnCourses.Text = "Khóa học";
            this.btnCourses.IconChar = IconChar.Book;
            this.btnCourses.IconColor = Color.White;
            this.btnCourses.IconFont = IconFont.Auto;
            this.btnCourses.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnCourses.ForeColor = Color.White;
            this.btnCourses.FlatStyle = FlatStyle.Flat;
            this.btnCourses.FlatAppearance.BorderSize = 0;
            this.btnCourses.Click += new System.EventHandler(this.btnCourses_Click);
            // 
            // btnProfile
            //
            this.btnProfile.Dock = DockStyle.Top;
            this.btnProfile.Height = 90;
            this.btnProfile.Text = "Thông tin cá nhân";
            this.btnProfile.IconChar = IconChar.User;
            this.btnProfile.IconColor = Color.White;
            this.btnProfile.IconFont = IconFont.Auto;
            this.btnProfile.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnProfile.ForeColor = Color.White;
            this.btnProfile.FlatStyle = FlatStyle.Flat;
            this.btnProfile.FlatAppearance.BorderSize = 0;
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            //
            // btnLogout
            //
            this.btnLogout = new IconButton();
            this.btnLogout.Dock = DockStyle.Top;
            this.btnLogout.Height = 90;
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.IconChar = IconChar.SignOutAlt;
            this.btnLogout.IconColor = Color.White;
            this.btnLogout.IconFont = IconFont.Auto;
            this.btnLogout.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnLogout.ForeColor = Color.White;
            this.btnLogout.FlatStyle = FlatStyle.Flat;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.Padding = new Padding(10, 0, 0, 0);
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.White;
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(200, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(800, 600);
            this.panelContent.TabIndex = 0;
            // 
            // TeacherForm
            // 
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelTabs);
            this.Name = "TeacherForm";
            this.Text = "Hệ thống quản lý học tập - Giảng viên";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelTabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}