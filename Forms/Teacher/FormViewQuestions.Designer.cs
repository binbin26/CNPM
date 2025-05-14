using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class FormViewQuestions
    {
        private System.ComponentModel.IContainer components = null;
        private FlowLayoutPanel flowLayoutPanel1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new FlowLayoutPanel();
            this.SuspendLayout();

            // flowLayoutPanel1
            this.flowLayoutPanel1.Dock = DockStyle.Fill;
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            this.flowLayoutPanel1.WrapContents = false;

            // FormViewQuestions
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "FormViewQuestions";
            this.Text = "Chi tiết bài tập trắc nghiệm";
            this.ResumeLayout(false);
        }
    }
}