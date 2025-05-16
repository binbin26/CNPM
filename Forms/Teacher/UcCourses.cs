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
            List<string> courses = new List<string> { "Lập trình C#", "Cơ sở dữ liệu", "Phân tích hệ thống" };

            flowPanelCourses.Controls.Clear();

            foreach (var course in courses)
            {
                Panel pnl = new Panel
                {
                    Height = 60,
                    Width = flowPanelCourses.Width - 25,
                    BackColor = Color.LightGray,
                    Margin = new Padding(5),
                    Cursor = Cursors.Hand,
                    Tag = course,
                };

                Label lbl = new Label
                {
                    Text = course,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(10),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Tag = course,
                };

                pnl.Click += Course_Click;
                lbl.Click += Course_Click;

                pnl.Controls.Add(lbl);
                flowPanelCourses.Controls.Add(pnl);
            }
        }

        private void Course_Click(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            string courseName = ctrl?.Tag as string ?? ctrl?.Parent?.Tag as string;

            if (string.IsNullOrEmpty(courseName))
            {
                MessageBox.Show("Không lấy được tên khóa học.");
                return;
            }

            panelCourseDetail.Controls.Clear();

            var courseDetail = new UcCourseDetail();
            courseDetail.Dock = DockStyle.Fill;
            courseDetail.CourseName = courseName; // truyền tên khóa học lấy được
            panelCourseDetail.Controls.Add(courseDetail);
        }
    }
}
