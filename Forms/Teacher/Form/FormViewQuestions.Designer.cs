using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class FormViewQuestions
    {
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;

        private void InitializeComponent()
        {
            this.Text = "Chi tiết bài tập trắc nghiệm";
            this.Size = new System.Drawing.Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            this.flowLayoutPanel1 = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = System.Drawing.Color.White,
                Padding = new Padding(10)
            };

            this.Controls.Add(flowLayoutPanel1);
        }
    }
}