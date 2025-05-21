using System.Windows.Forms;
using System.Drawing;

namespace CNPM.Forms.Teacher
{
    partial class FormViewStudentQuiz
    {
        private FlowLayoutPanel flowPanelQuestions;

        private void InitializeComponent()
        {
            this.Text = "Bài làm của sinh viên";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            this.flowPanelQuestions = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.White
            };

            this.Controls.Add(this.flowPanelQuestions);
        }
    }
}