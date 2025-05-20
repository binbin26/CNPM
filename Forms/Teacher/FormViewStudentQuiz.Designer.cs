using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class FormViewStudentQuiz
    {
        private FlowLayoutPanel flowPanelQuestions;

        private void InitializeComponent()
        {
            this.flowPanelQuestions = new FlowLayoutPanel();
            this.flowPanelQuestions.Dock = DockStyle.Fill;
            this.flowPanelQuestions.AutoScroll = true;
            this.flowPanelQuestions.FlowDirection = FlowDirection.TopDown;
            this.flowPanelQuestions.WrapContents = false;

            this.Controls.Add(this.flowPanelQuestions);
            this.Text = "Chi tiết bài trắc nghiệm";
            this.ClientSize = new System.Drawing.Size(700, 500);
        }
    }
}