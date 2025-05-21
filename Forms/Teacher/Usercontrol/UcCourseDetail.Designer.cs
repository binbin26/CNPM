using System.Windows.Forms;
using System.Drawing;
using System;
using FontAwesome.Sharp;

namespace CNPM.Forms.Teacher
{
    partial class UcCourseDetail
    {
        private Label lblCourseName;
        private IconButton btnAddSession;
        private FlowLayoutPanel flowPanelSessions;
        private IconButton btnCourseProgress;
        private FlowLayoutPanel panelButtons;

        private void InitializeComponent()
        {
            this.lblCourseName = new Label();
            this.btnAddSession = new IconButton();
            this.flowPanelSessions = new FlowLayoutPanel();
            this.btnCourseProgress = new IconButton();
            this.panelButtons = new FlowLayoutPanel();

            // lblCourseName
            this.lblCourseName.Text = "Tên môn học";
            this.lblCourseName.Dock = DockStyle.Top;
            this.lblCourseName.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            this.lblCourseName.ForeColor = Color.FromArgb(30, 30, 60);
            this.lblCourseName.TextAlign = ContentAlignment.MiddleCenter;
            this.lblCourseName.Height = 60;
            this.lblCourseName.BackColor = Color.WhiteSmoke;

            // btnAddSession
            btnAddSession = new IconButton();
            btnAddSession.Text = " Thêm buổi học";
            btnAddSession.IconChar = IconChar.PlusCircle;
            btnAddSession.IconColor = Color.White;
            btnAddSession.IconFont = IconFont.Auto;
            btnAddSession.IconSize = 22;
            btnAddSession.BackColor = Color.FromArgb(100, 149, 237); // CornflowerBlue
            btnAddSession.ForeColor = Color.White;
            btnAddSession.FlatStyle = FlatStyle.Flat;
            btnAddSession.FlatAppearance.BorderSize = 0;
            btnAddSession.Height = 45;
            btnAddSession.Dock = DockStyle.Top;
            btnAddSession.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnAddSession.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddSession.ImageAlign = ContentAlignment.MiddleLeft;
            btnAddSession.TextAlign = ContentAlignment.MiddleLeft;
            btnAddSession.Padding = new Padding(12, 0, 0, 0);
            btnAddSession.Cursor = Cursors.Hand;
            this.btnAddSession.Click += new EventHandler(this.btnAddSession_Click);

            // flowPanelSessions
            this.flowPanelSessions.Dock = DockStyle.Fill;
            this.flowPanelSessions.AutoScroll = true;
            this.flowPanelSessions.FlowDirection = FlowDirection.TopDown;
            this.flowPanelSessions.WrapContents = false;
            this.flowPanelSessions.Padding = new Padding(10);
            this.flowPanelSessions.BackColor = Color.White;
            this.flowPanelSessions.AutoSize = false; // 👈 Đảm bảo không co giãn theo nội dung
            this.flowPanelSessions.AutoScrollMargin = new Size(0, 10); // 👈 Scroll mượt hơn

            // btnCourseProgress
            btnCourseProgress = new IconButton();
            btnCourseProgress.Text = " Tiến độ khóa học";
            btnCourseProgress.IconChar = IconChar.ChartLine;
            btnCourseProgress.IconColor = Color.White;
            btnCourseProgress.IconFont = IconFont.Auto;
            btnCourseProgress.IconSize = 20;
            btnCourseProgress.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCourseProgress.ImageAlign = ContentAlignment.MiddleLeft;
            btnCourseProgress.TextAlign = ContentAlignment.MiddleLeft;
            btnCourseProgress.BackColor = Color.SeaGreen;
            btnCourseProgress.ForeColor = Color.White;
            btnCourseProgress.FlatStyle = FlatStyle.Flat;
            btnCourseProgress.FlatAppearance.BorderSize = 0;
            btnCourseProgress.Height = 40;
            btnCourseProgress.Width = 220;
            btnCourseProgress.Padding = new Padding(10, 5, 10, 5);
            btnCourseProgress.Cursor = Cursors.Hand;
            btnCourseProgress.Click += (s, e) =>
            {
                new CourseProgress(currentCourse.CourseID, currentCourse.CourseName).ShowDialog();
            };

            // panelButtons
            panelButtons.Dock = DockStyle.Top;
            panelButtons.Height = 50;
            panelButtons.FlowDirection = FlowDirection.LeftToRight;
            panelButtons.Padding = new Padding(10);
            panelButtons.BackColor = Color.WhiteSmoke;
            panelButtons.Controls.Add(btnCourseProgress);

            // UcCourseDetail
            this.Controls.Add(this.flowPanelSessions);
            this.Controls.Add(this.btnAddSession);
            this.Controls.Add(this.lblCourseName);
            this.Controls.Add(this.panelButtons);
            this.BackColor = Color.Gainsboro;
            this.Name = "UcCourseDetail";
            this.Size = new Size(980, 700);
        }
    }
}