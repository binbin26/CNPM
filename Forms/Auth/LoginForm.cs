using CNPM.BLL;
using System;
using CNPM.Utilities;
using System.Windows.Forms;
using CNPM.Forms.Admin;
using CNPM.Forms.Student;
using CNPM.Forms.Teacher;

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
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            try
            {

                if (_userBLL.ValidateLogin(username, password))
                {
                    // Get user role
                    string role = _userBLL.GetUserRole(username); // Implement this method in UserBLL
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
                            nextForm = new MainForm();
                            break;
                        case "Student":
                            int userId = _userBLL.GetUserId(username); // Lấy userId của người dùng
                            nextForm = new frmTongQuan(userId);
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
