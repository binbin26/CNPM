using System.Windows.Forms;
using System;
using System.Drawing;

namespace CNPM.Forms.Teacher
{
    partial class UcSessionItem
    {
        private Label lblSessionTitle;
        private Button btnAttachFile;
        private Button btnCreateAssignment;

        private void InitializeComponent()
        {
            this.lblSessionTitle = new Label();
            this.btnAttachFile = new Button();
            this.btnCreateAssignment = new Button();
            this.SuspendLayout();

            // lblSessionTitle
            this.lblSessionTitle.Dock = DockStyle.Top;
            this.lblSessionTitle.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            this.lblSessionTitle.Height = 30;

            // btnAttachFile
            this.btnAttachFile.Text = "Đính kèm file";
            this.btnAttachFile.Dock = DockStyle.Top;
            this.btnAttachFile.Height = 30;
            this.btnAttachFile.Click += new EventHandler(this.btnAttachFile_Click);

            // btnCreateAssignment
            this.btnCreateAssignment.Text = "Tạo bài tập";
            this.btnCreateAssignment.Dock = DockStyle.Top;
            this.btnCreateAssignment.Height = 30;
            this.btnCreateAssignment.Click += new EventHandler(this.btnCreateAssignment_Click);

            // UcSessionItem
            this.Controls.Add(this.btnCreateAssignment);
            this.Controls.Add(this.btnAttachFile);
            this.Controls.Add(this.lblSessionTitle);
            this.Height = 150;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ResumeLayout(false);
        }
    }
}