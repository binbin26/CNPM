using System.Windows.Forms;
using System.Drawing;
using System;

namespace CNPM.Forms.Teacher
{
    partial class UcSessionItem
    {
        private Label lblSessionTitle;
        private Button btnAttachFile;
        private Button btnCreateAssignment;
        private FlowLayoutPanel flowPanelAssignments;

        private void InitializeComponent()
        {
            this.lblSessionTitle = new Label();
            this.btnAttachFile = new Button();
            this.btnCreateAssignment = new Button();
            this.flowPanelAssignments = new FlowLayoutPanel();

            // lblSessionTitle
            this.lblSessionTitle.Text = "Buổi học";
            this.lblSessionTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblSessionTitle.Dock = DockStyle.Top;
            this.lblSessionTitle.Height = 50;
            this.lblSessionTitle.TextAlign = ContentAlignment.MiddleLeft;
            this.lblSessionTitle.Padding = new Padding(10, 0, 0, 0);
            this.lblSessionTitle.ForeColor = Color.FromArgb(30, 30, 30);

            // flowPanelAssignments
            this.flowPanelAssignments.AutoScroll = false;
            this.flowPanelAssignments.Dock = DockStyle.Fill;
            this.flowPanelAssignments.FlowDirection = FlowDirection.TopDown;
            this.flowPanelAssignments.WrapContents = false;
            this.flowPanelAssignments.Padding = new Padding(10);
            this.flowPanelAssignments.AutoScroll = true; // đã false, cần chỉnh
            this.flowPanelAssignments.Controls.Add(this.btnAttachFile);
            this.flowPanelAssignments.Controls.Add(this.btnCreateAssignment);

            // btnAttachFile
            this.btnAttachFile.Text = "📎 Đính kèm tài liệu";
            this.btnAttachFile.AutoSize = true;
            this.btnAttachFile.Margin = new System.Windows.Forms.Padding(5);

            // btnCreateAssignment
            this.btnCreateAssignment.Text = "📝 Tạo bài tập";
            this.btnCreateAssignment.AutoSize = true;
            this.btnCreateAssignment.Margin = new System.Windows.Forms.Padding(5);

            // UcSessionItem
            this.Controls.Add(this.flowPanelAssignments);
            this.Controls.Add(this.lblSessionTitle);
            this.Padding = new Padding(5);
            this.Height = 200;
            this.BackColor = Color.White;
            this.Margin = new Padding(10);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Name = "UcSessionItem";
        }
    }
}
