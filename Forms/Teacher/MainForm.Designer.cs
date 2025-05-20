using System.Windows.Forms;
using System;
using System.Drawing;

namespace CNPM.Forms.Teacher
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelTabs;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Button btnCourses;
        private System.Windows.Forms.Button btnSubmissions;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTabs = new System.Windows.Forms.Panel();
            this.btnSubmissions = new System.Windows.Forms.Button();
            this.btnCourses = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.btnThongke = new System.Windows.Forms.Button();
            this.panelTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTabs
            // 
            this.panelTabs.BackColor = System.Drawing.Color.MidnightBlue;
            this.panelTabs.Controls.Add(this.btnThongke);
            this.panelTabs.Controls.Add(this.btnSubmissions);
            this.panelTabs.Controls.Add(this.btnCourses);
            this.panelTabs.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTabs.Location = new System.Drawing.Point(0, 0);
            this.panelTabs.Name = "panelTabs";
            this.panelTabs.Size = new System.Drawing.Size(200, 600);
            this.panelTabs.TabIndex = 0;
            // 
            // btnSubmissions
            // 
            this.btnSubmissions.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSubmissions.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSubmissions.Location = new System.Drawing.Point(0, 50);
            this.btnSubmissions.Name = "btnSubmissions";
            this.btnSubmissions.Size = new System.Drawing.Size(200, 58);
            this.btnSubmissions.TabIndex = 1;
            this.btnSubmissions.Text = "Bài nộp";
            this.btnSubmissions.UseVisualStyleBackColor = false;
            this.btnSubmissions.Click += new System.EventHandler(this.btnSubmissions_Click);
            // 
            // btnCourses
            // 
            this.btnCourses.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCourses.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCourses.Location = new System.Drawing.Point(0, 0);
            this.btnCourses.Name = "btnCourses";
            this.btnCourses.Size = new System.Drawing.Size(200, 50);
            this.btnCourses.TabIndex = 0;
            this.btnCourses.Text = "Khóa học";
            this.btnCourses.UseVisualStyleBackColor = false;
            this.btnCourses.Click += new System.EventHandler(this.btnCourses_Click);
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(200, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(800, 600);
            this.panelContent.TabIndex = 1;
            // 
            // btnThongke
            // 
            this.btnThongke.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnThongke.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnThongke.Location = new System.Drawing.Point(0, 108);
            this.btnThongke.Name = "btnThongke";
            this.btnThongke.Size = new System.Drawing.Size(200, 58);
            this.btnThongke.TabIndex = 2;
            this.btnThongke.Text = "Thống kê";
            this.btnThongke.UseVisualStyleBackColor = false;
            this.btnThongke.Click += new System.EventHandler(this.btnThongke_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelTabs);
            this.Name = "MainForm";
            this.Text = "Hệ thống quản lý học tập - Giảng viên";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelTabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Button btnThongke;
    }
}