using System;
using System.Data.SqlClient;
using CNPM.DAL;
using System.Drawing;
using System.Windows.Forms;
using CNPM.Models.Courses;

namespace CNPM.Forms.Teacher
{
    public partial class UcCourses : UserControl
    {
        private int TeacherId = 10; // Giá trị này sẽ được truyền từ login
        public UcCourses()
        {
            InitializeComponent();
            LoadCourses();
        }
        private void LoadCourses()
        {
            flowPanelCourses.Controls.Clear();
            string query = "SELECT CourseID, CourseName FROM Courses WHERE TeacherID = @TeacherID";

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TeacherID", TeacherId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int courseId = reader.GetInt32(0);
                        string courseName = reader.GetString(1);

                        Panel pnl = new Panel
                        {
                            Height = 60,
                            Width = flowPanelCourses.Width - 25,
                            BackColor = Color.LightSteelBlue,
                            Margin = new Padding(5),
                            Cursor = Cursors.Hand,
                            Tag = new Course { CourseID = courseId, CourseName = courseName }
                        };

                        Label lbl = new Label
                        {
                            Text = courseName,
                            Dock = DockStyle.Fill,
                            TextAlign = ContentAlignment.MiddleLeft,
                            Padding = new Padding(10),
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            Tag = pnl.Tag
                        };

                        pnl.Click += Course_Click;
                        lbl.Click += Course_Click;

                        pnl.Controls.Add(lbl);
                        flowPanelCourses.Controls.Add(pnl);
                    }
                }
            }
        }

        private void Course_Click(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            Course course = ctrl?.Tag as Course;
            if (course == null) return;

            panelCourseDetail.Controls.Clear();
            var detail = new UcCourseDetail
            {
                Dock = DockStyle.Fill
            };
            detail.CurrentCourse = course;
            panelCourseDetail.Controls.Add(detail);
        }
    }
}
