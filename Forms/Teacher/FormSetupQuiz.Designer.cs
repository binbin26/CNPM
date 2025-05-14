namespace CNPM.Forms.Teacher
{
    partial class FormSetupQuiz
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtQuestionCount;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Button btnNext;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtQuestionCount = new System.Windows.Forms.TextBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtQuestionCount
            // 
            this.txtQuestionCount.Location = new System.Drawing.Point(30, 20);
            this.txtQuestionCount.Name = "txtQuestionCount";
            this.txtQuestionCount.Size = new System.Drawing.Size(200, 22);
            this.txtQuestionCount.TabIndex = 0;
            this.txtQuestionCount.Text = "Số câu hỏi";
            // 
            // txtDuration
            // 
            this.txtDuration.Location = new System.Drawing.Point(30, 60);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(200, 22);
            this.txtDuration.TabIndex = 1;
            this.txtDuration.Text = "Thời lượng";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(30, 100);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(100, 30);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Tiếp tục";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // FormSetupQuiz
            // 
            this.ClientSize = new System.Drawing.Size(280, 160);
            this.Controls.Add(this.txtQuestionCount);
            this.Controls.Add(this.txtDuration);
            this.Controls.Add(this.btnNext);
            this.Name = "FormSetupQuiz";
            this.Text = "Cài đặt bài trắc nghiệm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}