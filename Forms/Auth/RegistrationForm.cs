using CNPM.BLL;
using CNPM.Models.Users;
using System;
using System.Windows.Forms;
using CNPM.Utilities;
namespace CNPM.Forms.Auth
{
    public partial class RegistrationForm: Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text;
                string password = txtPassword.Text;
                string fullName = txtFullName.Text;
                string email = txtEmail.Text;
                string city = txtCity.Text;
                string phone = txtPhone.Text;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newUser = new User
                {
                    Username = username,
                    Password = password,
                    Role = "Student",
                    FullName = fullName,
                    QueQuan = city,
                    SoDienThoai = phone,
                    Email = email
                };
                UserBLL userBLL = new UserBLL();
                bool isSuccess = userBLL.AddUser(newUser);
                if (isSuccess)
                {
                    MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đăng ký thất bại! Vui lòng kiểm tra lại thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Lỗi trong RegistrationForm: {ex.Message}");
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
