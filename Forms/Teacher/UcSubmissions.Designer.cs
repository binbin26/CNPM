using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class UcSubmissions
    {
        private System.ComponentModel.IContainer components = null;
        private FlowLayoutPanel flowPanelCourses;
        private FlowLayoutPanel panelAssignments;
        private FlowLayoutPanel panelStudents;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.flowPanelCourses = new FlowLayoutPanel();
            this.panelAssignments = new FlowLayoutPanel();
            this.panelStudents = new FlowLayoutPanel();

            // flowPanelCourses
            this.flowPanelCourses.Dock = DockStyle.Left;
            this.flowPanelCourses.Width = 200;
            this.flowPanelCourses.AutoScroll = true;
            this.flowPanelCourses.BackColor = System.Drawing.Color.LightSteelBlue;
            this.flowPanelCourses.FlowDirection = FlowDirection.TopDown;
            this.flowPanelCourses.WrapContents = false;

            // panelAssignments
            this.panelAssignments.Dock = DockStyle.Left;
            this.panelAssignments.Width = 250;
            this.panelAssignments.AutoScroll = true;
            this.panelAssignments.BackColor = System.Drawing.Color.WhiteSmoke;

            // panelStudents
            this.panelStudents.Dock = DockStyle.Fill;
            this.panelStudents.AutoScroll = true;
            this.panelStudents.BackColor = System.Drawing.Color.White;

            // UcSubmissions
            this.Controls.Add(this.panelStudents);
            this.Controls.Add(this.panelAssignments);
            this.Controls.Add(this.flowPanelCourses);
            this.Name = "UcSubmissions";
            this.Size = new System.Drawing.Size(800, 600);
        }
    }
}
