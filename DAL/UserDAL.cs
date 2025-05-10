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
                using (SqlTransaction transaction = conn.BeginTransaction()) // Thêm using
                {
                    try
                    {
                        string query = @"
                    INSERT INTO Users (Username, PasswordHash, Role, FullName, Email) 
                    VALUES (@Username, @PasswordHash, @Role, @FullName, @Email)";

                        SqlCommand cmd = new SqlCommand(query, conn, transaction);
                        cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = user.Username; // Xác định kiểu dữ liệu
                        cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar).Value = user.PasswordHash;
                        cmd.Parameters.Add("@Role", SqlDbType.NVarChar).Value = user.Role;
                        cmd.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = user.FullName;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;

                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Logger.LogError($"Lỗi khi thêm người dùng: {ex.Message}");
                        return false; // Trả về false thay vì throw
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
    }
}

