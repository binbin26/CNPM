using System.Windows.Forms;
using System.Drawing;

namespace CNPM.Forms.Teacher
{
    partial class FormCreateQuizQuestions
    {
        private Panel panelMain;

        private void InitializeComponent()
        {
            this.Text = "Tạo câu hỏi trắc nghiệm";
            this.Size = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterParent;

            this.panelMain = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            this.Controls.Add(panelMain);
        }
    }
}
