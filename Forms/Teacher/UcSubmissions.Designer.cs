using System.Drawing;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class UcSubmissions
    {
        private System.ComponentModel.IContainer components = null;
        private FlowLayoutPanel flowPanelCourses;
        private FlowLayoutPanel flowPanelMCQAssignments;   // Bài tập trắc nghiệm
        private FlowLayoutPanel flowPanelEssayAssignments; // Bài tập tự luận
        private FlowLayoutPanel panelStudents;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.flowPanelCourses = new FlowLayoutPanel();
            this.flowPanelMCQAssignments = new FlowLayoutPanel();
            this.flowPanelEssayAssignments = new FlowLayoutPanel();
            this.panelStudents = new FlowLayoutPanel();

            // flowPanelCourses
            this.flowPanelCourses.Dock = DockStyle.Left;
            this.flowPanelCourses.Width = 200;
            this.flowPanelCourses.AutoScroll = true;
            this.flowPanelCourses.BackColor = Color.LightSteelBlue;
            this.flowPanelCourses.FlowDirection = FlowDirection.TopDown;
            this.flowPanelCourses.WrapContents = false;

            // Cấu hình flowPanelMCQAssignments
            this.flowPanelMCQAssignments.Dock = DockStyle.Left;
            this.flowPanelMCQAssignments.Width = 250;
            this.flowPanelMCQAssignments.AutoScroll = true;
            this.flowPanelMCQAssignments.BackColor = Color.LightYellow;
            this.flowPanelMCQAssignments.FlowDirection = FlowDirection.TopDown;
            this.flowPanelMCQAssignments.WrapContents = false;

            // Cấu hình flowPanelEssayAssignments
            this.flowPanelEssayAssignments.Dock = DockStyle.Left;
            this.flowPanelEssayAssignments.Width = 250;
            this.flowPanelEssayAssignments.AutoScroll = true;
            this.flowPanelEssayAssignments.BackColor = Color.LightPink;
            this.flowPanelEssayAssignments.FlowDirection = FlowDirection.TopDown;
            this.flowPanelEssayAssignments.WrapContents = false;

            // panelStudents
            this.panelStudents.Dock = DockStyle.Fill;
            this.panelStudents.AutoScroll = true;
            this.panelStudents.BackColor = Color.White;

            // Add controls
            this.Controls.Add(this.panelStudents);
            this.Controls.Add(this.flowPanelEssayAssignments);
            this.Controls.Add(this.flowPanelMCQAssignments);
            this.Controls.Add(this.flowPanelCourses);

            this.Name = "UcSubmissions";
            this.Size = new Size(800, 600);
        }
    }
}
