namespace CNPM.Forms.Teacher
{
    partial class FormViewStudentQuiz
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelQuestions;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.flowLayoutPanelQuestions = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowLayoutPanelQuestions
            // 
            this.flowLayoutPanelQuestions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelQuestions.AutoScroll = true;
            this.flowLayoutPanelQuestions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelQuestions.WrapContents = false;
            this.flowLayoutPanelQuestions.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelQuestions.Name = "flowLayoutPanelQuestions";
            this.flowLayoutPanelQuestions.Size = new System.Drawing.Size(600, 450);
            this.flowLayoutPanelQuestions.TabIndex = 0;
            // 
            // FormViewStudentQuiz
            // 
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.flowLayoutPanelQuestions);
            this.Name = "FormViewStudentQuiz";
            this.Text = "Xem bài tập trắc nghiệm của sinh viên";
            this.ResumeLayout(false);
        }
    }
}