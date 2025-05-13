namespace CNPM.Forms.Teacher
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelSidebar;
        private Button btnCourses;
        private Button btnSubmissions;
        private Panel panelMainContent;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnCourses = new System.Windows.Forms.Button();
            this.btnSubmissions = new System.Windows.Forms.Button();
            this.panelMainContent = new System.Windows.Forms.Panel();
            this.panelSidebar.SuspendLayout();
            this.SuspendLayout();

            // panelSidebar
            this.panelSidebar.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelSidebar.Controls.Add(this.btnSubmissions);
            this.panelSidebar.Controls.Add(this.btnCourses);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Width = 200;
            this.panelSidebar.Name = "panelSidebar";

            // btnCourses
            this.btnCourses.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCourses.Text = "Khóa học";
            this.btnCourses.Height = 50;
            this.btnCourses.Name = "btnCourses";
            this.btnCourses.Click += new System.EventHandler(this.btnCourses_Click);

            // btnSubmissions
            this.btnSubmissions.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSubmissions.Text = "Bài nộp";
            this.btnSubmissions.Height = 50;
            this.btnSubmissions.Name = "btnSubmissions";
            this.btnSubmissions.Click += new System.EventHandler(this.btnSubmissions_Click);

            // panelMainContent
            this.panelMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainContent.Name = "panelMainContent";
            this.panelMainContent.BackColor = System.Drawing.Color.White;

            // FormMain
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.panelMainContent);
            this.Controls.Add(this.panelSidebar);
            this.Name = "FormMain";
            this.Text = "LMS - Quản lý học tập";
            this.panelSidebar.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}