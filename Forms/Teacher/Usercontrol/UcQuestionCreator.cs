using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CNPM.Models.Assignments;

namespace CNPM.Forms.Teacher
{
    public partial class UcQuestionCreator : UserControl
    {
        public event Action<Question> QuestionSubmitted;

        public UcQuestionCreator()
        {
            InitializeComponent();
        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null && (tb.Text.StartsWith("Đáp án") || tb.Text.StartsWith("Câu hỏi")))
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }

        private void SetPlaceholder(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null && string.IsNullOrWhiteSpace(tb.Text))
            {
                if (tb == txtQuestion) tb.Text = "Câu hỏi";
                else if (tb == txtA) tb.Text = "Đáp án A";
                else if (tb == txtB) tb.Text = "Đáp án B";
                else if (tb == txtC) tb.Text = "Đáp án C";
                else if (tb == txtD) tb.Text = "Đáp án D";
                tb.ForeColor = Color.Gray;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string correct = new[] { rbA, rbB, rbC, rbD }
                        .FirstOrDefault(r => r.Checked)?.Tag?.ToString();

            if (string.IsNullOrEmpty(correct))
            {
                MessageBox.Show("Vui lòng chọn đáp án đúng.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var question = new Question
            {
                QuestionText = txtQuestion.Text.Trim(),
                OptionA = txtA.Text.Trim(),
                OptionB = txtB.Text.Trim(),
                OptionC = txtC.Text.Trim(),
                OptionD = txtD.Text.Trim(),
                CorrectAnswer = correct
            };

            QuestionSubmitted?.Invoke(question);
        }
    }
}