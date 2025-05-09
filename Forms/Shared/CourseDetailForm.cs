using CNPM.BLL;
using Microsoft.Extensions.DependencyInjection;          // ✅ thêm
using System;
using System.Windows.Forms;

namespace CNPM.Forms.Shared
{
    public partial class CourseDetailForm : Form
    {
        // ✅ bỏ khởi tạo trực tiếp, chuyển sang lấy từ DI
        private readonly CourseBLL _courseBLL;
        private readonly IUserContext _userContext;
        private int _courseID;
        public CourseDetailForm(int courseID)
        {
            InitializeComponent();
            _courseID = courseID;
            // ✅ Lấy các service đã đăng ký trong Program
            _courseBLL = Program.ServiceProvider.GetRequiredService<CourseBLL>();
            _userContext = Program.ServiceProvider.GetRequiredService<IUserContext>();

            LoadCourses();
        }

        private void LoadCourses()
        {
            dataGridViewCourses.DataSource = _courseBLL.GetAvailableCourses();
        }
        private void btnEnroll_Click(object sender, EventArgs e)
        {
            int selectedCourseID =
                (int)dataGridViewCourses.CurrentRow.Cells["CourseID"].Value;

            // ✅ Lấy ID sinh viên từ IUserContext
            int studentID = _userContext.CurrentUser.UserID;

            if (_courseBLL.EnrollStudent(studentID, selectedCourseID))
            {
                MessageBox.Show("Đăng ký thành công!");
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại!");
            }
        }
    }
}
