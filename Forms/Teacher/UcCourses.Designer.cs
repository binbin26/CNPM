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
            this.flowPanelCourses.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowPanelCourses.Width = 250;
            this.flowPanelCourses.BackColor = System.Drawing.Color.LightGray;
            this.flowPanelCourses.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanelCourses.WrapContents = false;
            // 
            // panelCourseDetail
            // 
            this.panelCourseDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCourseDetail.BackColor = System.Drawing.Color.White;
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
