using System;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class FormSetupQuiz
    {
        private System.Windows.Forms.TextBox txtQuestionCount, txtDuration;
        private System.Windows.Forms.Button btnConfirm;

        private void InitializeComponent()
        {
            this.txtQuestionCount = new System.Windows.Forms.TextBox { Text = "Số câu hỏi", Location = new System.Drawing.Point(20, 20), Width = 200, ForeColor = System.Drawing.Color.Gray };
            this.txtDuration = new System.Windows.Forms.TextBox { Text = "Thời lượng (phút)", Location = new System.Drawing.Point(20, 60), Width = 200, ForeColor = System.Drawing.Color.Gray };
            this.btnConfirm = new System.Windows.Forms.Button { Text = "Tiếp tục", Location = new System.Drawing.Point(20, 100), Width = 100 };

            this.txtQuestionCount.Enter += RemovePlaceholder;
            this.txtDuration.Enter += RemovePlaceholder;
            this.txtQuestionCount.Leave += AddPlaceholder;
            this.txtDuration.Leave += AddPlaceholder;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);

            this.Controls.Add(txtQuestionCount);
            this.Controls.Add(txtDuration);
            this.Controls.Add(btnConfirm);
            this.ClientSize = new System.Drawing.Size(260, 160);
            this.Text = "Cài đặt bài trắc nghiệm";
        }

        private void RemovePlaceholder(object sender, System.EventArgs e)
        {
            var tb = sender as System.Windows.Forms.TextBox;
            if (tb != null && tb.ForeColor == System.Drawing.Color.Gray)
            {
                tb.Text = "";
                tb.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void AddPlaceholder(object sender, System.EventArgs e)
        {
            var tb = sender as System.Windows.Forms.TextBox;
            if (tb != null && string.IsNullOrWhiteSpace(tb.Text))
            {
                if (tb == txtQuestionCount) tb.Text = "Số câu hỏi";
                else if (tb == txtDuration) tb.Text = "Thời lượng (phút)";
                tb.ForeColor = System.Drawing.Color.Gray;
            }
        }
    }
}
