namespace CNPM.Forms.Teacher
{
    partial class UcQuizQuestion
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.TextBox txtA;
        private System.Windows.Forms.TextBox txtB;
        private System.Windows.Forms.TextBox txtC;
        private System.Windows.Forms.TextBox txtD;
        private System.Windows.Forms.TextBox txtCorrectAnswer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.txtA = new System.Windows.Forms.TextBox();
            this.txtB = new System.Windows.Forms.TextBox();
            this.txtC = new System.Windows.Forms.TextBox();
            this.txtD = new System.Windows.Forms.TextBox();
            this.txtCorrectAnswer = new System.Windows.Forms.TextBox();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(90, 17);
            this.lblTitle.Text = "Câu hỏi n";

            // txtQuestion
            this.txtQuestion.Location = new System.Drawing.Point(6, 20);
            this.txtQuestion.Size = new System.Drawing.Size(480, 22);

            // txtA
            this.txtA.Location = new System.Drawing.Point(6, 50);
            this.txtA.Size = new System.Drawing.Size(480, 22);

            // txtB
            this.txtB.Location = new System.Drawing.Point(6, 80);
            this.txtB.Size = new System.Drawing.Size(480, 22);

            // txtC
            this.txtC.Location = new System.Drawing.Point(6, 110);
            this.txtC.Size = new System.Drawing.Size(480, 22);

            // txtD
            this.txtD.Location = new System.Drawing.Point(6, 140);
            this.txtD.Size = new System.Drawing.Size(480, 22);

            // txtCorrectAnswer
            this.txtCorrectAnswer.Location = new System.Drawing.Point(6, 170);
            this.txtCorrectAnswer.Size = new System.Drawing.Size(480, 22);

            // UcQuizQuestion
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtQuestion);
            this.Controls.Add(this.txtA);
            this.Controls.Add(this.txtB);
            this.Controls.Add(this.txtC);
            this.Controls.Add(this.txtD);
            this.Controls.Add(this.txtCorrectAnswer);
            this.Name = "UcQuizQuestion";
            this.Size = new System.Drawing.Size(500, 210);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
