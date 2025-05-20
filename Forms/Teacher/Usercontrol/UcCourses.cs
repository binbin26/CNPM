using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using CNPM.DAL;
using CNPM.Models.Courses;

namespace CNPM.Forms.Teacher
{
    public partial class UcCourses : UserControl
    {
        private int TeacherId;

        public UcCourses(int teacherId)
        {
            InitializeComponent();
            TeacherId = teacherId;
            LoadCourses();
        }

        private void LoadCourses()
        {
            flowPanelCourses.Controls.Clear();
            string query = "SELECT CourseID, CourseName FROM Courses WHERE TeacherID = @TeacherID";

            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TeacherID", TeacherId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int courseId = reader.GetInt32(0);
                        string courseName = reader.GetString(1);

                        var panel = CreateCourseCard(courseId, courseName);
                        flowPanelCourses.Controls.Add(panel);
                    }
                }
            }
        }

        private Panel CreateCourseCard(int courseId, string courseName)
        {
            Panel panel = new Panel
            {
                Height = 80,
                Width = flowPanelCourses.Width - 40,
                BackColor = Color.White,
                Margin = new Padding(10),
                Cursor = Cursors.Hand,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = new Course { CourseID = courseId, CourseName = courseName, TeacherID = TeacherId }
            };

            Label label = new Label
            {
                Text = courseName,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Padding = new Padding(15, 0, 0, 0),
                ForeColor = Color.FromArgb(30, 30, 30)
            };

            panel.Controls.Add(label);
            panel.Click += Course_Click;
            label.Click += Course_Click;

            return panel;
        }

        private void Course_Click(object sender, EventArgs e)
        {
            Control control = sender as Control;
            Course selectedCourse = null;

            if (control?.Tag is Course course)
                selectedCourse = course;
            else if (control?.Parent?.Tag is Course parentCourse)
                selectedCourse = parentCourse;

            if (selectedCourse != null)
            {
                var detail = new UcCourseDetail(selectedCourse);

                // Sửa tại đây:
                if (this.FindForm() is TeacherForm parentForm)
                {
                    parentForm.LoadControl(detail); // ← Dùng phương thức chuẩn
                }
            }
        }
    }
}