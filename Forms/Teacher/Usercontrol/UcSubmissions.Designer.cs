using System.Drawing;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class UcSubmissions
    {
        private FlowLayoutPanel flowPanelCourses;
        private FlowLayoutPanel flowPanelAssignments;
        private FlowLayoutPanel flowPanelSubmissions;

        private void InitializeComponent()
        {
            this.flowPanelCourses = new FlowLayoutPanel();
            this.flowPanelAssignments = new FlowLayoutPanel();
            this.flowPanelSubmissions = new FlowLayoutPanel();

            // flowPanelCourses
            this.flowPanelCourses.Dock = DockStyle.Left;
            this.flowPanelCourses.Width = 220;
            this.flowPanelCourses.AutoScroll = true;
            this.flowPanelCourses.FlowDirection = FlowDirection.TopDown;
            this.flowPanelCourses.WrapContents = false;
            this.flowPanelCourses.Padding = new Padding(10);
            this.flowPanelCourses.BackColor = Color.WhiteSmoke;

            // flowPanelAssignments
            this.flowPanelAssignments.Dock = DockStyle.Left;
            this.flowPanelAssignments.Width = 300;
            this.flowPanelAssignments.AutoScroll = true;
            this.flowPanelAssignments.FlowDirection = FlowDirection.TopDown;
            this.flowPanelAssignments.WrapContents = false;
            this.flowPanelAssignments.Padding = new Padding(10);
            this.flowPanelAssignments.BackColor = Color.White;

            // flowPanelSubmissions
            this.flowPanelSubmissions.Dock = DockStyle.Fill;
            this.flowPanelSubmissions.AutoScroll = true;
            this.flowPanelSubmissions.FlowDirection = FlowDirection.TopDown;
            this.flowPanelSubmissions.WrapContents = false;
            this.flowPanelSubmissions.Padding = new Padding(10);
            this.flowPanelSubmissions.BackColor = Color.Gainsboro;

            // UcSubmissions
            this.Controls.Add(this.flowPanelSubmissions);
            this.Controls.Add(this.flowPanelAssignments);
            this.Controls.Add(this.flowPanelCourses);
            this.Name = "UcSubmissions";
            this.Size = new Size(1000, 600);
        }
    }
}