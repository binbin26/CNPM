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
            var question = new Question
            {
                Content = txtQuestion.Text,
                OptionA = txtA.Text,
                OptionB = txtB.Text,
                OptionC = txtC.Text,
                OptionD = txtD.Text,
                CorrectAnswer = txtCorrect.Text.ToUpper()
            };

            if (!new[] { "A", "B", "C", "D" }.Contains(question.CorrectAnswer))
            {
                MessageBox.Show("Đáp án đúng phải là A, B, C hoặc D.");
                return;
            }

            QuestionSubmitted?.Invoke(question);
        }
    }
}
