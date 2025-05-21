using System.Windows.Forms;
using System.Drawing;

namespace CNPM.Forms.Teacher
{
    partial class FormSetupQuiz
    {
        private TextBox txtQuestionCount;
        private TextBox txtDuration;
        private Button btnConfirm;

        private void InitializeComponent()
        {
            this.Text = "Cài đặt bài trắc nghiệm";
            this.Size = new Size(300, 180);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;

            txtQuestionCount = new TextBox
            {
                Text = "Số câu hỏi",
                ForeColor = Color.Gray,
                Location = new Point(20, 20),
                Width = 240
            };
            txtQuestionCount.Enter += RemovePlaceholder;
            txtQuestionCount.Leave += SetPlaceholder;

            txtDuration = new TextBox
            {
                Text = "Thời lượng (phút)",
                ForeColor = Color.Gray,
                Location = new Point(20, 60),
                Width = 240
            };
            txtDuration.Enter += RemovePlaceholder;
            txtDuration.Leave += SetPlaceholder;

            btnConfirm = new Button
            {
                Text = "Tiếp tục",
                Location = new Point(20, 100),
                Width = 100
            };
            btnConfirm.Click += btnConfirm_Click;

            this.Controls.Add(txtQuestionCount);
            this.Controls.Add(txtDuration);
            this.Controls.Add(btnConfirm);
        }
    }
}
