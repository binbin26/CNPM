using CNPM.BLL;
using System;
using CNPM.Utilities;
using System.Windows.Forms;

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
                    MessageBox.Show("Đăng nhập thành công!");
                    Logger.LogInfo($"Đăng nhập thành công với tài khoản: {username}");
                    // Mở form chính
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
