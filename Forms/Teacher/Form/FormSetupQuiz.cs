using System;
using System.Drawing;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class FormSetupQuiz : Form
    {
        private int SessionID, CourseID, TeacherID;

        public FormSetupQuiz(int sessionId, int courseId, int teacherID)
        {
            InitializeComponent();
            SessionID = sessionId;
            CourseID = courseId;
            TeacherID = teacherID;
        }
        private void RemovePlaceholder(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && (tb.Text == "Số câu hỏi" || tb.Text == "Thời lượng (phút)"))
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }

        private void SetPlaceholder(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && string.IsNullOrWhiteSpace(tb.Text))
            {
                if (tb == txtQuestionCount)
                    tb.Text = "Số câu hỏi";
                else if (tb == txtDuration)
                    tb.Text = "Thời lượng (phút)";
                tb.ForeColor = Color.Gray;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtQuestionCount.Text, out int count) || count <= 0)
            {
                MessageBox.Show("Số câu hỏi không hợp lệ.");
                return;
            }
            if (!int.TryParse(txtDuration.Text, out int duration) || duration <= 0)
            {
                MessageBox.Show("Thời lượng không hợp lệ.");
                return;
            }

            var formCreate = new FormCreateQuizQuestions(count,SessionID, CourseID, TeacherID);
            formCreate.ShowDialog();
            this.Close();
        }
    }
}
