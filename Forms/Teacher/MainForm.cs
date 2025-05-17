using System;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
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
    }
}
