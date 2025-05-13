namespace CNPM.Forms.Teacher
{
    partial class UcCourseDetail
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnAddSession;
        private System.Windows.Forms.FlowLayoutPanel flowPanelSessions;

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
            this.btnAddSession = new System.Windows.Forms.Button();
            this.flowPanelSessions = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // btnAddSession
            // 
            this.btnAddSession.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddSession.Height = 40;
            this.btnAddSession.Text = "Thêm buổi học";
            this.btnAddSession.Click += new System.EventHandler(this.btnAddSession_Click);
            // 
            // flowPanelSessions
            // 
            this.flowPanelSessions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelSessions.AutoScroll = true;
            this.flowPanelSessions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanelSessions.WrapContents = false;
            // 
            // UcCourseDetail
            // 
            this.Controls.Add(this.flowPanelSessions);
            this.Controls.Add(this.btnAddSession);
            this.Name = "UcCourseDetail";
            this.Size = new System.Drawing.Size(550, 600);
            this.ResumeLayout(false);
        }
    }
}
