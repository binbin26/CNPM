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
            this.lblCourseName = new System.Windows.Forms.Label();
            this.btnAddSession = new System.Windows.Forms.Button();
            this.flowPanelSessions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnStat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCourseName
            // 
            this.lblCourseName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCourseName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblCourseName.Location = new System.Drawing.Point(0, 0);
            this.lblCourseName.Name = "lblCourseName";
            this.lblCourseName.Size = new System.Drawing.Size(600, 40);
            this.lblCourseName.TabIndex = 2;
            this.lblCourseName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAddSession
            // 
            this.btnAddSession.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddSession.Location = new System.Drawing.Point(0, 40);
            this.btnAddSession.Name = "btnAddSession";
            this.btnAddSession.Size = new System.Drawing.Size(600, 40);
            this.btnAddSession.TabIndex = 1;
            this.btnAddSession.Text = "Thêm buổi học";
            this.btnAddSession.Click += new System.EventHandler(this.btnAddSession_Click);
            // 
            // flowPanelSessions
            // 
            this.flowPanelSessions.AutoScroll = true;
            this.flowPanelSessions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanelSessions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanelSessions.Location = new System.Drawing.Point(0, 80);
            this.flowPanelSessions.Name = "flowPanelSessions";
            this.flowPanelSessions.Size = new System.Drawing.Size(600, 520);
            this.flowPanelSessions.TabIndex = 0;
            this.flowPanelSessions.WrapContents = false;
            // 
            // btnStat
            // 
            this.btnStat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnStat.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.btnStat.Location = new System.Drawing.Point(344, 3);
            this.btnStat.Name = "btnStat";
            this.btnStat.Size = new System.Drawing.Size(253, 31);
            this.btnStat.TabIndex = 0;
            this.btnStat.Text = "Thống kê bài tập trắc nghiệm";
            this.btnStat.UseVisualStyleBackColor = true;
            this.btnStat.Click += new System.EventHandler(this.btnStat_Click);
            // 
            // UcCourseDetail
            // 
            this.Controls.Add(this.btnStat);
            this.Controls.Add(this.flowPanelSessions);
            this.Controls.Add(this.btnAddSession);
            this.Controls.Add(this.lblCourseName);
            this.Name = "UcCourseDetail";
            this.Size = new System.Drawing.Size(600, 600);
            this.ResumeLayout(false);

        }

        private Button btnStat;
    }
}