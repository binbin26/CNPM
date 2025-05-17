using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CNPM.Models.Assignments;

namespace CNPM.DAL
{
    public class AssignmentDAL
    {
        // (Nếu bạn đã có DatabaseHelper.GetConnection() thì không cần connectionString riêng)

        /// <summary>
        /// Thêm bài tập mới vào bảng Assignments
        /// </summary>
        public bool AddAssignment(Assignments assignment)
        {
            if (assignment == null) return false;

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = @"
                    INSERT INTO Assignments 
                        (CourseID, Title, Description, DueDate, MaxScore, CreatedBy, AssignmentType) 
                    VALUES 
                        (@CourseID, @Title, @Description, @DueDate, @MaxScore, @CreatedBy, @AssignmentType)";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", assignment.CourseID);
                    cmd.Parameters.AddWithValue("@Title", assignment.Title);
                    cmd.Parameters.AddWithValue("@Description", (object)assignment.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DueDate", assignment.DueDate);
                    cmd.Parameters.AddWithValue("@MaxScore", assignment.MaxScore);
                    cmd.Parameters.AddWithValue("@CreatedBy", assignment.CreatedBy);
                    cmd.Parameters.AddWithValue("@AssignmentType", (int)assignment.AssignmentType);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>
        /// Lấy toàn bộ bài tập theo CourseID
        /// </summary>
        public List<Assignments> GetAssignmentsByCourse(int courseID)
        {
            var list = new List<Assignments>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM Assignments WHERE CourseID = @CourseID";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseID);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapReaderToAssignment(reader));
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Lấy tất cả bài tập của các khóa học mà sinh viên (username) đã đăng ký
        /// </summary>
        public List<Assignments> GetAssignmentsForStudent(string username)
        {
            var list = new List<Assignments>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = @"
                    SELECT a.*
                      FROM Assignments a
                      JOIN CourseEnrollments ce ON ce.CourseID = a.CourseID
                      JOIN Users u             ON u.UserID = ce.StudentID
                     WHERE u.Username = @Username";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapReaderToAssignment(reader));
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Map 1 record SqlDataReader sang đối tượng Assignment
        /// </summary>
        private Assignments MapReaderToAssignment(SqlDataReader reader)
        {
            return new Assignments
            {
                AssignmentID = reader.GetInt32(reader.GetOrdinal("AssignmentID")),
                CourseID = reader.GetInt32(reader.GetOrdinal("CourseID")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description"))
                                   ? null
                                   : reader.GetString(reader.GetOrdinal("Description")),
                DueDate = reader.GetDateTime(reader.GetOrdinal("DueDate")),
                MaxScore = Convert.ToDecimal(reader["MaxScore"]),
                CreatedBy = reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                AssignmentType = (AssignmentTypes)reader.GetInt32(reader.GetOrdinal("AssignmentType"))
            };
        }
    }
}
