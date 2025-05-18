using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class FormCreateQuizQuestions
    {
        private Panel panelMain;
        private void InitializeComponent()
        {
            this.panelMain = new Panel();
            this.panelMain.Dock = DockStyle.Fill;
            this.Controls.Add(panelMain);
            this.Text = "Tạo câu hỏi trắc nghiệm";
            this.ClientSize = new System.Drawing.Size(600, 400);
        }
    }
}