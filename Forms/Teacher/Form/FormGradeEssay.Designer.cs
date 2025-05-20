using System.Drawing;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class FormGradeEssay
    {
        private TextBox txtScore;
        private Button btnConfirm;

        private void InitializeComponent()
        {
            this.Text = "Chấm điểm bài tự luận";
            this.Size = new Size(280, 150);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label lbl = new Label
            {
                Text = "Nhập điểm (0 - 10):",
                Location = new Point(20, 20),
                AutoSize = true
            };

            txtScore = new TextBox
            {
                Location = new Point(20, 45),
                Width = 200
            };

            btnConfirm = new Button
            {
                Text = "Xác nhận",
                Location = new Point(20, 80),
                Width = 100
            };
            btnConfirm.Click += btnConfirm_Click;

            this.Controls.Add(lbl);
            this.Controls.Add(txtScore);
            this.Controls.Add(btnConfirm);
        }
    }
}