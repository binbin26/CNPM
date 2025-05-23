namespace CNPM.Forms.Student
{
    partial class ucQuiz
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblQuestion = new System.Windows.Forms.Label();
            this.radioA = new System.Windows.Forms.RadioButton();
            this.radioB = new System.Windows.Forms.RadioButton();
            this.radioC = new System.Windows.Forms.RadioButton();
            this.radioD = new System.Windows.Forms.RadioButton();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblTimer = new System.Windows.Forms.Label();
            this.lblQuestionNumber = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.flpQuestionList = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.lblQuestion.Location = new System.Drawing.Point(50, 100);
            this.lblQuestion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(288, 35);
            this.lblQuestion.TabIndex = 0;
            this.lblQuestion.Text = "Câu hỏi sẽ hiển thị ở đây";
            // 
            // radioA
            // 
            this.radioA.AutoSize = true;
            this.radioA.Location = new System.Drawing.Point(56, 166);
            this.radioA.Margin = new System.Windows.Forms.Padding(4);
            this.radioA.Name = "radioA";
            this.radioA.Size = new System.Drawing.Size(99, 24);
            this.radioA.TabIndex = 1;
            this.radioA.Text = "Đáp án A";
            this.radioA.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // radioB
            // 
            this.radioB.AutoSize = true;
            this.radioB.Location = new System.Drawing.Point(56, 204);
            this.radioB.Margin = new System.Windows.Forms.Padding(4);
            this.radioB.Name = "radioB";
            this.radioB.Size = new System.Drawing.Size(100, 24);
            this.radioB.TabIndex = 2;
            this.radioB.Text = "Đáp án B";
            this.radioB.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // radioC
            // 
            this.radioC.AutoSize = true;
            this.radioC.Location = new System.Drawing.Point(56, 241);
            this.radioC.Margin = new System.Windows.Forms.Padding(4);
            this.radioC.Name = "radioC";
            this.radioC.Size = new System.Drawing.Size(100, 24);
            this.radioC.TabIndex = 3;
            this.radioC.Text = "Đáp án C";
            this.radioC.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // radioD
            // 
            this.radioD.AutoSize = true;
            this.radioD.Location = new System.Drawing.Point(56, 278);
            this.radioD.Margin = new System.Windows.Forms.Padding(4);
            this.radioD.Name = "radioD";
            this.radioD.Size = new System.Drawing.Size(101, 24);
            this.radioD.TabIndex = 4;
            this.radioD.Text = "Đáp án D";
            this.radioD.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(30, 387);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(125, 38);
            this.btnPrevious.TabIndex = 5;
            this.btnPrevious.Text = "Câu trước";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(195, 387);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(125, 38);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "Câu tiếp";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTimer.Location = new System.Drawing.Point(904, 10);
            this.lblTimer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(109, 46);
            this.lblTimer.TabIndex = 8;
            this.lblTimer.Text = "00:00";
            // 
            // lblQuestionNumber
            // 
            this.lblQuestionNumber.AutoSize = true;
            this.lblQuestionNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblQuestionNumber.Location = new System.Drawing.Point(51, 10);
            this.lblQuestionNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuestionNumber.Name = "lblQuestionNumber";
            this.lblQuestionNumber.Size = new System.Drawing.Size(192, 29);
            this.lblQuestionNumber.TabIndex = 9;
            this.lblQuestionNumber.Text = "Câu 1 / Tổng số";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(888, 535);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(125, 38);
            this.btnSubmit.TabIndex = 10;
            this.btnSubmit.Text = "Nộp bài";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // flpQuestionList
            // 
            this.flpQuestionList.AutoScroll = true;
            this.flpQuestionList.Location = new System.Drawing.Point(530, 105);
            this.flpQuestionList.Name = "flpQuestionList";
            this.flpQuestionList.Size = new System.Drawing.Size(314, 320);
            this.flpQuestionList.TabIndex = 11;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(400, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(321, 46);
            this.lblTitle.TabIndex = 13;
            this.lblTitle.Text = "BÀI TRẮC NGHIỆM";
            // 
            // ucQuiz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.radioA);
            this.Controls.Add(this.radioB);
            this.Controls.Add(this.radioC);
            this.Controls.Add(this.radioD);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.lblQuestionNumber);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.flpQuestionList);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucQuiz";
            this.Size = new System.Drawing.Size(1034, 591);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.RadioButton radioA;
        private System.Windows.Forms.RadioButton radioB;
        private System.Windows.Forms.RadioButton radioC;
        private System.Windows.Forms.RadioButton radioD;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Label lblQuestionNumber;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.FlowLayoutPanel flpQuestionList;
        private System.Windows.Forms.Label lblTitle;
    }
}
