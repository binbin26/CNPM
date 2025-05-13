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
    public partial class UcCourses : UserControl
    {
        public UcCourses()
        {
            InitializeComponent();
            LoadCourses();
        }

        private void LoadCourses()
        {
            // Demo data
            List<string> courses = new List<string> { "Lập trình C#", "Cơ sở dữ liệu", "Phân tích hệ thống" };

            int index = 0;
            foreach (var course in courses)
            {
                Button btn = new Button();
                btn.Text = course;
                btn.Width = flowPanelCourses.Width - 25;
                btn.Height = 60;
                btn.Tag = index++;
                btn.Click += Course_Click;
                flowPanelCourses.Controls.Add(btn);
            }
        }

        private void Course_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            panelCourseDetail.Controls.Clear();

            var courseDetail = new UcCourseDetail();
            courseDetail.Dock = DockStyle.Fill;
            panelCourseDetail.Controls.Add(courseDetail);
        }
    }
}
