using System.Collections.Generic;
using System.Windows.Forms;
using CNPM.Models.Assignments;

namespace CNPM.Forms.Teacher
{
    public partial class FormViewQuestions : Form
    {
        public FormViewQuestions(List<Question> questions)
        {
            InitializeComponent();
            LoadQuestions(questions);
        }

        private void LoadQuestions(List<Question> questions)
        {
            flowLayoutPanel1.Controls.Clear();
            int index = 1;
            foreach (var q in questions)
            {
                Label lbl = new Label
                {
                    AutoSize = true,
                    Padding = new Padding(10),
                    Text = $"Câu {index++}: {q.QuestionText}\n" +
                           $"A. {q.OptionA}\n" +
                           $"B. {q.OptionB}\n" +
                           $"C. {q.OptionC}\n" +
                           $"D. {q.OptionD}\n" +
                           $"Đáp án đúng: {q.CorrectAnswer}",
                    Font = new System.Drawing.Font("Segoe UI", 10),
                    BackColor = System.Drawing.Color.WhiteSmoke,
                    Margin = new Padding(10)
                };

                flowLayoutPanel1.Controls.Add(lbl);
            }
        }
    }
}