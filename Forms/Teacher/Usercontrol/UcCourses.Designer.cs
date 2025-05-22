using System.Drawing;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class UcCourses
    {
        private FlowLayoutPanel flowPanelCourses;

        private void InitializeComponent()
        {
            this.flowPanelCourses = new System.Windows.Forms.FlowLayoutPanel();
            this.flowPanelCourses.SuspendLayout();
            this.SuspendLayout();
            //
            // flowPanelCourses
            //
            this.flowPanelCourses.Dock = DockStyle.Fill;
            this.flowPanelCourses.AutoScroll = true;
            this.flowPanelCourses.FlowDirection = FlowDirection.TopDown;
            this.flowPanelCourses.WrapContents = false;
            this.flowPanelCourses.Padding = new Padding(10);
            this.flowPanelCourses.BackColor = Color.WhiteSmoke;
            this.flowPanelCourses.ResumeLayout(false);
            // 
            // UcCourses
            // 
            this.Controls.Add(this.flowPanelCourses);
            this.Name = "UcCourses";
            this.Size = new System.Drawing.Size(980, 700); // phù hợp với form chính
            this.ResumeLayout(false);
        }
    }
}