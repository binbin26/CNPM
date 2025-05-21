using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class UcQuestionCreator
    {
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.TextBox txtA, txtB, txtC, txtD;
        private System.Windows.Forms.RadioButton rbA, rbB, rbC, rbD;
        private System.Windows.Forms.Button btnSubmit;

        private void InitializeComponent()
        {
            this.BackColor = System.Drawing.Color.White;
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            // Câu hỏi
            lblQuestion = new Label
            {
                Text = "Nhập câu hỏi:",
                Location = new System.Drawing.Point(20, 20),
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                AutoSize = true
            };
            txtQuestion = new TextBox
            {
                Location = new System.Drawing.Point(20, 50),
                Width = 600,
                Font = new System.Drawing.Font("Segoe UI", 10)
            };

            // Đáp án A-D
            txtA = CreateAnswerTextBox("A", 100);
            txtB = CreateAnswerTextBox("B", 140);
            txtC = CreateAnswerTextBox("C", 180);
            txtD = CreateAnswerTextBox("D", 220);

            // Radio chọn đáp án đúng
            rbA = CreateRadio("A", 100);
            rbB = CreateRadio("B", 140);
            rbC = CreateRadio("C", 180);
            rbD = CreateRadio("D", 220);

            // Nút xác nhận
            btnSubmit = new Button
            {
                Text = "Lưu câu hỏi",
                Location = new System.Drawing.Point(20, 270),
                Width = 150,
                Height = 35,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                BackColor = System.Drawing.Color.SteelBlue,
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.Click += btnSubmit_Click;

            // Thêm controls
            this.Controls.Add(lblQuestion);
            this.Controls.Add(txtQuestion);
            this.Controls.AddRange(new Control[] { txtA, txtB, txtC, txtD, rbA, rbB, rbC, rbD, btnSubmit });
        }

        private TextBox CreateAnswerTextBox(string label, int y)
        {
            var tb = new TextBox
            {
                Location = new System.Drawing.Point(50, y),
                Width = 500,
                Font = new System.Drawing.Font("Segoe UI", 10)
            };
            var lbl = new Label
            {
                Text = $"{label}:",
                Location = new System.Drawing.Point(20, y + 3),
                Font = new System.Drawing.Font("Segoe UI", 10),
                AutoSize = true
            };
            this.Controls.Add(lbl);
            return tb;
        }

        private RadioButton CreateRadio(string tag, int y)
        {
            return new RadioButton
            {
                Text = "Đáp án đúng",
                Tag = tag,
                Location = new System.Drawing.Point(570, y + 3),
                Font = new System.Drawing.Font("Segoe UI", 9),
                AutoSize = true
            };
        }
    }
}
