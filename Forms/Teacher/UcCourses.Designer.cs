using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class UcCourses
    {
        private FlowLayoutPanel flowPanelCourses;

        private void InitializeComponent()
        {
            this.flowPanelCourses = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowPanelCourses
            // 
            this.flowPanelCourses.AutoScroll = true;
            this.flowPanelCourses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelCourses.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanelCourses.Location = new System.Drawing.Point(0, 0);
            this.flowPanelCourses.Name = "flowPanelCourses";
            this.flowPanelCourses.Size = new System.Drawing.Size(800, 600);
            this.flowPanelCourses.TabIndex = 0;
            this.flowPanelCourses.WrapContents = false;
            // 
            // UcCourses
            // 
            this.Controls.Add(this.flowPanelCourses);
            this.Name = "UcCourses";
            this.Size = new System.Drawing.Size(800, 600);
            this.ResumeLayout(false);

        }
    }
}