using System.Windows.Forms;
using System.Drawing;

namespace CNPM.Forms.Teacher
{
    partial class FormSetupQuiz
    {
        private TextBox txtQuestionCount;
        private TextBox txtDuration;
        private Button btnConfirm;
        private TextBox txtPassScore;
        private TextBox txtMaxAttempts;
        private Panel panelMain;
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

            txtPassScore = new TextBox
            {
                Text = "Điểm đạt (VD: 5)",
                ForeColor = Color.Gray,
                Location = new Point(20, 100),
                Width = 240
            };
            txtPassScore.Enter += RemovePlaceholder;
            txtPassScore.Leave += SetPlaceholder;

            txtMaxAttempts = new TextBox
            {
                Text = "Số lần làm tối đa",
                ForeColor = Color.Gray,
                Location = new Point(20, 140),
                Width = 240
            };
            txtMaxAttempts.Enter += RemovePlaceholder;
            txtMaxAttempts.Leave += SetPlaceholder;
            
            panelMain = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10),
                BackColor = Color.White
            };

            btnConfirm = new Button
            {
                Text = "Tiếp tục",
                Location = new Point(20, 180),
                Width = 100
            };
            btnConfirm.Click += btnConfirm_Click;

            panelMain.Controls.Add(txtQuestionCount);
            panelMain.Controls.Add(txtDuration);
            panelMain.Controls.Add(txtPassScore);
            panelMain.Controls.Add(txtMaxAttempts);
            panelMain.Controls.Add(btnConfirm);
            this.Controls.Add(panelMain);
        }
    }
}