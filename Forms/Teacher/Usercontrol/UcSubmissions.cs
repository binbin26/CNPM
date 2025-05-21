using System;
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
                            Height = 70,
                            Width = flowPanelCourses.Width - 30,
                            BackColor = Color.White,
                            Margin = new Padding(10),
                            Cursor = Cursors.Hand,
                            Tag = courseId,
                            BorderStyle = BorderStyle.FixedSingle
                        };

                        Label label = new Label
                        {
                            Text = courseName,
                            Dock = DockStyle.Fill,
                            TextAlign = ContentAlignment.MiddleLeft,
                            Font = new Font("Segoe UI", 11, FontStyle.Bold),
                            Padding = new Padding(15, 0, 0, 0)
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
            int courseId = 0;

            if (sender is Control ctrl)
            {
                if (ctrl.Tag is int id) courseId = id;
                else if (ctrl.Parent?.Tag is int pid) courseId = pid;
            }

            if (courseId != 0)
            {
                LoadAssignments(courseId);
            }
        }

        private void LoadAssignments(int courseId)
        {
            flowPanelAssignments.Controls.Clear();
            flowPanelSubmissions.Controls.Clear();

            string query = "SELECT AssignmentID, Title, AssignmentType FROM Assignments WHERE CourseID = @CourseID";
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
                        string type = reader.IsDBNull(2) ? "Essay" : reader.GetString(2);

                        Button btn = new Button
                        {
                            Text = title + (type == "Quiz" ? " (Trắc nghiệm)" : " (Tự luận)"),
                            Height = 40,
                            Width = 280,
                            Tag = new { AssignmentID = aid, Type = type },
                            Margin = new Padding(5),
                            BackColor = Color.MediumSlateBlue,
                            ForeColor = Color.White,
                            FlatStyle = FlatStyle.Flat
                        };
                        btn.Click += (s, e) => LoadSubmissions((int)((dynamic)btn.Tag).AssignmentID, ((dynamic)btn.Tag).Type);
                        flowPanelAssignments.Controls.Add(btn);
                    }
                }
            }
        }

        private void LoadSubmissions(int assignmentId, string type)
        {
            flowPanelSubmissions.Controls.Clear();
            if (type == "Essay") LoadEssaySubmissions(assignmentId);
            else LoadQuizSubmissions(assignmentId);
        }

        private void LoadEssaySubmissions(int assignmentId)
        {
            string query = @"SELECT ss.StudentID, u.FullName, ss.FilePath, ss.SubmitDate, ss.Score
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
                        string filePath = reader.IsDBNull(2) ? null : reader.GetString(2);
                        DateTime submitDate = reader.GetDateTime(3);
                        int? score = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4);

                        Button btn = new Button
                        {
                            Text = $"{name} - {submitDate:dd/MM/yyyy} - " + (score.HasValue ? $"Điểm: {score}" : "Chưa chấm"),
                            Height = 40,
                            Width = 450,
                            Tag = new SubmissionModel { StudentID = sid, FilePath = filePath, AssignmentID = assignmentId, Score = score },
                            Margin = new Padding(5),
                            FlatStyle = FlatStyle.Flat
                        };
                        btn.Click += ViewEssay_Click;
                        btn.MouseUp += EssayRightClick;
                        flowPanelSubmissions.Controls.Add(btn);
                    }
                }
            }
        }
        private void ViewEssay_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is SubmissionModel model)
            {
                SaveFileDialog dialog = new SaveFileDialog
                {
                    FileName = Path.GetFileName(model.FilePath)
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(model.FilePath, dialog.FileName, true);
                    MessageBox.Show("Tải bài nộp thành công!");
                }
            }
        }

        private void EssayRightClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && sender is Button btn && btn.Tag is SubmissionModel model)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                ToolStripMenuItem gradeItem = new ToolStripMenuItem(model.Score.HasValue ? "Sửa điểm" : "Chấm điểm");
                gradeItem.Click += (s, ev) =>
                {
                    var form = new FormGradeEssay(model.Score);
                    if (form.ShowDialog() == DialogResult.OK && form.Score.HasValue)
                    {
                        using (var conn = DatabaseHelper.GetConnection())
                        using (var cmd = new SqlCommand("UPDATE StudentSubmissions SET Score = @Score WHERE AssignmentID = @AID AND StudentID = @SID", conn))
                        {
                            cmd.Parameters.AddWithValue("@Score", form.Score);
                            cmd.Parameters.AddWithValue("@AID", model.AssignmentID);
                            cmd.Parameters.AddWithValue("@SID", model.StudentID);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        LoadEssaySubmissions(model.AssignmentID); // reload to reflect change
                    }
                };
                menu.Items.Add(gradeItem);
                menu.Show(Cursor.Position);
            }
        }

        private void LoadQuizSubmissions(int assignmentId)
        {
            // TODO: Tự động tính điểm từ bảng Questions và StudentAnswers
            // Sau đó hiển thị tương tự như bài tự luận
        }
        public class SubmissionModel
        {
            public int AssignmentID { get; set; }
            public int StudentID { get; set; }
            public string FilePath { get; set; }
            public int? Score { get; set; }
        }
    }
}
