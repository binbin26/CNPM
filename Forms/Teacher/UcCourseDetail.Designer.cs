using System.Windows.Forms;
using System;
using System.Drawing;

namespace CNPM.Forms.Teacher
{
    partial class UcCourseDetail
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblCourseName;
        private Button btnAddSession;
        private FlowLayoutPanel flowPanelSessions;

        private void InitializeComponent()
        {
            this.lblCourseName = new Label();
            this.btnAddSession = new Button();
            this.flowPanelSessions = new FlowLayoutPanel();
            this.SuspendLayout();

            // lblCourseName
            this.lblCourseName.Dock = DockStyle.Top;
            this.lblCourseName.Height = 40;
            this.lblCourseName.TextAlign = ContentAlignment.MiddleCenter;
            this.lblCourseName.Font = new System.Drawing.Font("Segoe UI", 14, FontStyle.Bold);

            // btnAddSession
            this.btnAddSession.Dock = DockStyle.Top;
            this.btnAddSession.Height = 40;
            this.btnAddSession.Text = "Thêm buổi học";
            this.btnAddSession.Click += new EventHandler(this.btnAddSession_Click);

            // flowPanelSessions
            this.flowPanelSessions.Dock = DockStyle.Fill;
            this.flowPanelSessions.AutoScroll = true;
            this.flowPanelSessions.FlowDirection = FlowDirection.TopDown;
            this.flowPanelSessions.WrapContents = false;

            // UcCourseDetail
            this.Controls.Add(this.flowPanelSessions);
            this.Controls.Add(this.btnAddSession);
            this.Controls.Add(this.lblCourseName);
            this.Size = new System.Drawing.Size(600, 600);
            this.ResumeLayout(false);
        }
    }
}