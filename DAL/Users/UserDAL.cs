using CNPM.Models.Users;
using CNPM.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;


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
                    string query = "SELECT UserID, Username, Role, FullName, Email, QueQuan, SoDienThoai FROM Users";
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
                        INSERT INTO Users (Username, PasswordHash, Role, FullName, Email, CreatedAt, IsActive, QueQuan, SoDienThoai) 
                        VALUES (@Username, @PasswordHash, @Role, @FullName, @Email, @CreatedAt, @IsActive, @QueQuan, @SoDienThoai)";

                        SqlCommand cmd = new SqlCommand(query, conn, transaction);
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@PasswordHash", user.Password);
                        cmd.Parameters.AddWithValue("@Role", user.Role);
                        cmd.Parameters.AddWithValue("@FullName", user.FullName ?? user.Username); // Sử dụng Username nếu FullName là null
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                        cmd.Parameters.AddWithValue("@QueQuan", user.QueQuan ?? "");
                        cmd.Parameters.AddWithValue("@SoDienThoai", user.SoDienThoai ?? "");
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
                            SoDienThoai = @SoDienThoai,
                            AvatarPath = @AvatarPath
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
                        cmd.Parameters.AddWithValue("@AvatarPath", (object)user.AvatarPath ?? DBNull.Value);

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

        public DataTable GetAvailableCourses(int userId, string search)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT c.CourseID, c.CourseName, u.FullName AS Teacher, 
                               c.MaxEnrollment - ISNULL(e.NumEnrolled,0) AS SlotsLeft, EndDate,
                               CASE 
                                   WHEN ce.EnrollmentID IS NOT NULL THEN N'Đã đăng ký'
                                   WHEN c.EndDate < GETDATE() THEN N'Hết hạn'
                                   WHEN c.MaxEnrollment - ISNULL(e.NumEnrolled,0) <= 0 THEN N'Hết chỗ'
                                   ELSE N'Chưa đăng ký'
                               END AS Status
                        FROM Courses c
                        JOIN Users u ON c.TeacherID = u.UserID
                        LEFT JOIN (
                            SELECT CourseID, COUNT(*) AS NumEnrolled
                            FROM CourseEnrollments
                            GROUP BY CourseID
                        ) e ON c.CourseID = e.CourseID
                        LEFT JOIN CourseEnrollments ce ON ce.CourseID = c.CourseID AND ce.StudentID = @UserID
                        WHERE (@Search = '' OR c.CourseName LIKE '%' + @Search + '%')
                    ";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.Parameters.AddWithValue("@Search", search);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
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

        //Report 1
        public List<StudentProgressDTO> GetProgressByUsername(string username)
        {
            List<StudentProgressDTO> result = new List<StudentProgressDTO>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = @"
            SELECT 
                c.CourseName,
                ISNULL(total.TotalAssignments, 0) AS TotalAssignments,
                ISNULL(sub.SubmittedAssignments, 0) AS SubmittedAssignments,
                g.Score AS Grade
            FROM Users u
            INNER JOIN CourseEnrollments ce ON u.UserID = ce.StudentID
            INNER JOIN Courses c ON ce.CourseID = c.CourseID
            LEFT JOIN (
                SELECT a.CourseID, COUNT(*) AS TotalAssignments
                FROM Assignments a
                GROUP BY a.CourseID
            ) total ON c.CourseID = total.CourseID
            LEFT JOIN (
                SELECT a.CourseID, ss.StudentID, COUNT(*) AS SubmittedAssignments
                FROM StudentSubmissions ss
                INNER JOIN Assignments a ON ss.AssignmentID = a.AssignmentID
                GROUP BY a.CourseID, ss.StudentID
            ) sub ON sub.CourseID = c.CourseID AND sub.StudentID = u.UserID
            LEFT JOIN Grades g ON g.CourseID = c.CourseID AND g.StudentID = u.UserID
            WHERE u.Username = @Username";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var total = Convert.ToInt32(reader["TotalAssignments"]);
                        var submitted = Convert.ToInt32(reader["SubmittedAssignments"]);
                        double? grade = reader["Grade"] != DBNull.Value ? Convert.ToDouble(reader["Grade"]) : (double?)null;
                        double completion = total == 0 ? 0 : Math.Round((double)submitted * 100 / total, 2);

                        string status = grade.HasValue
                            ? (grade >= 8 ? "Hoàn thành tốt" : (grade >= 5 ? "Cần cố gắng" : "Chưa đạt"))
                            : "Chưa có điểm";

                        result.Add(new StudentProgressDTO
                        {
                            CourseName = reader["CourseName"].ToString(),
                            TotalAssignments = total,
                            SubmittedAssignments = submitted,
                            CompletionRate = completion,
                            Grade = grade,
                            Status = status
                        });
                    }
                }
            }

            return result;
        }

        public User GetAdminInfo()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT TOP 1 * FROM Users WHERE Role = 'Admin'";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            UserID = reader["UserID"] != DBNull.Value ? (int)reader["UserID"] : 0,
                            Username = reader["Username"]?.ToString(),
                            Role = reader["Role"]?.ToString(),
                            FullName = reader["FullName"]?.ToString(),
                            QueQuan = reader["QueQuan"]?.ToString(),
                            SoDienThoai = reader["SoDienThoai"]?.ToString(),
                            Email = reader["Email"]?.ToString()
                        };
                    }
                }
            }
            return null;
        }
    }
}