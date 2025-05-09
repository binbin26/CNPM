using CNPM.Models.Courses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.DAL
{
    public class CourseDAL
    {
        public List<Course> GetAllCourses()
        {
            List<Course> courses = new List<Course>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM Courses";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    courses.Add(new Course
                    {
                        CourseID = (int)reader["CourseID"],
                        CourseCode = reader["CourseCode"].ToString(),
                        CourseName = reader["CourseName"].ToString(),
                        TeacherID = (int)reader["TeacherID"]
                    });
                }
            }
            return courses;
        }
        // ✅ Bổ sung hàm EnrollStudent vào cuối lớp
        public bool EnrollStudent(int studentID, int courseID)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                // Giả sử bạn có bảng Enrollments với các cột: StudentID, CourseID
                string checkQuery = "SELECT COUNT(*) FROM Enrollments WHERE StudentID = @StudentID AND CourseID = @CourseID";
                string insertQuery = "INSERT INTO Enrollments (StudentID, CourseID) VALUES (@StudentID, @CourseID)";

                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@StudentID", studentID);
                checkCmd.Parameters.AddWithValue("@CourseID", courseID);

                conn.Open();
                int exists = (int)checkCmd.ExecuteScalar();

                if (exists > 0)
                {
                    return false; // Đã ghi danh rồi
                }

                SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@StudentID", studentID);
                insertCmd.Parameters.AddWithValue("@CourseID", courseID);

                int rowsAffected = insertCmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
