using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class FormCreateQuizQuestions : Form
    {
        private int Total;
        private int Index = 0;
        private readonly List<Question> Questions = new List<Question>();
        private readonly int SessionID, CourseID, TeacherID;

        public FormCreateQuizQuestions(int total, int sessionId, int courseId, int teacherID)
        {
            InitializeComponent();
            Total = total;
            SessionID = sessionId;
            CourseID = courseId;
            TeacherID = teacherID;
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
            MessageBox.Show($"DEBUG → SessionID = {SessionID}");
            MessageBox.Show($"DEBUG → CourseID: {CourseID}, TeacherID: {TeacherID}");
            using (var conn = DAL.DatabaseHelper.GetConnection())
            {
                conn.Open();
                // Lấy ID câu hỏi tiếp theo
                int nextQuestionID = 1;
                string getMaxIdSql = "SELECT ISNULL(MAX(QuestionID), 0) + 1 FROM Questions";
                using (var getMaxCmd = new SqlCommand(getMaxIdSql, conn))
                {
                    nextQuestionID = (int)getMaxCmd.ExecuteScalar();
                }
                // Tạo bài tập trắc nghiệm
                string insertAssignment = @"
                       INSERT INTO Assignments (CourseID, SessionID, Title, CreatedBy, DueDate)
                       OUTPUT INSERTED.AssignmentID
                       VALUES (@CID, @SID, @Title, @CreatedBy, @DueDate)";

                int assignmentId;
                using (var cmd = new System.Data.SqlClient.SqlCommand(insertAssignment, conn))
                {
                    cmd.Parameters.AddWithValue("@CID", CourseID);
                    cmd.Parameters.AddWithValue("@SID", SessionID);
                    cmd.Parameters.AddWithValue("@Title", "Bài tập trắc nghiệm");
                    cmd.Parameters.AddWithValue("@CreatedBy", TeacherID);
                    cmd.Parameters.AddWithValue("@DueDate", DateTime.Now);
                    assignmentId = (int)cmd.ExecuteScalar();
                }

                string insertQuestion = @"
                        INSERT INTO Questions 
                        (QuestionID, AssignmentID, QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectAnswer)
                        VALUES 
                        (@QID, @AID, @Q, @A, @B, @C, @D, @Ans)";

                foreach (var q in Questions)
                {
                    using (var cmd = new System.Data.SqlClient.SqlCommand(insertQuestion, conn))
                    {
                        cmd.Parameters.AddWithValue("@QID", nextQuestionID);
                        cmd.Parameters.AddWithValue("@AID", assignmentId);
                        cmd.Parameters.AddWithValue("@Q", q.QuestionText);
                        cmd.Parameters.AddWithValue("@A", q.OptionA);
                        cmd.Parameters.AddWithValue("@B", q.OptionB);
                        cmd.Parameters.AddWithValue("@C", q.OptionC);
                        cmd.Parameters.AddWithValue("@D", q.OptionD);
                        cmd.Parameters.AddWithValue("@Ans", q.CorrectAnswer);
                        cmd.ExecuteNonQuery();
                        nextQuestionID++; // Tăng cho câu tiếp theo
                    }
                }
            }
        }
    }
}
