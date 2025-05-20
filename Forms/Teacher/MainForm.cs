using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CNPM.BLL;
using CNPM.Models.Courses;
using CNPM.Models.Users;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CNPM.Forms.Teacher
{
    public partial class MainForm : Form
    {
        private List<Course> _courses;
        private int _userId;
        private readonly string _username;
        public MainForm(List<Course> courses, int userId, string username)
        {
            _courses = courses;
            _userId = userId;
            InitializeComponent();
            _username = username;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            /*var courseTab = new UcCourses();
            courseTab.Dock = DockStyle.Fill;
            panelContent.Controls.Add(courseTab);*/

        }

        private void btnCourses_Click(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();
            var courseTab = new UcCourses();
            courseTab.Dock = DockStyle.Fill;
            panelContent.Controls.Add(courseTab);
        }

        private void btnSubmissions_Click(object sender, EventArgs e)
        {
            panelContent.Controls.Clear();
            var submissionsTab = new UcSubmissions();
            submissionsTab.Dock = DockStyle.Fill;
            panelContent.Controls.Add(submissionsTab);
        }

        private void btnThongke_Click(object sender, EventArgs e)
        {
            var frm = new SelectCourseForm(_courses, _username);
            frm.ShowDialog();
        }
    }
}
