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
                    using (SqlDataReader reader = cmd.ExecuteReader()) // Thêm using
                    {
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
                    conn.Close(); // Đảm bảo đóng kết nối sau khi hoàn thành
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Lỗi khi tải danh sách người dùng: {ex.Message}");
                throw; // Ném lại exception để xử lý ở tầng cao hơn
            }

            return users;
        }

        public bool AddUser(User user)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string query = @"
                        INSERT INTO Users (Username, PasswordHash, Role, FullName, Email, CreatedAt, IsActive) 
                        VALUES (@Username, @PasswordHash, @Role, @FullName, @Email, @CreatedAt, @IsActive)";

                        SqlCommand cmd = new SqlCommand(query, conn, transaction);
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@PasswordHash", user.Password);
                        cmd.Parameters.AddWithValue("@Role", user.Role);
                        cmd.Parameters.AddWithValue("@FullName", user.FullName ?? user.Username); // Sử dụng Username nếu FullName là null
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                        cmd.Parameters.AddWithValue("@IsActive", true);

                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                        conn.Close();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw; // Ném ngoại lệ để xử lý ở tầng cao hơn
                    }
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
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            UserID = reader["UserID"] != DBNull.Value ? (int)reader["UserID"] : 0,
                            Username = reader["Username"] != DBNull.Value ? reader["Username"].ToString() : "",
                            PasswordHash = reader["PasswordHash"] != DBNull.Value ? reader["PasswordHash"].ToString() : "",
                            Role = reader["Role"] != DBNull.Value ? reader["Role"].ToString() : "",
                            FullName = reader["FullName"] != DBNull.Value ? reader["FullName"].ToString() : "",
                            Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : ""
                        };
                    }
                    return null;
                }
            }
        }

        public bool UpdateUser(User user)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string query = @"
                        UPDATE Users 
                        SET Username = @Username,
                            Role = @Role,
                            FullName = @FullName,
                            Email = @Email,
                            IsActive = @IsActive
                        WHERE UserID = @UserID";

                        SqlCommand cmd = new SqlCommand(query, conn, transaction);
                        cmd.Parameters.AddWithValue("@UserID", user.UserID);
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@Role", user.Role);
                        cmd.Parameters.AddWithValue("@FullName", user.FullName ?? user.Username);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@IsActive", true);

                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool DeleteUser(int userId)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string query = "DELETE FROM Users WHERE UserID = @UserID";
                        SqlCommand cmd = new SqlCommand(query, conn, transaction);
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}

