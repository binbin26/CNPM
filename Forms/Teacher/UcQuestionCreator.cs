using System;
using System.Linq;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class UcQuestionCreator : UserControl
    {
        public event Action<Question> QuestionSubmitted;

        public UcQuestionCreator()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string correct = txtCorrect.Text.Trim().ToUpper();
            if (!new[] { "A", "B", "C", "D" }.Contains(correct))
            {
                MessageBox.Show("Đáp án phải là A, B, C hoặc D");
                return;
            }

            var q = new Question
            {
                QuestionText = txtQuestion.Text,
                OptionA = txtA.Text,
                OptionB = txtB.Text,
                OptionC = txtC.Text,
                OptionD = txtD.Text,
                CorrectAnswer = correct
            };

            QuestionSubmitted?.Invoke(q);
        }
    }
}