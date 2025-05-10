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
                    Logger.LogError("Username hoặc Password không được trống.");
                    return false;
                }
                // ⭐⭐ THÊM CODE HASH MẬT KHẨU TẠI ĐÂY ⭐⭐
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password); // Hash mật khẩu gốc
                // Kiểm tra email hợp lệ (nếu cần)
                if (!IsValidEmail(user.Email))
                {
                    Logger.LogError("Email không hợp lệ.");
                    return false;
                }

                return _userDAL.AddUser(user);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Lỗi thêm User: {ex.Message}");
                return false; // Trả về false thay vì throw để tránh crash
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
    }
}