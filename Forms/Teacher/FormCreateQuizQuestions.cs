using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class FormCreateQuizQuestions : Form
    {
        private int Total, Index = 0;
        private List<Question> Questions = new List<Question>();
        private int SessionID, CourseID, Duration;

        public FormCreateQuizQuestions(int count, int duration, int sessionId, int courseId)
        {
            InitializeComponent();
            Total = count;
            Duration = duration;
            SessionID = sessionId;
            CourseID = courseId;
            LoadNext();
        }

        private void LoadNext()
        {
            if (Index == Total)
            {
                SaveAllToDatabase();
                MessageBox.Show("Tạo bài tập trắc nghiệm thành công");
                this.Close();
                return;
            }
            panelMain.Controls.Clear();
            var uc = new UcQuestionCreator();
            uc.QuestionSubmitted += q => { Questions.Add(q); Index++; LoadNext(); };
            uc.Dock = DockStyle.Fill;
            panelMain.Controls.Add(uc);
        }

        private void SaveAllToDatabase()
        {
            using (var conn = DAL.DatabaseHelper.GetConnection())
            {
                conn.Open();
                string insertA = "INSERT INTO Assignments (CourseID, Title, Duration, CreatedAt) OUTPUT INSERTED.AssignmentID VALUES (@CID, @Title, @Dur, GETDATE())";
                int AID;
                using (var cmd = new System.Data.SqlClient.SqlCommand(insertA, conn))
                {
                    cmd.Parameters.AddWithValue("@CID", CourseID);
                    cmd.Parameters.AddWithValue("@Title", "Bài tập trắc nghiệm");
                    cmd.Parameters.AddWithValue("@Dur", Duration);
                    AID = (int)cmd.ExecuteScalar();
                }
                foreach (var q in Questions)
                {
                    string insertQ = "INSERT INTO Questions (AssignmentID, QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectAnswer) VALUES (@AID, @Q, @A, @B, @C, @D, @Ans)";
                    using (var cmd = new System.Data.SqlClient.SqlCommand(insertQ, conn))
                    {
                        cmd.Parameters.AddWithValue("@AID", AID);
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