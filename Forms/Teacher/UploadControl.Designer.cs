namespace CNPM.Forms.Teacher
{
    partial class UploadControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnUploadDocument;
        private System.Windows.Forms.Button btnUploadAssignment;

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
            this.btnUploadDocument = new System.Windows.Forms.Button();
            this.btnUploadAssignment = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // btnUploadDocument
            this.btnUploadDocument.Text = "Upload Tài liệu";
            this.btnUploadDocument.Location = new System.Drawing.Point(20, 20);
            this.btnUploadDocument.Size = new System.Drawing.Size(150, 30);
            this.btnUploadDocument.Click += new System.EventHandler(this.btnUploadDocument_Click);

            // btnUploadAssignment
            this.btnUploadAssignment.Text = "Upload Bài tập";
            this.btnUploadAssignment.Location = new System.Drawing.Point(20, 70);
            this.btnUploadAssignment.Size = new System.Drawing.Size(150, 30);
            this.btnUploadAssignment.Click += new System.EventHandler(this.btnUploadAssignment_Click);

            // UploadControl
            this.Controls.Add(this.btnUploadDocument);
            this.Controls.Add(this.btnUploadAssignment);
            this.Name = "UploadControl";
            this.Size = new System.Drawing.Size(800, 400);
            this.ResumeLayout(false);
        }
    }
}
