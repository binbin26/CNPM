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
                return _userDAL.AddUser(user);
            }
            catch (Exception ex)
            {
                // Log lỗi (ví dụ: sử dụng NLog, Serilog)
                Logger.LogError($"Lỗi ở lớp BLL: {ex.Message}");

                // Ném lại exception để UI xử lý
                throw;
            }
        }

        public bool ValidateLogin(string username, string password)
        {
            try
            {
                User user = _userDAL.GetUserByUsername(username);
                if (user == null)
                {
                    Logger.LogError($"User {username} không tồn tại."); // Gọi Logger
                    return false;
                }

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
                if (!isPasswordValid)
                {
                    Logger.LogError($"Mật khẩu không đúng cho user {username}."); // Gọi Logger
                }

                return isPasswordValid;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Lỗi đăng nhập: {ex.Message}"); // Gọi Logger
                return false;
            }
        }
    }
}