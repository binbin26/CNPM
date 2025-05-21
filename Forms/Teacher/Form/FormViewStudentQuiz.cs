using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using CNPM.DAL;

namespace CNPM.Forms.Teacher
{
    public partial class FormViewStudentQuiz : Form
    {
        private int AssignmentID;
        private int StudentID;

        public FormViewStudentQuiz(int assignmentId, int studentId)
        {
            InitializeComponent();
            AssignmentID = assignmentId;
            StudentID = studentId;
            LoadData();
        }

        private void LoadData()
        {
            List<Question> questions = new List<Question>();
            Dictionary<int, string> studentAnswers = new Dictionary<int, string>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                // Lấy danh sách câu hỏi
                string q1 = "SELECT QuestionID, QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectAnswer " +
                            "FROM Questions WHERE AssignmentID = @AID";
                using (var cmd = new SqlCommand(q1, conn))
                {
                    cmd.Parameters.AddWithValue("@AID", AssignmentID);
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

                // Lấy đáp án sinh viên đã chọn
                string q2 = "SELECT QuestionID, SelectedAnswer FROM StudentAnswers " +
                            "WHERE AssignmentID = @AID AND StudentID = @SID";
                using (var cmd = new SqlCommand(q2, conn))
                {
                    cmd.Parameters.AddWithValue("@AID", AssignmentID);
                    cmd.Parameters.AddWithValue("@SID", StudentID);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            studentAnswers[reader.GetInt32(0)] = reader.GetString(1);
                        }
                    }
                }
            }

            RenderQuiz(questions, studentAnswers);
        }

        private void RenderQuiz(List<Question> questions, Dictionary<int, string> studentAnswers)
        {
            flowPanelQuestions.Controls.Clear();
            int index = 1;

            foreach (var q in questions)
            {
                string studentAnswer = studentAnswers.ContainsKey(q.QuestionID)
                    ? studentAnswers[q.QuestionID]
                    : "[Chưa chọn]";

                Label lbl = new Label
                {
                    AutoSize = true,
                    Padding = new Padding(10),
                    Font = new Font("Segoe UI", 10),
                    Text = $"Câu {index++}: {q.QuestionText}\n" +
                           $"A. {q.OptionA}\n" +
                           $"B. {q.OptionB}\n" +
                           $"C. {q.OptionC}\n" +
                           $"D. {q.OptionD}\n" +
                           $"✔ Đáp án đúng: {q.CorrectAnswer}\n" +
                           $"🧑 Sinh viên chọn: {studentAnswer}",
                    BackColor = studentAnswer == q.CorrectAnswer ? Color.LightGreen : Color.LightCoral,
                    Margin = new Padding(10)
                };

                flowPanelQuestions.Controls.Add(lbl);
            }
        }
    }
}
