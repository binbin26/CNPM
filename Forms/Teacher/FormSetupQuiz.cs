using System;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class FormSetupQuiz : Form
    {
        public int QuestionCount { get; private set; }
        public int Duration { get; private set; }

        public FormSetupQuiz()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtQuestionCount.Text, out int qCount) || qCount <= 0)
            {
                MessageBox.Show("Số câu hỏi không hợp lệ.");
                return;
            }

            if (!int.TryParse(txtDuration.Text, out int duration) || duration <= 0)
            {
                MessageBox.Show("Thời lượng không hợp lệ.");
                return;
            }

            QuestionCount = qCount;
            Duration = duration;
            DialogResult = DialogResult.OK;
        }
    }
}
