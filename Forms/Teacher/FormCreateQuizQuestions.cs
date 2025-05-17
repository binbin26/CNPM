using System.Collections.Generic;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class FormCreateQuizQuestions : Form
    {
        private int totalQuestions;
        private int currentIndex = 0;
        private List<Question> questions;

        public List<Question> CreatedQuestions => questions;

        public FormCreateQuizQuestions(int questionCount)
        {
            InitializeComponent();
            totalQuestions = questionCount;
            questions = new List<Question>();
            ShowNextQuestion();
        }

        private void ShowNextQuestion()
        {
            panelContainer.Controls.Clear();

            if (currentIndex >= totalQuestions)
            {
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            var uc = new UcQuestionCreator();
            uc.Dock = DockStyle.Fill;
            uc.QuestionSubmitted += OnQuestionSubmitted;
            panelContainer.Controls.Add(uc);

            lblProgress.Text = $"Câu hỏi {currentIndex + 1} / {totalQuestions}";
        }

        private void OnQuestionSubmitted(Question q)
        {
            questions.Add(q);
            currentIndex++;
            ShowNextQuestion();
        }
    }
}
