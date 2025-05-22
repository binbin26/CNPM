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
                Tag = new Course { TeacherID = TeacherId, CourseID = courseId, CourseName = courseName }
            };

            // Bo góc
            panel.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                var rect = panel.ClientRectangle;
                rect.Inflate(-1, -1);
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddArc(rect.X, rect.Y, 15, 15, 180, 90);
                    path.AddArc(rect.Right - 15, rect.Y, 15, 15, 270, 90);
                    path.AddArc(rect.Right - 15, rect.Bottom - 15, 15, 15, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - 15, 15, 15, 90, 90);
                    path.CloseFigure();
                    using (var brush = new SolidBrush(Color.White))
                    {
                        g.FillPath(brush, path);
                    }
                    using (var borderPen = new Pen(Color.LightGray, 1))
                    {
                        g.DrawPath(borderPen, path);
                    }
                }
            };

            // Hiệu ứng hover
            panel.MouseEnter += (s, e) => panel.BackColor = Color.FromArgb(245, 250, 255);
            panel.MouseLeave += (s, e) => panel.BackColor = Color.White;

            Label label = new Label
            {
                Text = courseName,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Padding = new Padding(15, 0, 0, 0),
                ForeColor = Color.FromArgb(30, 30, 30)
            };
            label.Click += Course_Click;
            panel.Controls.Add(label);
            panel.Click += Course_Click;
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