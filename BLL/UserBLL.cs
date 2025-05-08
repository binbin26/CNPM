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
                // Lấy thông tin user từ database
                User user = _userDAL.GetUserByUsername(username);

                // Kiểm tra user tồn tại và mật khẩu đúng
                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Log lỗi và trả về false
                Logger.LogError($"Lỗi xác thực đăng nhập: {ex.Message}");
                return false;
            }
        }
    }
}