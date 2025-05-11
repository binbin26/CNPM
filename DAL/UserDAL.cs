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
                        // Sửa câu lệnh SQL để loại bỏ dấu nháy đơn xung quanh tên cột
                        string query = @"
                INSERT INTO Users (Username, PasswordHash, Role, FullName, Email, CreatedAt, IsActive) 
                VALUES (@Username, @PasswordHash, @Role, @FullName, @Email, @CreatedAt, @IsActive)";

                        SqlCommand cmd = new SqlCommand(query, conn, transaction);
                        cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = user.Username;
                        cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar).Value = user.Password; // Lưu mật khẩu gốc
                        cmd.Parameters.Add("@Role", SqlDbType.NVarChar).Value = user.Role;
                        cmd.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = user.FullName;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                        cmd.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = DateTime.Now; // Thời gian hiện tại
                        cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = true; // Mặc định là kích hoạt

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
    }
}

