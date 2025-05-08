using CNPM.Models.Users;
using CNPM.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data; // Thêm namespace này để sử dụng SqlTransaction

namespace CNPM.DAL
{
    public class UserDAL
    {
        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = "SELECT UserID, Username, Role, FullName, Email FROM Users";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UserID = (int)reader["UserID"],
                            Username = reader["Username"].ToString(),
                            Role = reader["Role"].ToString(),
                            FullName = reader["FullName"].ToString(),
                            Email = reader["Email"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Log lỗi và trả về danh sách rỗng
                Logger.LogError("Lỗi khi tải danh sách người dùng: " + ex.Message);
            }

            return users;
        }

        public bool AddUser(User user)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction(); // Bắt đầu transaction

                try
                {
                    string query = @"
                        INSERT INTO Users (Username, PasswordHash, Role, FullName, Email) 
                        VALUES (@Username, @PasswordHash, @Role, @FullName, @Email)";

                    SqlCommand cmd = new SqlCommand(query, conn, transaction);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@Role", user.Role);
                    cmd.Parameters.AddWithValue("@FullName", user.FullName);
                    cmd.Parameters.AddWithValue("@Email", user.Email);

                    cmd.ExecuteNonQuery();
                    transaction.Commit(); // Commit transaction
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback nếu có lỗi
                    Logger.LogError($"Lỗi khi thêm người dùng: {ex.Message}");
                    throw;
                }
            }
        }

        public User GetUserByUsername(string username)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM Users WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new User
                    {
                        UserID = (int)reader["UserID"],
                        Username = reader["Username"].ToString(),
                        PasswordHash = reader["PasswordHash"].ToString(),
                        Role = reader["Role"].ToString()
                    };
                }
                return null; // Không tìm thấy user
            }
        }
    }
}

