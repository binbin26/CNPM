using CNPM.DAL;
using CNPM.Models.Users;
using CNPM.Utilities;
using System;

namespace CNPM.BLL
{
    public class UserBLL
    {
        private readonly UserDAL _userDAL = new UserDAL();

        public bool AddUser(User user)
        {
            try
            {
                // Validate dữ liệu trước khi thêm
                if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                {
                    throw new ArgumentException("Username hoặc Password không được trống.");
                }

                if (!IsValidEmail(user.Email))
                {
                    throw new ArgumentException("Email không hợp lệ.");
                }

                // Kiểm tra trùng lặp Username
                if (_userDAL.GetUserByUsername(user.Username) != null)
                {
                    throw new ArgumentException($"Username '{user.Username}' đã tồn tại.");
                }

                // Gọi UserDAL để thêm user
                return _userDAL.AddUser(user);
            }
            catch
            {
                throw; // Ném ngoại lệ để xử lý ở tầng cao hơn
            }
        }



        // Phương thức kiểm tra email
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidateLogin(string username, string password)
        {
            try
            {
                User user = _userDAL.GetUserByUsername(username);
                if (user == null || string.IsNullOrEmpty(user.PasswordHash))
                {
                    return false;
                }
                bool isPasswordValid = password == user.PasswordHash;
                return isPasswordValid;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Lỗi đăng nhập: {ex.Message}");
                return false;
            }
        }
        public string GetUserRole(string username)
        {
            try
            {
                // Lấy thông tin người dùng từ UserDAL
                User user = _userDAL.GetUserByUsername(username);

                // Kiểm tra nếu người dùng không tồn tại
                if (user == null)
                {
                    Logger.LogError($"Không tìm thấy người dùng với username: {username}");
                    return null;
                }

                // Trả về vai trò của người dùng
                return user.Role;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Lỗi khi lấy vai trò người dùng: {ex.Message}");
                return null;
            }
        }
    }
}