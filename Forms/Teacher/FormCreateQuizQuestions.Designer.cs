using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class FormCreateQuizQuestions
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelContainer;
        private Label lblProgress;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelContainer = new Panel();
            this.lblProgress = new Label();

            // lblProgress
            this.lblProgress.Dock = DockStyle.Top;
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblProgress.Height = 30;
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // panelContainer
            this.panelContainer.Dock = DockStyle.Fill;

            // FormCreateQuizQuestions
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.lblProgress);
            this.Name = "FormCreateQuizQuestions";
            this.Text = "Tạo câu hỏi trắc nghiệm";
        }
    }
}