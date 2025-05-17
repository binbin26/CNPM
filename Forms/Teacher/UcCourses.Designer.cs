namespace CNPM.Forms.Teacher
{
    partial class UcCourses
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flowPanelCourses;
        private System.Windows.Forms.Panel panelCourseDetail;

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
            this.flowPanelCourses = new System.Windows.Forms.FlowLayoutPanel();
            this.panelCourseDetail = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // flowPanelCourses
            // 
            this.flowPanelCourses.AutoScroll = true;
            this.flowPanelCourses.BackColor = System.Drawing.Color.White;
            this.flowPanelCourses.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowPanelCourses.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanelCourses.Location = new System.Drawing.Point(0, 0);
            this.flowPanelCourses.Name = "flowPanelCourses";
            this.flowPanelCourses.Size = new System.Drawing.Size(235, 600);
            this.flowPanelCourses.TabIndex = 1;
            this.flowPanelCourses.WrapContents = false;
            // 
            // panelCourseDetail
            // 
            this.panelCourseDetail.BackColor = System.Drawing.Color.White;
            this.panelCourseDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCourseDetail.Location = new System.Drawing.Point(235, 0);
            this.panelCourseDetail.Name = "panelCourseDetail";
            this.panelCourseDetail.Size = new System.Drawing.Size(565, 600);
            this.panelCourseDetail.TabIndex = 0;
            // 
            // UcCourses
            // 
            this.Controls.Add(this.panelCourseDetail);
            this.Controls.Add(this.flowPanelCourses);
            this.Name = "UcCourses";
            this.Size = new System.Drawing.Size(800, 600);
            this.ResumeLayout(false);

        }
    }
}
