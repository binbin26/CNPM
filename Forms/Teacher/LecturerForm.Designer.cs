namespace CNPM.Forms.Teacher
{
    partial class LecturerForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelCourses = new System.Windows.Forms.Panel();
            this.labelCourses = new System.Windows.Forms.Label();
            this.panelUpload = new System.Windows.Forms.Panel();
            this.labelUpload = new System.Windows.Forms.Label();
            this.panelExam = new System.Windows.Forms.Panel();
            this.labelExam = new System.Windows.Forms.Label();
            this.panelScoreReport = new System.Windows.Forms.Panel();
            this.labelScore = new System.Windows.Forms.Label();
            this.panelLeft.SuspendLayout();
            this.panelCourses.SuspendLayout();
            this.panelUpload.SuspendLayout();
            this.panelExam.SuspendLayout();
            this.panelScoreReport.SuspendLayout();
            this.SuspendLayout();

            // panelLeft
            this.panelLeft.BackColor = System.Drawing.Color.LightGray;
            this.panelLeft.Controls.Add(this.panelScoreReport);
            this.panelLeft.Controls.Add(this.panelExam);
            this.panelLeft.Controls.Add(this.panelUpload);
            this.panelLeft.Controls.Add(this.panelCourses);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Width = 200;

            // panelMain
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.BackColor = System.Drawing.Color.White;

            // panelCourses
            this.panelCourses.BackColor = System.Drawing.Color.Gray;
            this.panelCourses.Controls.Add(this.labelCourses);
            this.panelCourses.Height = 50;
            this.panelCourses.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCourses.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelCourses.Click += new System.EventHandler(this.panelCourses_Click);

            // labelCourses
            this.labelCourses.Text = "Danh sách khóa học";
            this.labelCourses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCourses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelCourses.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.labelCourses.Click += new System.EventHandler(this.panelCourses_Click);

            // panelUpload
            this.panelUpload.BackColor = System.Drawing.Color.Gray;
            this.panelUpload.Controls.Add(this.labelUpload);
            this.panelUpload.Height = 50;
            this.panelUpload.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUpload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelUpload.Click += new System.EventHandler(this.panelUpload_Click);

            // labelUpload
            this.labelUpload.Text = "Tải tài liệu/Bài tập";
            this.labelUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelUpload.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelUpload.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.labelUpload.Click += new System.EventHandler(this.panelUpload_Click);

            // panelExam
            this.panelExam.BackColor = System.Drawing.Color.Gray;
            this.panelExam.Controls.Add(this.labelExam);
            this.panelExam.Height = 50;
            this.panelExam.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelExam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelExam.Click += new System.EventHandler(this.panelExam_Click);

            // labelExam
            this.labelExam.Text = "Đăng bài kiểm tra";
            this.labelExam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelExam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelExam.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.labelExam.Click += new System.EventHandler(this.panelExam_Click);

            // panelScoreReport
            this.panelScoreReport.BackColor = System.Drawing.Color.Gray;
            this.panelScoreReport.Controls.Add(this.labelScore);
            this.panelScoreReport.Height = 50;
            this.panelScoreReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelScoreReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelScoreReport.Click += new System.EventHandler(this.panelScoreReport_Click);

            // labelScore
            this.labelScore.Text = "Báo cáo điểm";
            this.labelScore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelScore.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.labelScore.Click += new System.EventHandler(this.panelScoreReport_Click);

            // LecturerForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelLeft);
            this.Name = "LecturerForm";
            this.Text = "Quản lý học tập - Giảng viên";
            this.panelLeft.ResumeLayout(false);
            this.panelCourses.ResumeLayout(false);
            this.panelUpload.ResumeLayout(false);
            this.panelExam.ResumeLayout(false);
            this.panelScoreReport.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelCourses;
        private System.Windows.Forms.Label labelCourses;
        private System.Windows.Forms.Panel panelUpload;
        private System.Windows.Forms.Label labelUpload;
        private System.Windows.Forms.Panel panelExam;
        private System.Windows.Forms.Label labelExam;
        private System.Windows.Forms.Panel panelScoreReport;
        private System.Windows.Forms.Label labelScore;
    }
}