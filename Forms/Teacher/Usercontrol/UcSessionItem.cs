using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CNPM.DAL;

namespace CNPM.Forms.Teacher
{
    public partial class UcSessionItem : UserControl
    {
        private int SessionID;
        private int CourseID;
        private string Title;
        private int TeacherID;

        public UcSessionItem(int sessionId, int courseId, string title, int teacherID)
        {
            InitializeComponent();
            SessionID = sessionId;
            CourseID = courseId;
            Title = title;
            TeacherID = teacherID;
            lblSessionTitle.Text = title;
            // 👉 Thiết kế nút hiện đại
            StyleModernButton(btnAttachFile, Color.FromArgb(100, 149, 237));
            StyleModernButton(btnCreateAssignment, Color.FromArgb(72, 201, 176));
            LoadAssignments();
        }

        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = dialog.FileName;
                string fileName = Path.GetFileName(filePath);
                string uploadsDir = Path.Combine(Application.StartupPath, "Uploads");

                Directory.CreateDirectory(uploadsDir);
                string destPath = Path.Combine(uploadsDir, fileName);
                File.Copy(filePath, destPath, true);

                string query = "INSERT INTO CourseDocuments (CourseID, Title, FilePath, UploadDate) " +
                               "VALUES (@CourseID, @Title, @Path, GETDATE())";

                using (var conn = DatabaseHelper.GetConnection())
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", CourseID);
                    cmd.Parameters.AddWithValue("@Title", fileName);
                    cmd.Parameters.AddWithValue("@Path", $"Uploads/{fileName}");
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Đính kèm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCreateAssignment_Click(object sender, EventArgs e)
        {
            FormChooseAssignmentType choose = new FormChooseAssignmentType(SessionID, CourseID, TeacherID);
            choose.FormClosed += (s, args) => LoadAssignments();
            choose.ShowDialog();
        }
        private void LoadAssignments()
        {
            flowPanelAssignments.Controls.Remove(btnCreateAssignment);
            flowPanelAssignments.Controls.Remove(btnAttachFile);
            // Xóa toàn bộ và thêm lại tiêu đề
            flowPanelAssignments.Controls.Clear();
            int totalHeight = 0;
            string query = "SELECT AssignmentID, Title FROM Assignments WHERE SessionID = @SID";
            using (var conn = DAL.DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@SID", SessionID);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int assignmentId = reader.GetInt32(0);
                        string title = reader.GetString(1);

                        Label lbl = new Label
                        {
                            Text = $"📝 {title}",
                            AutoSize = false,
                            Width = flowPanelAssignments.Width - 40,
                            Height = 40,
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            BackColor = Color.LightGray,
                            ForeColor = Color.Black,
                            Padding = new Padding(10),
                            Margin = new Padding(5),
                            Cursor = Cursors.Hand
                        };

                        // Mở FormViewQuestions khi click
                        lbl.Click += (s, e) =>
                        {
                            var questions = LoadQuestionsFromDatabase(assignmentId);
                            var view = new FormViewQuestions(questions);
                            view.ShowDialog();
                        };

                        flowPanelAssignments.Controls.Add(lbl);
                    }
                }
            }
            flowPanelAssignments.Controls.Add(btnAttachFile);
            flowPanelAssignments.Controls.Add(btnCreateAssignment);
            btnAttachFile.Click -= btnAttachFile_Click;
            btnAttachFile.Click += btnAttachFile_Click;
            btnCreateAssignment.Click -= btnCreateAssignment_Click;
            btnCreateAssignment.Click += btnCreateAssignment_Click;
        }
        private List<Question> LoadQuestionsFromDatabase(int assignmentId)
        {
            List<Question> questions = new List<Question>();
            string query = "SELECT QuestionID, QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectAnswer FROM Questions WHERE AssignmentID = @AID";

            using (var conn = DAL.DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@AID", assignmentId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        questions.Add(new Question
                        {
                            QuestionID = reader.GetInt32(0),
                            QuestionText = reader.GetString(1),
                            OptionA = reader.GetString(2),
                            OptionB = reader.GetString(3),
                            OptionC = reader.GetString(4),
                            OptionD = reader.GetString(5),
                            CorrectAnswer = reader.GetString(6)
                        });
                    }
                }
            }
            return questions;
        }
        private void StyleModernButton(Button button, Color backColor)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = backColor;
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button.Padding = new Padding(6);
            button.Margin = new Padding(8, 5, 8, 5);
            button.AutoSize = true;
        }
    }
}
