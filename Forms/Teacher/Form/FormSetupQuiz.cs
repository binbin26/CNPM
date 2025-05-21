using System;
using System.Drawing;
using System.Windows.Forms;
using CNPM.Models.Assignments;

namespace CNPM.Forms.Teacher
{
    public partial class FormSetupQuiz : Form
    {
        private int TeacherID, CourseID, SessionID;

        public FormSetupQuiz(int teacherID, int courseId, int sessionId)
        {
            InitializeComponent();
            TeacherID = teacherID;
            CourseID = courseId;
            SessionID = sessionId;
        }
        private void RemovePlaceholder(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && (
                tb.Text == "Số câu hỏi" ||
                tb.Text == "Thời lượng (phút)" ||
                tb.Text == "Điểm đạt (VD: 5)" ||
                tb.Text == "Số lần làm tối đa"))
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
                else if (tb == txtPassScore)
                    tb.Text = "Điểm đạt (VD: 5)";
                else if (tb == txtMaxAttempts)
                    tb.Text = "Số lần làm tối đa";

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
            if (!float.TryParse(txtPassScore.Text, out float passScore) || passScore < 0)
            {
                MessageBox.Show("Điểm đạt không hợp lệ.");
                return;
            }
            if (!int.TryParse(txtMaxAttempts.Text, out int maxAttempts) || maxAttempts <= 0)
            {
                MessageBox.Show("Số lần làm không hợp lệ.");
                return;
            }

            var formCreate = new FormCreateQuizQuestions(count, TeacherID, CourseID, SessionID)
            {
                PassScore = passScore,
                MaxAttempts = maxAttempts
            };
            formCreate.ShowDialog();
            this.Close();
        }
    }
}