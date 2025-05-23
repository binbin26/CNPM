namespace CNPM.Forms.Student
{
    partial class ucEssay
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
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
            this.lblAssignmentTitle = new System.Windows.Forms.Label();
            this.btnDownloadPrompt = new System.Windows.Forms.Button();
            this.btnChooseFile = new System.Windows.Forms.Button();
            this.txtSelectedFile = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // lblAssignmentTitle
            // 
            this.lblAssignmentTitle.AutoSize = true;
            this.lblAssignmentTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblAssignmentTitle.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lblAssignmentTitle.Location = new System.Drawing.Point(330, 22);
            this.lblAssignmentTitle.Name = "lblAssignmentTitle";
            this.lblAssignmentTitle.Size = new System.Drawing.Size(334, 32);
            this.lblAssignmentTitle.TabIndex = 0;
            this.lblAssignmentTitle.Text = "Đề bài: [Tên bài tập tự luận]";
            // 
            // btnDownloadPrompt
            // 
            this.btnDownloadPrompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.btnDownloadPrompt.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.btnDownloadPrompt.Location = new System.Drawing.Point(356, 80);
            this.btnDownloadPrompt.Name = "btnDownloadPrompt";
            this.btnDownloadPrompt.Size = new System.Drawing.Size(249, 50);
            this.btnDownloadPrompt.TabIndex = 1;
            this.btnDownloadPrompt.Text = "Tải đề bài về máy";
            this.btnDownloadPrompt.UseVisualStyleBackColor = true;
            this.btnDownloadPrompt.Click += new System.EventHandler(this.btnDownloadPrompt_Click);
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.btnChooseFile.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.btnChooseFile.Location = new System.Drawing.Point(264, 308);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(400, 39);
            this.btnChooseFile.TabIndex = 2;
            this.btnChooseFile.Text = "Chọn file bài làm";
            this.btnChooseFile.UseVisualStyleBackColor = true;
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
            // 
            // txtSelectedFile
            // 
            this.txtSelectedFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.txtSelectedFile.Location = new System.Drawing.Point(264, 353);
            this.txtSelectedFile.Name = "txtSelectedFile";
            this.txtSelectedFile.ReadOnly = true;
            this.txtSelectedFile.Size = new System.Drawing.Size(400, 36);
            this.txtSelectedFile.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.btnSubmit.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.btnSubmit.Location = new System.Drawing.Point(378, 425);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(176, 40);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Nộp bài";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "PDF files (*.pdf)|*.pdf|Word files (*.docx)|*.docx|ZIP files (*.zip)|*.zip|RAR files (*.rar)|*.rar|All files (*.*)|*.*";
            this.openFileDialog1.Title = "Chọn file bài làm";
            // 
            // ucEssay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblAssignmentTitle);
            this.Controls.Add(this.btnDownloadPrompt);
            this.Controls.Add(this.btnChooseFile);
            this.Controls.Add(this.txtSelectedFile);
            this.Controls.Add(this.btnSubmit);
            this.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.Name = "ucEssay";
            this.Size = new System.Drawing.Size(938, 544);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblAssignmentTitle;
        private System.Windows.Forms.Button btnDownloadPrompt;
        private System.Windows.Forms.Button btnChooseFile;
        private System.Windows.Forms.TextBox txtSelectedFile;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}
