using CNPM.DAL;
using CNPM.Models.Users;
using CNPM.Utilities;
using System;
using System.Collections.Generic;

namespace CNPM.BLL
{
    public class UserBLL
    {
        private readonly UserDAL _userDAL;

        public UserBLL()
        {
            _userDAL = new UserDAL();
        }

        public List<User> GetAllUsers()
        {
            try
            {
                return _userDAL.GetAllUsers();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting users: " + ex.Message);
            }
        }

        public bool AddUser(User user)
        {
            try
            {
                // Validate user data
                if (string.IsNullOrWhiteSpace(user.Username) || 
                    string.IsNullOrWhiteSpace(user.Password) ||
                    string.IsNullOrWhiteSpace(user.Role))
                {
                    throw new Exception("Required fields cannot be empty");
                }

                // Check if username already exists
                var existingUser = _userDAL.GetUserByUsername(user.Username);
                if (existingUser != null)
                {
                    throw new Exception("Username already exists");
                }

                return _userDAL.AddUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding user: " + ex.Message);
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                // Validate user data
                if (string.IsNullOrWhiteSpace(user.Username) || 
                    string.IsNullOrWhiteSpace(user.Role))
                {
                    throw new Exception("Required fields cannot be empty");
                }

                // Check if user exists
                var existingUser = _userDAL.GetUserByUsername(user.Username);
                if (existingUser == null)
                {
                    throw new Exception("User not found");
                }

                return _userDAL.UpdateUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating user: " + ex.Message);
            }
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                return _userDAL.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting user: " + ex.Message);
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