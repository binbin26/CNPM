using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CNPM.DAL;
using CNPM.Models;
using CNPM.Models.Courses;

namespace CNPM.Forms.Teacher
{
    public partial class UcSubmissions : UserControl
    {
        private int TeacherID;

        public UcSubmissions(int teacherId)
        {
            InitializeComponent();
            TeacherID = teacherId;
            LoadCourses();
        }

        private void LoadCourses()
        {
            flowPanelCourses.Controls.Clear();
            string query = "SELECT CourseID, CourseName FROM Courses WHERE TeacherID = @TeacherID";

            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TeacherID", TeacherID);
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
                            Tag = new Course { CourseID = courseId, CourseName = courseName, TeacherID = TeacherID }
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
            if (control?.Tag is int cid)
            {
                LoadAssignments(cid);
            }
            else if (control?.Parent?.Tag is int pid)
            {
                LoadAssignments(pid);
            }
        }

        private void LoadAssignments(int courseId)
        {
            flowPanelAssignments.Controls.Clear();
            flowPanelSubmissions.Controls.Clear();

            string query = "SELECT AssignmentID, Title FROM Assignments WHERE CourseID = @CourseID";
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CourseID", courseId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int aid = reader.GetInt32(0);
                        string title = reader.GetString(1);

                        var btn = new Button
                        {
                            Text = title,
                            Height = 40,
                            Width = 300,
                            Tag = aid,
                            Margin = new Padding(5)
                        };
                        btn.Click += (s, e) => LoadSubmissions(aid);
                        flowPanelAssignments.Controls.Add(btn);
                    }
                }
            }
        }

        private void LoadSubmissions(int assignmentId)
        {
            flowPanelSubmissions.Controls.Clear();
            string query = @"SELECT ss.StudentID, u.FullName, ss.FilePath
                             FROM StudentSubmissions ss
                             JOIN Users u ON ss.StudentID = u.UserID
                             WHERE ss.AssignmentID = @AID";

            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@AID", assignmentId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int sid = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string path = reader.IsDBNull(2) ? null : reader.GetString(2);

                        var btn = new Button
                        {
                            Text = name,
                            Height = 35,
                            Width = 280,
                            Tag = new SubmissionModel { StudentID = sid, FilePath = path, AssignmentID = assignmentId }
                        };
                        btn.Click += ViewSubmission_Click;
                        flowPanelSubmissions.Controls.Add(btn);
                    }
                }
            }
        }

        private void ViewSubmission_Click(object sender, EventArgs e)
        {
            var model = (SubmissionModel)((Button)sender).Tag;

            if (!string.IsNullOrEmpty(model.FilePath))
            {
                SaveFileDialog dialog = new SaveFileDialog
                {
                    FileName = Path.GetFileName(model.FilePath)
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(model.FilePath, dialog.FileName, true);
                    MessageBox.Show("Tải file thành công!");
                }
            }
            else
            {
                FormViewStudentQuiz quiz = new FormViewStudentQuiz(model.AssignmentID, model.StudentID);
                quiz.ShowDialog();
            }
        }
    }

    public class SubmissionModel
    {
        public int AssignmentID { get; set; }
        public int StudentID { get; set; }
        public string FilePath { get; set; }
    }
}
