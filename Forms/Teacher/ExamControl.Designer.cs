namespace CNPM.Forms.Teacher
{
    partial class ExamControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.RadioButton radioBtnMultipleChoice;
        private System.Windows.Forms.RadioButton radioBtnEssay;
        private System.Windows.Forms.Button btnCreateExam;

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
            this.radioBtnMultipleChoice = new System.Windows.Forms.RadioButton();
            this.radioBtnEssay = new System.Windows.Forms.RadioButton();
            this.btnCreateExam = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // radioBtnMultipleChoice
            this.radioBtnMultipleChoice.Text = "Trắc nghiệm";
            this.radioBtnMultipleChoice.Location = new System.Drawing.Point(20, 20);
            this.radioBtnMultipleChoice.Size = new System.Drawing.Size(100, 20);

            // radioBtnEssay
            this.radioBtnEssay.Text = "Tự luận";
            this.radioBtnEssay.Location = new System.Drawing.Point(20, 60);
            this.radioBtnEssay.Size = new System.Drawing.Size(100, 20);

            // btnCreateExam
            this.btnCreateExam.Text = "Tạo bài kiểm tra";
            this.btnCreateExam.Location = new System.Drawing.Point(20, 100);
            this.btnCreateExam.Size = new System.Drawing.Size(150, 30);
            this.btnCreateExam.Click += new System.EventHandler(this.btnCreateExam_Click);

            // ExamControl
            this.Controls.Add(this.radioBtnMultipleChoice);
            this.Controls.Add(this.radioBtnEssay);
            this.Controls.Add(this.btnCreateExam);
            this.Name = "ExamControl";
            this.Size = new System.Drawing.Size(800, 400);
            this.ResumeLayout(false);
        }
    }
}
