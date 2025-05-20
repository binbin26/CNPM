using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using CNPM.DAL;

namespace CNPM.Forms.Teacher
{
    public partial class FormViewStudentQuiz : Form
    {
        private int AssignmentID, StudentID;

        public FormViewStudentQuiz(int aid, int sid)
        {
            InitializeComponent();
            AssignmentID = aid;
            StudentID = sid;
            LoadData();
        }

        private void LoadData()
        {
            var questions = new List<Question>();
            var answers = new Dictionary<int, string>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                var q1 = "SELECT QuestionID, QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectAnswer FROM Questions WHERE AssignmentID = @AID";
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

                var q2 = "SELECT QuestionID, SelectedAnswer FROM StudentAnswers WHERE AssignmentID = @AID AND StudentID = @SID";
                using (var cmd = new SqlCommand(q2, conn))
                {
                    cmd.Parameters.AddWithValue("@AID", AssignmentID);
                    cmd.Parameters.AddWithValue("@SID", StudentID);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            answers[reader.GetInt32(0)] = reader.GetString(1);
                        }
                    }
                }
            }

            RenderQuiz(questions, answers);
        }

        private void RenderQuiz(List<Question> questions, Dictionary<int, string> answers)
        {
            flowPanelQuestions.Controls.Clear();
            int index = 1;
            foreach (var q in questions)
            {
                var lbl = new Label
                {
                    Text = $"Câu {index++}: {q.QuestionText}\nA. {q.OptionA}    B. {q.OptionB}\nC. {q.OptionC}    D. {q.OptionD}\nĐáp án đúng: {q.CorrectAnswer}\nSinh viên chọn: {(answers.ContainsKey(q.QuestionID) ? answers[q.QuestionID] : "[Chưa chọn]")}",
                    AutoSize = true,
                    Padding = new Padding(10)
                };
                flowPanelQuestions.Controls.Add(lbl);
            }
        }
    }
}