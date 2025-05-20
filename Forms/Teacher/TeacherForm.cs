using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class TeacherForm : Form
    {
        private int TeacherID;
        public TeacherForm(int teacherId)
        {
            InitializeComponent();
            TeacherID = teacherId;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            btnCourses.PerformClick();
        }

        private void btnCourses_Click(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();
            var courseTab = new UcCourses(TeacherID);
            courseTab.Dock = DockStyle.Fill;
            panelContent.Controls.Add(courseTab);
        }
        private void btnProfile_Click(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();
            var profile = new UcProfile(TeacherID);
            profile.Dock = DockStyle.Fill;
            panelContent.Controls.Add(profile);
        }
        private void btnSubmissions_Click(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();
            var submissionsTab = new UcSubmissions(TeacherID);
            submissionsTab.Dock = DockStyle.Fill;
            panelContent.Controls.Add(submissionsTab);
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Optional: lưu thay đổi nếu cần

            this.Hide();
            var loginForm = new CNPM.Forms.Auth.LoginForm();
            loginForm.Show();
            this.Close(); // hoặc loginForm.ShowDialog(); tùy cách bạn mở MainForm
        }
    }
}