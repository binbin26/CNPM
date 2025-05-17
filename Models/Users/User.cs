using System;

namespace CNPM.Models.Users
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public string QueQuan { get; set; }
        public string SoDienThoai { get; set; }
        public string AvatarPath { get; set; }
    }
}
