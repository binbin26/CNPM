using CNPM.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CNPM.DAL
{
    public class EnrollmentDAL
    {
        public List<EnrolledStudent> GetEnrolledStudents(int courseID)
        {
            List<EnrolledStudent> students = new List<EnrolledStudent>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = @"
                    SELECT u.UserID, u.FullName, u.Email, ce.EnrollmentDate 
                    FROM CourseEnrollments ce
                    JOIN Users u ON ce.StudentID = u.UserID
                    WHERE ce.CourseID = @CourseID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseID", courseID);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    students.Add(new EnrolledStudent
                    {
                        StudentID = (int)reader["UserID"],
                        FullName = reader["FullName"].ToString(),
                        Email = reader["Email"].ToString(),
                        EnrollmentDate = (DateTime)reader["EnrollmentDate"]
                    });
                }
            }
            return students;
        }
    }
}