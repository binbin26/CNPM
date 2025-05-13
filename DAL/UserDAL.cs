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
                                QueQuan = reader["QueQuan"] != DBNull.Value ? reader["QueQuan"].ToString() : null,
                                SoDienThoai = reader["SoDienThoai"] != DBNull.Value ? reader["SoDienThoai"].ToString() : null,
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null
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
        public bool UpdateAvatar(string username, byte[] avatarImage)
        {
            string query = "UPDATE Users SET AvatarPath = @AvatarPath WHERE Username = @Username";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.Add("@Avatar", SqlDbType.VarBinary).Value = avatarImage;
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public string GetAvatarPath(string username)
        {
            string query = "SELECT AvatarPath FROM Users WHERE Username = @Username";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    object result = cmd.ExecuteScalar();
                    return result != DBNull.Value ? result.ToString() : null;
                }
            }
        }

        public bool UpdateAvatarPath(string username, string newPath)
        {
            string query = "UPDATE Users SET AvatarPath = @AvatarPath WHERE Username = @Username";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@AvatarPath", newPath);
                    return cmd.ExecuteNonQuery() > 0;
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
                            QueQuan = reader["QueQuan"] != DBNull.Value ? reader["QueQuan"].ToString() : "",
                            SoDienThoai = reader["SoDienThoai"] != DBNull.Value ? reader["SoDienThoai"].ToString() : "",
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
                            IsActive = @IsActive,
                            QueQuan = @QueQuan,
                            SoDienThoai = @SoDienThoai
                        WHERE UserID = @UserID";

                        SqlCommand cmd = new SqlCommand(query, conn, transaction);
                        cmd.Parameters.AddWithValue("@UserID", user.UserID);
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@Role", user.Role);
                        cmd.Parameters.AddWithValue("@FullName", user.FullName ?? user.Username);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@QueQuan", user.QueQuan ?? "");
                        cmd.Parameters.AddWithValue("@SoDienThoai", user.SoDienThoai ?? "");
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

        public string GetPassword(string username)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT PasswordHash FROM Users WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }

        public bool ChangePassword(string username, string newPassword)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Users SET PasswordHash = @NewPassword WHERE Username = @Username";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }
    }
}

