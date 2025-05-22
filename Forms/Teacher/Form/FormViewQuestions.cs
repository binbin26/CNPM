using System.Collections.Generic;
using System.Drawing;
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
                var panel = new Panel
                {
                    Width = 740,
                    BackColor = Color.White,
                    Margin = new Padding(10),
                    BorderStyle = BorderStyle.FixedSingle
                };

                var lblQuestion = new Label
                {
                    Text = $"Câu {index++}: {q.QuestionText}",
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    Location = new Point(10, 10),
                    AutoSize = true
                };
                panel.Controls.Add(lblQuestion);

                string[] options = { q.OptionA, q.OptionB, q.OptionC, q.OptionD };
                string[] labels = { "A", "B", "C", "D" };
                int currentY = 40;
                for (int i = 0; i < 4; i++)
                {
                    bool isCorrect = labels[i] == q.CorrectAnswer;
                    var lblOption = new Label
                    {
                        Text = $"{labels[i]}. {options[i]}",
                        AutoSize = false,
                        MaximumSize = new Size(700, 0), // 👈 Cho phép wrap dòng
                        Font = new Font("Segoe UI", 10),
                        BackColor = isCorrect ? Color.LightGreen : Color.WhiteSmoke,
                        ForeColor = Color.Black,
                        Padding = new Padding(8),
                        BorderStyle = BorderStyle.FixedSingle,
                        Location = new Point(20, currentY),
                    };
                    lblOption.Height = TextRenderer.MeasureText(lblOption.Text, lblOption.Font, new Size(700, 0), TextFormatFlags.WordBreak).Height + 20;
                    panel.Controls.Add(lblOption);
                    currentY += lblOption.Height + 8; // Cộng thêm padding giữa các ô
                }
                panel.Height = currentY + 20;
                flowLayoutPanel1.Controls.Add(panel);
            }
        }
    }
}