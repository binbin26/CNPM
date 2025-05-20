using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CNPM.Models.Assignments;
using CNPM.Models.Courses;

namespace CNPM.DAL
{
    public class AssignmentDAL
    {
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
        public List<ProgressReportDTO> GetProgressByCourse(int courseId)
        {
            var reports = new List<ProgressReportDTO>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                string query = @"
            SELECT 
    u.FullName,
    ISNULL(a.TotalAssignments, 0) AS TotalAssignments,
    ISNULL(s.Submitted, 0) AS SubmittedAssignments,
    ISNULL(g.AverageGrade, 0) AS AverageGrade
FROM CourseEnrollments ce
JOIN Users u ON ce.StudentID = u.UserID
LEFT JOIN (
    SELECT CourseID, COUNT(*) AS TotalAssignments
    FROM Assignments
    WHERE CourseID = @CourseID
    GROUP BY CourseID
) a ON a.CourseID = ce.CourseID
LEFT JOIN (
    SELECT ss.StudentID, a.CourseID, COUNT(*) AS Submitted
    FROM StudentSubmissions ss
    JOIN Assignments a ON ss.AssignmentID = a.AssignmentID
    WHERE a.CourseID = @CourseID
    GROUP BY ss.StudentID, a.CourseID
) s ON s.StudentID = ce.StudentID AND s.CourseID = ce.CourseID
LEFT JOIN (
    SELECT g.StudentID, g.CourseID, AVG(g.Score) AS AverageGrade
    FROM Grades g
    WHERE g.CourseID = @CourseID
    GROUP BY g.StudentID, g.CourseID
) g ON g.StudentID = ce.StudentID AND g.CourseID = ce.CourseID
WHERE ce.CourseID = @CourseID";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var report = new ProgressReportDTO
                            {
                                FullName = reader.GetString(0),
                                TotalAssignments = reader.GetInt32(1),
                                SubmittedAssignments = reader.GetInt32(2),
                                AverageGrade = Convert.ToDouble(reader["AverageGrade"]),
                            };
                            reports.Add(report);
                        }
                    }
                }
            }

            return reports;
        }

    }
}
