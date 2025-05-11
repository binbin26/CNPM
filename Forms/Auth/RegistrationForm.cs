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

        // Sự kiện khi người dùng nhấn nút "Đăng ký"

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form và quay lại LoginForm
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Lấy dữ liệu từ các control trên form
                string username = txtUsername.Text;
                string password = txtPassword.Text;
                string fullName = txtFullName.Text;
                string email = txtEmail.Text;

                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Tạo đối tượng User từ dữ liệu nhập vào
                var newUser = new User
                {
                    Username = username,
                    Password = password, // Mật khẩu gốc (sẽ được hash trong BLL)
                    Role = "Student",     // Mặc định role là Student khi đăng ký
                    FullName = fullName,
                    Email = email
                };

                // 3. Gọi BLL để thêm user
                UserBLL userBLL = new UserBLL();
                bool isSuccess = userBLL.AddUser(newUser);

                // 4. Hiển thị kết quả
                if (isSuccess)
                {
                    MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Đóng form sau khi đăng ký thành công
                }
                else
                {
                    MessageBox.Show("Đăng ký thất bại! Vui lòng kiểm tra lại thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Lỗi trong RegistrationForm: {ex.Message}");
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
