using CNPM.BLL;
using System;
using CNPM.Utilities;
using System.Windows.Forms;
using CNPM.Forms.Admin;
using CNPM.Forms.Student;
using CNPM.Forms.Teacher;
using CNPM.Models.Users;
using Microsoft.Extensions.DependencyInjection;

namespace CNPM.Forms.Auth
{
    public partial class LoginForm : Form
    {
        private readonly UserBLL _userBLL;
        private readonly IUserContext _userContext;

        public LoginForm()
        {
            InitializeComponent();
            _userBLL = new UserBLL();
            _userContext = (IUserContext)Program.ServiceProvider.GetService(typeof(IUserContext));
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            try
            {
                if (_userBLL.ValidateLogin(username, password))
                {
                    int userId = _userBLL.GetUserId(username);
                    string role = _userBLL.GetUserRole(username);    
                    User currentUser = _userBLL.GetUserByUsername(username);
                    _userContext.CurrentUser = currentUser;

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
                            nextForm = new TeacherForm(userId);
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
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đăng nhập: {ex.Message}");
                Logger.LogError($"Lỗi đăng nhập: {ex.Message}");
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            registrationForm.ShowDialog(); // Hiển thị dưới dạng modal
        }
    }
}
