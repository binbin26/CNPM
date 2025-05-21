using System.Windows.Forms;
using System.Drawing;

namespace CNPM.Forms.Teacher
{
    partial class UcQuestionCreator
    {
        private TextBox txtQuestion, txtA, txtB, txtC, txtD, txtCorrect;
        private Button btnSubmit;

        private void InitializeComponent()
        {
            txtQuestion = new TextBox { Text = "Câu hỏi", Location = new Point(20, 20), Width = 600, ForeColor = Color.Gray };
            txtA = new TextBox { Text = "Đáp án A", Location = new Point(20, 60), Width = 600, ForeColor = Color.Gray };
            txtB = new TextBox { Text = "Đáp án B", Location = new Point(20, 100), Width = 600, ForeColor = Color.Gray };
            txtC = new TextBox { Text = "Đáp án C", Location = new Point(20, 140), Width = 600, ForeColor = Color.Gray };
            txtD = new TextBox { Text = "Đáp án D", Location = new Point(20, 180), Width = 600, ForeColor = Color.Gray };
            txtCorrect = new TextBox { Text = "Đáp án đúng (A/B/C/D)", Location = new Point(20, 220), Width = 600, ForeColor = Color.Gray };

            foreach (var tb in new[] { txtQuestion, txtA, txtB, txtC, txtD, txtCorrect })
            {
                tb.Enter += RemovePlaceholder;
                tb.Leave += SetPlaceholder;
            }

            this.btnSubmit = new Button
            {
                Text = "Xác nhận & tiếp tục",
                Location = new Point(20, 260),
                Width = 200
            };
            this.btnSubmit.Click += btnSubmit_Click;

            this.Controls.AddRange(new Control[]
            {
                txtQuestion, txtA, txtB, txtC, txtD, txtCorrect, btnSubmit
            });
        }
    }
}