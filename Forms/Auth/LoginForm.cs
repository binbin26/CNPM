using CNPM.BLL;
using System;
using CNPM.Utilities;
using System.Windows.Forms;
using CNPM.Forms.Admin;
using CNPM.Forms.Student;
using CNPM.Forms.Teacher;
using CNPM.Models.Users;

namespace CNPM.Forms.Auth
{
    public partial class LoginForm : Form
    {


        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserBLL _userBLL = new UserBLL();
            CourseBLL _courseBLL = new CourseBLL();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            try
            {
                int userId = _userBLL.GetUserId(username);
                var courses = _courseBLL.GetCoursesByTeacher(userId);
                if (_userBLL.ValidateLogin(username, password))
                {
                    // Get user role
                    string role = _userBLL.GetUserRole(username);
                    int courseId = courses[0].CourseID;
                    string courseName = courses[0].CourseName;
                    MessageBox.Show("Đăng nhập thành công!");
                    Logger.LogInfo($"Đăng nhập thành công với tài khoản: {username}");
                    // Navigate based on role
                    Form nextForm = null;
                    switch (role)
                    {

                        case "Admin":
                            nextForm = new AdminForm();
                            break;
                        case "Teacher":
                            nextForm = new MainForm(courses,userId,username);
                            break;
                        case "Student":
                            nextForm = new frmTongQuan(userId, username);
                            break;
                        default:
                            MessageBox.Show("Không xác định được vai trò người dùng.");
                            return;
                    }

                    this.Hide();
                    nextForm.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
                    Logger.LogInfo($"Đăng nhập thất bại");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}");
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            registrationForm.ShowDialog(); // Hiển thị dưới dạng modal
        }
    }
}
