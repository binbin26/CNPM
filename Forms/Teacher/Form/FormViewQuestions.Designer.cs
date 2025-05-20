using System.Windows.Forms;
using System.Drawing;

namespace CNPM.Forms.Teacher
{
    partial class FormViewQuestions
    {
        private FlowLayoutPanel flowLayoutPanel1;

        private void InitializeComponent()
        {
            this.Text = "Chi tiết bài tập trắc nghiệm";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            this.flowLayoutPanel1 = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.White
            };

            this.Controls.Add(flowLayoutPanel1);
        }
    }
}