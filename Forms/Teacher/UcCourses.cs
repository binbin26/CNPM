using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using CNPM.DAL;
using CNPM.Forms.Admin;
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

                        Panel panel = new Panel
                        {
                            Height = 60,
                            Width = flowPanelCourses.Width - 30,
                            BackColor = Color.LightSteelBlue,
                            Margin = new Padding(5),
                            Cursor = Cursors.Hand,
                            Tag = new Course { CourseID = courseId, CourseName = courseName, TeacherID = TeacherId }
                        };

                        Label label = new Label
                        {
                            Text = courseName,
                            Dock = DockStyle.Fill,
                            TextAlign = ContentAlignment.MiddleLeft,
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            Padding = new Padding(10)
                        };

                        panel.Controls.Add(label);
                        panel.Click += Course_Click;
                        label.Click += Course_Click;

                        flowPanelCourses.Controls.Add(panel);
                    }
                }
            }
        }

        private void Course_Click(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control?.Tag is Course course)
            {
                var detail = new UcCourseDetail(course)
                {
                    Dock = DockStyle.Fill
                };

                var parentForm = this.FindForm();
                if (parentForm is TeacherForm teacherForm)
                {
                    teacherForm.Controls["panelContent"].Controls.Clear();
                    teacherForm.Controls["panelContent"].Controls.Add(detail);
                }
            }
            else if (control?.Parent?.Tag is Course parentCourse)
            {
                var detail = new UcCourseDetail(parentCourse)
                {
                    Dock = DockStyle.Fill
                };

                var parentForm = this.FindForm();
                if (parentForm is TeacherForm teacherForm)
                {
                    teacherForm.Controls["panelContent"].Controls.Clear();
                    teacherForm.Controls["panelContent"].Controls.Add(detail);
                }
            }
        }
    }
}
