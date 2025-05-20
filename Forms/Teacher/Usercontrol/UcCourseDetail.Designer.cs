using System.Windows.Forms;
using System.Drawing;
using System;

namespace CNPM.Forms.Teacher
{
    partial class UcCourseDetail
    {
        private Label lblCourseName;
        private Button btnAddSession;
        private FlowLayoutPanel flowPanelSessions;

        private void InitializeComponent()
        {
            this.lblCourseName = new Label();
            this.btnAddSession = new Button();
            this.flowPanelSessions = new FlowLayoutPanel();

            // lblCourseName
            this.lblCourseName.Text = "Tên môn học";
            this.lblCourseName.Dock = DockStyle.Top;
            this.lblCourseName.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            this.lblCourseName.ForeColor = Color.FromArgb(30, 30, 60);
            this.lblCourseName.TextAlign = ContentAlignment.MiddleCenter;
            this.lblCourseName.Height = 60;
            this.lblCourseName.BackColor = Color.WhiteSmoke;

            // btnAddSession
            this.btnAddSession.Text = "➕ Thêm buổi học";
            this.btnAddSession.Dock = DockStyle.Top;
            this.btnAddSession.Height = 45;
            this.btnAddSession.BackColor = Color.SteelBlue;
            this.btnAddSession.ForeColor = Color.White;
            this.btnAddSession.FlatStyle = FlatStyle.Flat;
            this.btnAddSession.FlatAppearance.BorderSize = 0;
            this.btnAddSession.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            this.btnAddSession.Click += new EventHandler(this.btnAddSession_Click);

            // flowPanelSessions
            this.flowPanelSessions.Dock = DockStyle.Fill;
            this.flowPanelSessions.AutoScroll = true;
            this.flowPanelSessions.FlowDirection = FlowDirection.TopDown;
            this.flowPanelSessions.WrapContents = false;
            this.flowPanelSessions.Padding = new Padding(10);
            this.flowPanelSessions.BackColor = Color.White;

            // UcCourseDetail
            this.Controls.Add(this.flowPanelSessions);
            this.Controls.Add(this.btnAddSession);
            this.Controls.Add(this.lblCourseName);
            this.BackColor = Color.Gainsboro;
            this.Name = "UcCourseDetail";
            this.Size = new Size(980, 700);
        }
    }
}