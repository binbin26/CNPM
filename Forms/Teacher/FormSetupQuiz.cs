using System;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class FormSetupQuiz : Form
    {
        private int SessionID, CourseID;

        public FormSetupQuiz(int sessionId, int courseId)
        {
            InitializeComponent();
            SessionID = sessionId;
            CourseID = courseId;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtQuestionCount.Text, out int count) || count <= 0)
            {
                MessageBox.Show("Số câu hỏi không hợp lệ");
                return;
            }
            if (!int.TryParse(txtDuration.Text, out int duration) || duration <= 0)
            {
                MessageBox.Show("Thời lượng không hợp lệ");
                return;
            }

            var create = new FormCreateQuizQuestions(count, duration, SessionID, CourseID);
            create.ShowDialog();
            this.Close();
        }
    }
}