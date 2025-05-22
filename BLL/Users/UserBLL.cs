using CNPM.DAL;
using CNPM.Models.Users;
using CNPM.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace CNPM.BLL
{
    public class UserBLL
    {
        private readonly UserDAL _userDAL;

        public UserBLL()
        {
            _userDAL = new UserDAL();
        }

        public UserBLL(UserDAL userDAL)
        {
            _userDAL = userDAL;
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

        public bool Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Tên đăng nhập và mật khẩu không được để trống.");

            User user = _userDAL.GetUserByUsername(username);

            if (user == null)
                return false;
            return user.Password == password;
        }

        public bool RegisterUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
                throw new ArgumentException("Tên đăng nhập và mật khẩu là bắt buộc.");

            if (_userDAL.GetUserByUsername(user.Username) != null)
                throw new InvalidOperationException("Tên đăng nhập đã tồn tại.");
            return _userDAL.AddUser(user);
        }

        public bool AddUser(User user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user.Username) ||
                    string.IsNullOrWhiteSpace(user.Password) ||
                    string.IsNullOrWhiteSpace(user.Role) ||
                    string.IsNullOrWhiteSpace(user.SoDienThoai) ||
                    string.IsNullOrWhiteSpace(user.QueQuan) ||
                    string.IsNullOrWhiteSpace(user.Email))
                {
                    throw new Exception("Các trường bắt buộc không được để trống.");
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(user.Username, @"^[a-zA-Z0-9]{5,20}$"))
                {
                    throw new Exception("Tên đăng nhập chỉ được chứa chữ cái và số, độ dài từ 5 đến 20 ký tự.");
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(user.Password, @"^(?=.*[A-Za-z])(?=.*\d).{6,}$"))
                {
                    throw new Exception("Mật khẩu phải có ít nhất 6 ký tự, bao gồm cả chữ cái và số.");
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    throw new Exception("Email không hợp lệ.");
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(user.SoDienThoai, @"^\d{10}$"))
                {
                    throw new Exception("Số điện thoại phải chứa 10 chữ số.");
                }
                var existingUser = _userDAL.GetUserByUsername(user.Username);
                if (existingUser != null)
                {
                    throw new Exception("Tên đăng nhập đã tồn tại.");
                }
                return _userDAL.AddUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm người dùng: " + ex.Message);
            }
        }

        public User GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username không được để trống.");
            }

            return _userDAL.GetUserByUsername(username);
        }


        public bool UpdateUser(User user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user.Username) || 
                    string.IsNullOrWhiteSpace(user.Role))
                {
                    throw new Exception("Required fields cannot be empty");
                }
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
                User user = _userDAL.GetUserByUsername(username);
                if (user == null)
                {
                    Logger.LogError($"Không tìm thấy người dùng với username: {username}");
                    return null;
                }
                return user.Role;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Lỗi khi lấy vai trò người dùng: {ex.Message}");
                return null;
            }
        }
        public bool UpdateUserAvatar(string username, byte[] avatarImage)
        {
            if (string.IsNullOrWhiteSpace(username) || avatarImage == null || avatarImage.Length == 0)
                throw new ArgumentException("Dữ liệu không hợp lệ");

            UserDAL userDAL = new UserDAL();
            return userDAL.UpdateAvatar(username, avatarImage);
        }

        public bool ChangeUserPassword(string username, string oldPassword, string newPassword)
            {
                string currentPassword = _userDAL.GetPassword(username);

                if (currentPassword == oldPassword)
                {
                    return _userDAL.ChangePassword(username, newPassword);
                }

                return false;
            }

        public DataTable GetAvailableCourses(int userId, string search)
        {
            return _userDAL.GetAvailableCourses(userId, search);
        }

        public List<StudentProgressDTO> GetStudentProgress(string username)
        {
            return _userDAL.GetProgressByUsername(username);
        }

        public bool UpdateUserAvatarPath(string username, string imagePath)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(imagePath))
                throw new ArgumentException("Dữ liệu không hợp lệ");

            UserDAL userDAL = new UserDAL();
            return userDAL.UpdateAvatarPath(username, imagePath);
        }

        public bool ChangeUserAvatar(string username, string selectedImagePath)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(selectedImagePath))
                throw new ArgumentException("Username hoặc ảnh không hợp lệ");

            string avatarFolderPath = Path.Combine(Application.StartupPath, "Avatars", "Student");

            if (!Directory.Exists(avatarFolderPath))
            {
                Directory.CreateDirectory(avatarFolderPath);
            }

            string fileName = $"{username}_{Guid.NewGuid()}{Path.GetExtension(selectedImagePath)}";
            string destinationPath = Path.Combine(avatarFolderPath, fileName);

            User user = _userDAL.GetUserByUsername(username);
            if (user == null) return false;

            string oldAvatarPath = user.AvatarPath;

            try
            {
                File.Copy(selectedImagePath, destinationPath, true);
                if (!string.IsNullOrWhiteSpace(oldAvatarPath) && File.Exists(oldAvatarPath))
                {
                    try
                    {
                        // Đảm bảo file không bị khóa
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        File.Delete(oldAvatarPath);
                    }
                    catch (IOException ex)
                    {
                        Logger.LogError($"Không thể xóa ảnh cũ: {ex.Message}");
                        // Không throw lỗi ở đây để tránh gián đoạn quá trình cập nhật
                    }
                }
                // Cập nhật đường dẫn mới vào DB
                return _userDAL.UpdateAvatarPath(username, destinationPath);
            }
            catch (Exception ex)
            {
                Logger.LogError("Lỗi khi cập nhật ảnh đại diện: " + ex.Message);
                return false;
            }
        }

        public int GetUserId(string username)
        {
            try
            {
                User user = _userDAL.GetUserByUsername(username);
                if (user == null)
                {
                    Logger.LogError($"Không tìm thấy người dùng với username: {username}");
                    return -1;
                }
                return user.UserID;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Lỗi khi lấy ID người dùng: {ex.Message}");
                return -1;
            }
        }

        public User GetAdminInfo()
        {
            return _userDAL.GetAdminInfo();
        }
    }
}