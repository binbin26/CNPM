using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class LecturerForm : Form
    {
        public LecturerForm()
        {
            InitializeComponent();
        }

        private void panelCourses_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            var uc = new CoursesControl();
            uc.Dock = DockStyle.Fill;
            panelMain.Controls.Add(uc);
        }

        private void panelUpload_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            var uc = new UploadControl();
            uc.Dock = DockStyle.Fill;
            panelMain.Controls.Add(uc);
        }

        private void panelExam_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            var uc = new ExamControl();
            uc.Dock = DockStyle.Fill;
            panelMain.Controls.Add(uc);
        }

        private void panelScoreReport_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            var uc = new ScoreReportControl();
            uc.Dock = DockStyle.Fill;
            panelMain.Controls.Add(uc);
        }
    }
}
