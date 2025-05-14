using System.Drawing;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class UcQuestionCreator
    {
        private TextBox txtQuestion, txtA, txtB, txtC, txtD, txtCorrect;
        private Button btnSubmit;

        private void InitializeComponent()
        {
            txtQuestion = new TextBox { Text = "Nhập nội dung câu hỏi", Width = 400, Location = new Point(10, 10) };
            txtA = new TextBox { Text = "Đáp án A", Width = 400, Location = new Point(10, 40) };
            txtB = new TextBox { Text = "Đáp án B", Width = 400, Location = new Point(10, 70) };
            txtC = new TextBox { Text = "Đáp án C", Width = 400, Location = new Point(10, 100) };
            txtD = new TextBox { Text = "Đáp án D", Width = 400, Location = new Point(10, 130) };
            txtCorrect = new TextBox { Text = "Nhập đáp án đúng (A/B/C/D)", Width = 400, Location = new Point(10, 160) };
            btnSubmit = new Button { Text = "Xác nhận", Location = new Point(10, 200) };

            btnSubmit.Click += btnSubmit_Click;

            Controls.AddRange(new Control[] { txtQuestion, txtA, txtB, txtC, txtD, txtCorrect, btnSubmit });

            Size = new Size(420, 250);
        }
    }
}
