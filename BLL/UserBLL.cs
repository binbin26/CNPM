using CNPM.DAL;
using CNPM.Models.Users;
using CNPM.Utilities;
using System;
using System.Collections.Generic;
using System.IO;


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

        public bool Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Tên đăng nhập và mật khẩu không được để trống.");

            User user = _userDAL.GetUserByUsername(username);

            if (user == null)
                return false;

            // Ví dụ: logic hash password có thể được thêm ở đây
            return user.Password == password;
        }

        public bool RegisterUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
                throw new ArgumentException("Tên đăng nhập và mật khẩu là bắt buộc.");

            if (_userDAL.GetUserByUsername(user.Username) != null)
                throw new InvalidOperationException("Tên đăng nhập đã tồn tại.");

            // Áp dụng hash mật khẩu
            user.Password = HashPassword(user.Password);

            return _userDAL.AddUser(user);
        }

        private string HashPassword(string password)
        {
            // Thực hiện mã hóa mật khẩu nếu cần
            return password; // thay bằng thuật toán hash thực tế
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


        public bool ValidateLogin(string username, string password)
        {
            try
            {
                User user = _userDAL.GetUserByUsername(username);
                if (user == null || string.IsNullOrEmpty(user.PasswordHash))
                {
                    return false;
                }
                // So sánh password trực tiếp vì trong database đang lưu plain text
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

                return false; // Sai mật khẩu cũ
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

            string avatarFolderPath = @"C:\Users\baong\OneDrive\Desktop\CNPM\Resources\Avatar\Student";

            if (!Directory.Exists(avatarFolderPath))
            {
                Directory.CreateDirectory(avatarFolderPath);
            }

            // Tạo tên file mới để tránh trùng
            string fileName = $"{username}_{Guid.NewGuid()}{Path.GetExtension(selectedImagePath)}";
            string destinationPath = Path.Combine(avatarFolderPath, fileName);

            User user = _userDAL.GetUserByUsername(username);
            if (user == null) return false;

            string oldAvatarPath = user.AvatarPath;

            try
            {
                // Copy ảnh mới vào thư mục lưu trữ
                File.Copy(selectedImagePath, destinationPath, true);

                // Xóa ảnh cũ nếu có
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
    }
}