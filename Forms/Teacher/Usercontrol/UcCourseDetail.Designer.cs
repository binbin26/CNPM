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
        private Button btnStat;
        private FlowLayoutPanel flowPanelSessions;
        private Button btnCourseProgress;

        private void InitializeComponent()
        {
            this.lblCourseName = new Label();
            this.btnAddSession = new IconButton();
            this.btnStat = new Button();
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
            btnCourseProgress = new Button();
            btnCourseProgress.Text = "📈 Tiến độ khóa học";
            btnCourseProgress.BackColor = Color.SeaGreen;
            btnCourseProgress.ForeColor = Color.White;
            btnCourseProgress.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCourseProgress.FlatStyle = FlatStyle.Flat;
            btnCourseProgress.FlatAppearance.BorderSize = 0;
            btnCourseProgress.Cursor = Cursors.Hand;
            btnCourseProgress.Height = 36;
            btnCourseProgress.Width = 200;
            btnCourseProgress.Margin = new Padding(10);
            btnCourseProgress.Click += (s, e) =>
            {
                new CourseProgress(currentCourse.CourseID, currentCourse.CourseName).ShowDialog();
            };
            //
            // btnStat
            // 
            this.btnStat.Text = "📊 Thống kê trắc nghiệm";
            this.btnStat.BackColor = Color.MediumSlateBlue;
            this.btnStat.ForeColor = Color.White;
            this.btnStat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnStat.FlatStyle = FlatStyle.Flat;
            this.btnStat.FlatAppearance.BorderSize = 0;
            this.btnStat.Cursor = Cursors.Hand;
            this.btnStat.Height = 36;
            this.btnStat.Width = 200;
            this.btnStat.Margin = new Padding(10);
            this.btnStat.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnStat.Click += new EventHandler(this.btnStat_Click);

            // UcCourseDetail
            this.Controls.Add(this.flowPanelSessions);
            this.Controls.Add(this.btnAddSession);
            this.Controls.Add(this.lblCourseName);
            this.Controls.Add(this.btnCourseProgress);
            this.BackColor = Color.Gainsboro;
            this.Name = "UcCourseDetail";
            this.Size = new Size(980, 700);
        }
    }
}