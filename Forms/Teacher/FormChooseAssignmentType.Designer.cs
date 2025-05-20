using System;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class FormChooseAssignmentType
    {
        private Button btnMultipleChoice;
        private Button btnEssay;

        private void InitializeComponent()
        {
            this.btnMultipleChoice = new Button();
            this.btnEssay = new Button();

            this.SuspendLayout();

            // btnMultipleChoice
            this.btnMultipleChoice.Text = "Bài tập trắc nghiệm";
            this.btnMultipleChoice.Size = new System.Drawing.Size(200, 40);
            this.btnMultipleChoice.Location = new System.Drawing.Point(30, 20);
            this.btnMultipleChoice.Click += new EventHandler(this.btnMultipleChoice_Click);

            // btnEssay
            this.btnEssay.Text = "Bài tập tự luận";
            this.btnEssay.Size = new System.Drawing.Size(200, 40);
            this.btnEssay.Location = new System.Drawing.Point(30, 70);
            this.btnEssay.Click += new EventHandler(this.btnEssay_Click);

            // FormChooseAssignmentType
            this.ClientSize = new System.Drawing.Size(260, 140);
            this.Controls.Add(this.btnEssay);
            this.Controls.Add(this.btnMultipleChoice);
            this.Text = "Chọn loại bài tập";
            this.ResumeLayout(false);
        }
    }
}
