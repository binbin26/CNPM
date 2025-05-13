namespace CNPM.Forms.Teacher
{
    partial class UcSessionItem
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblSessionTitle;
        private System.Windows.Forms.Button btnAttachFile;
        private System.Windows.Forms.Button btnCreateAssignment;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.ListBox listBoxAssignments;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblSessionTitle = new System.Windows.Forms.Label();
            this.btnAttachFile = new System.Windows.Forms.Button();
            this.btnCreateAssignment = new System.Windows.Forms.Button();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.listBoxAssignments = new System.Windows.Forms.ListBox();

            this.SuspendLayout();

            // lblSessionTitle
            this.lblSessionTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSessionTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSessionTitle.Height = 30;

            // btnAttachFile
            this.btnAttachFile.Text = "Đính kèm file";
            this.btnAttachFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAttachFile.Height = 30;
            this.btnAttachFile.Click += new System.EventHandler(this.btnAttachFile_Click);

            // listBoxFiles
            this.listBoxFiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.listBoxFiles.Height = 100;

            // btnCreateAssignment
            this.btnCreateAssignment.Text = "Tạo bài tập";
            this.btnCreateAssignment.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCreateAssignment.Height = 30;
            //this.btnCreateAssignment.Click += new System.EventHandler(this.btnCreateAssignment_Click);

            // listBoxAssignments
            this.listBoxAssignments.Dock = System.Windows.Forms.DockStyle.Fill;

            // UcSessionItem
            this.Controls.Add(this.listBoxAssignments);
            this.Controls.Add(this.btnCreateAssignment);
            this.Controls.Add(this.listBoxFiles);
            this.Controls.Add(this.btnAttachFile);
            this.Controls.Add(this.lblSessionTitle);
            this.Name = "UcSessionItem";
            this.Size = new System.Drawing.Size(500, 250);
            this.ResumeLayout(false);
        }
    }
}
