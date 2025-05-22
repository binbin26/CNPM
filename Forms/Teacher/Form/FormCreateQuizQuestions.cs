using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using CNPM.Models.Assignments;

namespace CNPM.Forms.Teacher
{
    public partial class FormCreateQuizQuestions : Form
    {
        private int Total, Index = 0;
        private readonly List<Question> Questions = new List<Question>();
        private readonly int TeacherID, CourseID, SessionID;
        public float PassScore { get; set; } = 5.0f;
        public int MaxAttempts { get; set; } = 1;

        public FormCreateQuizQuestions(int total, int teacherID, int courseId, int sessionId)
        {
            InitializeComponent();
            Total = total;
            TeacherID = teacherID;
            CourseID = courseId;
            SessionID = sessionId;
            LoadNext();
        }

        private void LoadNext()
        {
            if (Index == Total)
            {
                SaveToDatabase();
                MessageBox.Show("Tạo bài tập trắc nghiệm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            panelMain.Controls.Clear();
            var questionControl = new UcQuestionCreator();
            questionControl.QuestionSubmitted += (q) =>
            {
                Questions.Add(q);
                Index++;
                LoadNext();
            };
            questionControl.Dock = DockStyle.Fill;
            panelMain.Controls.Add(questionControl);
        }

        private void SaveToDatabase()
        {
            MessageBox.Show($"DEBUG →  TeacherID: {TeacherID}, CourseID: {CourseID}");
            MessageBox.Show($"DEBUG → SessionID = {SessionID}");
            using (var conn = DAL.DatabaseHelper.GetConnection())
            {
                conn.Open();

                // Tạo bản ghi trong Assignments và lấy AssignmentID
                string insertAssignment = @"
                    INSERT INTO Assignments (CourseID, SessionID, Title, CreatedBy, DueDate)
                    OUTPUT INSERTED.AssignmentID
                    VALUES (@CID, @SessionID, @Title, @CreatedBy, @DueDate)";

                int assignmentId;
                using (var cmd = new SqlCommand(insertAssignment, conn))
                {
                    cmd.Parameters.AddWithValue("@CID", CourseID);
                    cmd.Parameters.AddWithValue("@SessionID", SessionID);
                    cmd.Parameters.AddWithValue("@Title", "Bài tập trắc nghiệm");
                    cmd.Parameters.AddWithValue("@CreatedBy", TeacherID);
                    cmd.Parameters.AddWithValue("@DueDate", DateTime.Now);
                    assignmentId = (int)cmd.ExecuteScalar();
                }

                // Lưu cấu hình bài tập trắc nghiệm
                string insertMC = @"
                    INSERT INTO AssignmentMC (AssignmentID, QuestionCount, MaxAttempts, PassScore)
                    VALUES (@AID, @Count, @Max, @Pass)";
                using (var cmd = new SqlCommand(insertMC, conn))
                {
                    cmd.Parameters.AddWithValue("@AID", assignmentId);
                    cmd.Parameters.AddWithValue("@Count", Questions.Count);
                    cmd.Parameters.AddWithValue("@Max", MaxAttempts);
                    cmd.Parameters.AddWithValue("@Pass", PassScore);
                    cmd.ExecuteNonQuery();
                }

                // Lưu danh sách câu hỏi
                string insertQuestion = @"
                    INSERT INTO Questions 
                    (QuestionID, AssignmentID, QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectAnswer)
                    VALUES 
                    (@QID, @AID, @Q, @A, @B, @C, @D, @Ans)";

                int nextQuestionID = 1;
                using (var getMaxCmd = new SqlCommand("SELECT ISNULL(MAX(QuestionID), 0) + 1 FROM Questions", conn))
                {
                    nextQuestionID = (int)getMaxCmd.ExecuteScalar();
                }

                foreach (var q in Questions)
                {
                    using (var cmd = new SqlCommand(insertQuestion, conn))
                    {
                        cmd.Parameters.AddWithValue("@QID", nextQuestionID++);
                        cmd.Parameters.AddWithValue("@AID", assignmentId);
                        cmd.Parameters.AddWithValue("@Q", q.QuestionText);
                        cmd.Parameters.AddWithValue("@A", q.OptionA);
                        cmd.Parameters.AddWithValue("@B", q.OptionB);
                        cmd.Parameters.AddWithValue("@C", q.OptionC);
                        cmd.Parameters.AddWithValue("@D", q.OptionD);
                        cmd.Parameters.AddWithValue("@Ans", q.CorrectAnswer);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}