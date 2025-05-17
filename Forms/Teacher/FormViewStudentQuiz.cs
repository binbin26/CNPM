using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class FormViewStudentQuiz : Form
    {
        private List<Question> questions;
        private List<string> selectedAnswers;

        public FormViewStudentQuiz(List<Question> questions, List<string> selectedAnswers)
        {
            InitializeComponent();

            this.questions = questions ?? new List<Question>();
            this.selectedAnswers = selectedAnswers ?? new List<string>();

            LoadQuestions();
        }

        private void LoadQuestions()
        {
            flowLayoutPanelQuestions.Controls.Clear();

            for (int i = 0; i < questions.Count; i++)
            {
                var q = questions[i];
                string studentAnswer = i < selectedAnswers.Count ? selectedAnswers[i] : "";

                var panel = new Panel
                {
                    Width = flowLayoutPanelQuestions.Width - 25,
                    Height = 150,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(5)
                };

                var lblQ = new Label
                {
                    Text = $"Câu {i + 1}: {q.Content}",
                    AutoSize = false,
                    Width = panel.Width - 20,
                    Height = 30,
                    Location = new Point(10, 5),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };
                panel.Controls.Add(lblQ);

                // Tạo checkbox cho 4 đáp án
                string[] options = { q.OptionA, q.OptionB, q.OptionC, q.OptionD };
                char[] optionLabels = { 'A', 'B', 'C', 'D' };

                for (int j = 0; j < 4; j++)
                {
                    var chk = new CheckBox
                    {
                        Text = $"{optionLabels[j]}. {options[j]}",
                        AutoSize = true,
                        Location = new Point(20, 40 + j * 25),
                        Enabled = false,
                        Checked = studentAnswer.Equals(optionLabels[j].ToString(), StringComparison.OrdinalIgnoreCase)
                    };

                    panel.Controls.Add(chk);
                }

                flowLayoutPanelQuestions.Controls.Add(panel);
            }
        }
    }
}
