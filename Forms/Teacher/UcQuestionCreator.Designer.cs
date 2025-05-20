using System;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class UcQuestionCreator
    {
        private TextBox txtQuestion, txtA, txtB, txtC, txtD, txtCorrect;
        private Button btnSubmit;

        private void InitializeComponent()
        {
            txtQuestion = new TextBox { Text = "Câu hỏi", Width = 500, Top = 10, Left = 10, ForeColor = System.Drawing.Color.Gray };
            txtA = new TextBox { Text = "Đáp án A", Width = 500, Top = 40, Left = 10, ForeColor = System.Drawing.Color.Gray };
            txtB = new TextBox { Text = "Đáp án B", Width = 500, Top = 70, Left = 10, ForeColor = System.Drawing.Color.Gray };
            txtC = new TextBox { Text = "Đáp án C", Width = 500, Top = 100, Left = 10, ForeColor = System.Drawing.Color.Gray };
            txtD = new TextBox { Text = "Đáp án D", Width = 500, Top = 130, Left = 10, ForeColor = System.Drawing.Color.Gray };
            txtCorrect = new TextBox { Text = "Đáp án đúng (A/B/C/D)", Width = 500, Top = 160, Left = 10, ForeColor = System.Drawing.Color.Gray };

            txtQuestion.Enter += RemovePlaceholder;
            txtA.Enter += RemovePlaceholder;
            txtB.Enter += RemovePlaceholder;
            txtC.Enter += RemovePlaceholder;
            txtD.Enter += RemovePlaceholder;
            txtCorrect.Enter += RemovePlaceholder;

            txtQuestion.Leave += AddPlaceholder;
            txtA.Leave += AddPlaceholder;
            txtB.Leave += AddPlaceholder;
            txtC.Leave += AddPlaceholder;
            txtD.Leave += AddPlaceholder;
            txtCorrect.Leave += AddPlaceholder;

            btnSubmit = new Button { Text = "Xác nhận", Top = 200, Left = 10 };
            btnSubmit.Click += new EventHandler(this.btnSubmit_Click);

            this.Controls.AddRange(new Control[] { txtQuestion, txtA, txtB, txtC, txtD, txtCorrect, btnSubmit });
            this.Size = new System.Drawing.Size(540, 250);
        }
        private void RemovePlaceholder(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && tb.ForeColor == System.Drawing.Color.Gray)
            {
                tb.Text = "";
                tb.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void AddPlaceholder(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && string.IsNullOrWhiteSpace(tb.Text))
            {
                if (tb == txtQuestion) tb.Text = "Câu hỏi";
                else if (tb == txtA) tb.Text = "Đáp án A";
                else if (tb == txtB) tb.Text = "Đáp án B";
                else if (tb == txtC) tb.Text = "Đáp án C";
                else if (tb == txtD) tb.Text = "Đáp án D";
                else if (tb == txtCorrect) tb.Text = "Đáp án đúng (A/B/C/D)";
                tb.ForeColor = System.Drawing.Color.Gray;
            }
        }
    }
}