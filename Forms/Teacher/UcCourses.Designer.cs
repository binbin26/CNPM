using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class UcCourses
    {
        private FlowLayoutPanel flowPanelCourses;

        private void InitializeComponent()
        {
            this.flowPanelCourses = new FlowLayoutPanel();
            this.SuspendLayout();

            // flowPanelCourses
            this.flowPanelCourses.Dock = DockStyle.Fill;
            this.flowPanelCourses.AutoScroll = true;
            this.flowPanelCourses.FlowDirection = FlowDirection.TopDown;
            this.flowPanelCourses.WrapContents = false;

            // UcCourses
            this.Controls.Add(this.flowPanelCourses);
            this.Name = "UcCourses";
            this.Size = new System.Drawing.Size(800, 600);
            this.ResumeLayout(false);
        }
    }
}