using CNPM.Models.Courses;
using CNPM.Models;
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
                        TeacherID = (int)reader["TeacherID"],
                        StartDate = (DateTime)reader["StartDate"],
                        EndDate = (DateTime)reader["EndDate"]
                    });
                }
            }
            return courses;
        }
        public bool EnrollStudent(int studentID, int courseID)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                // Giả sử bạn có bảng Enrollments với các cột: StudentID, CourseID
                string checkQuery = "SELECT COUNT(*) FROM CourseEnrollments WHERE StudentID = @StudentID AND CourseID = @CourseID";
                string insertQuery = "INSERT INTO CourseEnrollments (StudentID, CourseID) VALUES (@StudentID, @CourseID)";

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
            // Lấy danh sách khóa học do Giảng viên phụ trách
        public List<Course> GetCoursesByTeacher(int teacherID)
            {
                List<Course> courses = new List<Course>();
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    string query = "SELECT * FROM Courses WHERE TeacherID = @TeacherID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TeacherID", teacherID);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        courses.Add(new Course
                        {
                            CourseID = (int)reader["CourseID"],
                            CourseCode = reader["CourseCode"].ToString(),
                            CourseName = reader["CourseName"].ToString(),
                            TeacherID = (int)reader["TeacherID"],
                            StartDate = (DateTime)reader["StartDate"],
                            EndDate = (DateTime)reader["EndDate"]
                        });
                    }
                }
                return courses;
        }

        public List<Course> GetCoursesByStudent(int userId)
        {
            List<Course> courses = new List<Course>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT c.*
                         FROM Courses c
                         JOIN StudentCourse sc ON c.CourseID = sc.CourseID
                         WHERE sc.UserID = @userID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userID", userId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Course course = new Course
                    {
                        CourseID = Convert.ToInt32(reader["CourseID"]),
                        CourseName = reader["CourseName"].ToString(),
                        // thêm các cột khác nếu có
                    };
                    courses.Add(course);
                }
            }
            return courses;
        }


        public Course GetCourseByID(int courseID)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM Courses WHERE CourseID = @CourseID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseID", courseID);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Course
                    {
                        CourseID = (int)reader["CourseID"],
                        CourseCode = reader["CourseCode"].ToString(),
                        CourseName = reader["CourseName"].ToString(),
                        TeacherID = (int)reader["TeacherID"],
                        StartDate = (DateTime)reader["StartDate"],
                        EndDate = (DateTime)reader["EndDate"]
                    };
                }
                return null;
            }
        }

        public bool AddCourse(Course course)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = @"INSERT INTO Courses (CourseCode, CourseName, TeacherID, StartDate, EndDate) 
                               VALUES (@CourseCode, @CourseName, @TeacherID, @StartDate, @EndDate)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseCode", course.CourseCode);
                cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                cmd.Parameters.AddWithValue("@TeacherID", course.TeacherID);
                cmd.Parameters.AddWithValue("@StartDate", course.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", course.EndDate);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


        public List<EnrolledStudent> GetEnrolledStudents(int courseId)
        {
            List<EnrolledStudent> students = new List<EnrolledStudent>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = @"
                    SELECT u.UserID as StudentID, u.FullName, u.Email, ce.EnrollmentDate
                    FROM Users u
                    INNER JOIN CourseEnrollments ce ON u.UserID = ce.StudentID
                    WHERE ce.CourseID = @CourseID AND u.Role = 'Student'";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseID", courseId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    students.Add(new EnrolledStudent
                    {
                        StudentID = (int)reader["StudentID"],
                        FullName = reader["FullName"].ToString(),
                        Email = reader["Email"].ToString(),
                        EnrollmentDate = (DateTime)reader["EnrollmentDate"]
                    });
                }
            }
            return students;
        }

        public bool RemoveStudent(int studentId, int courseId)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "DELETE FROM CourseEnrollments WHERE StudentID = @StudentID AND CourseID = @CourseID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentID", studentId);
                cmd.Parameters.AddWithValue("@CourseID", courseId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
