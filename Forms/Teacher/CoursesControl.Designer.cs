namespace CNPM.Forms.Teacher
{
    partial class CoursesControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox listBoxCourses;
        private System.Windows.Forms.Button btnSelectCourse;
        private System.Windows.Forms.ListBox listBoxDocuments;

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
            this.listBoxCourses = new System.Windows.Forms.ListBox();
            this.btnSelectCourse = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // listBoxCourses
            this.listBoxCourses.FormattingEnabled = true;
            this.listBoxCourses.ItemHeight = 16;
            this.listBoxCourses.Location = new System.Drawing.Point(20, 20);
            this.listBoxCourses.Size = new System.Drawing.Size(300, 180);
            this.listBoxCourses.Items.AddRange(new object[] {
                "Lập trình C#",
                "Cơ sở dữ liệu",
                "Cấu trúc dữ liệu",
                "Hệ điều hành"
            });

            // btnSelectCourse
            this.btnSelectCourse.Text = "Chọn khóa học";
            this.btnSelectCourse.Location = new System.Drawing.Point(20, 220);
            this.btnSelectCourse.Size = new System.Drawing.Size(150, 30);
            this.btnSelectCourse.Click += new System.EventHandler(this.btnSelectCourse_Click);

            // CoursesControl
            this.Controls.Add(this.listBoxCourses);
            this.Controls.Add(this.btnSelectCourse);
            this.Name = "CoursesControl";
            this.Size = new System.Drawing.Size(800, 400);
            this.ResumeLayout(false);

            //add
            this.listBoxDocuments = new System.Windows.Forms.ListBox();
            this.listBoxDocuments.Location = new System.Drawing.Point(340, 20);
            this.listBoxDocuments.Size = new System.Drawing.Size(300, 180);
            this.Controls.Add(this.listBoxDocuments);
            this.listBoxCourses.SelectedIndexChanged += new System.EventHandler(this.listBoxCourses_SelectedIndexChanged);
        }
    }
}
