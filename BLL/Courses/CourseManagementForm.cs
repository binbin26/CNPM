using CNPM.Forms.Teacher;
using CNPM.Models.Courses;
using CNPM.Forms.Shared;
using System;
using System.Windows.Forms;
using CNPM.Forms;

namespace CNPM.BLL.Courses
{
    public partial class CourseManagementForm : Form
    {
        private readonly int _courseID;
        private readonly CourseBLL _courseBLL;

        public CourseManagementForm(int courseID)
        {
            InitializeComponent();
            _courseID = courseID;
            LoadCourseDetails();
        }

        // Tải thông tin khóa học
        private void LoadCourseDetails()
        {
            Course course = _courseBLL.GetCourseByID(_courseID);
            lblCourseCode.Text = course.CourseCode;
            lblCourseName.Text = course.CourseName;
            lblStartDate.Text = course.StartDate.ToShortDateString();
            lblEndDate.Text = course.EndDate.ToShortDateString();
        }

        // Nút "Thêm Tài Liệu"
        private void btnAddDocument_Click(object sender, EventArgs e)
        {
            // Mở form upload tài liệu
            UploadContentForm form = new UploadContentForm(_courseID);
            form.ShowDialog();
        }

        // Nút "Xem Sinh Viên Đăng Ký"
        private void btnViewStudents_Click(object sender, EventArgs e)
        {
            // Mở form danh sách sinh viên
            EnrolledStudentsForm form = new EnrolledStudentsForm(_courseID);
            form.ShowDialog();
        }
    }
}
