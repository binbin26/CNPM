using System.Drawing;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class UcSubmissions
    {
        private FlowLayoutPanel flowPanelCourses;
        private FlowLayoutPanel flowPanelAssignments;
        private FlowLayoutPanel flowPanelSubmissions;
        private Panel panelCourse1;

        private void InitializeComponent()
        {
            this.flowPanelCourses = new System.Windows.Forms.FlowLayoutPanel();
            this.flowPanelAssignments = new System.Windows.Forms.FlowLayoutPanel();
            this.flowPanelSubmissions = new System.Windows.Forms.FlowLayoutPanel();
            this.panelCourse1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // flowPanelCourses
            // 
            this.flowPanelCourses.AutoScroll = true;
            this.flowPanelCourses.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowPanelCourses.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanelCourses.Location = new System.Drawing.Point(0, 0);
            this.flowPanelCourses.Name = "flowPanelCourses";
            this.flowPanelCourses.Size = new System.Drawing.Size(220, 600);
            this.flowPanelCourses.TabIndex = 2;
            // 
            // flowPanelAssignments
            // 
            this.flowPanelAssignments.AutoScroll = true;
            this.flowPanelAssignments.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowPanelAssignments.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanelAssignments.Location = new System.Drawing.Point(220, 0);
            this.flowPanelAssignments.Name = "flowPanelAssignments";
            this.flowPanelAssignments.Size = new System.Drawing.Size(280, 600);
            this.flowPanelAssignments.TabIndex = 1;
            // 
            // flowPanelSubmissions
            // 
            this.flowPanelSubmissions.AutoScroll = true;
            this.flowPanelSubmissions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelSubmissions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanelSubmissions.Location = new System.Drawing.Point(500, 0);
            this.flowPanelSubmissions.Name = "flowPanelSubmissions";
            this.flowPanelSubmissions.Size = new System.Drawing.Size(500, 600);
            this.flowPanelSubmissions.TabIndex = 0;
            // 
            // panelCourse1
            // 
            this.panelCourse1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelCourse1.Location = new System.Drawing.Point(0, 0);
            this.panelCourse1.Name = "panelCourse1";
            this.panelCourse1.Size = new System.Drawing.Size(300, 60);
            this.panelCourse1.TabIndex = 0;
            this.panelCourse1.Tag = 101;
            // 
            // UcSubmissions
            // 
            this.Controls.Add(this.flowPanelSubmissions);
            this.Controls.Add(this.flowPanelAssignments);
            this.Controls.Add(this.flowPanelCourses);
            this.Name = "UcSubmissions";
            this.Size = new System.Drawing.Size(1000, 600);
            this.ResumeLayout(false);

        }
    }
}