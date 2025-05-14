using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CNPM.DAL;
using CNPM.Models.Courses;

public class AssignmentDAL
{
    private readonly string _connectionString = "your_connection_string_here";

    public bool AddAssignment(Assignment assignment)
    {
        using (SqlConnection conn = DatabaseHelper.GetConnection())
        {
            string query = @"INSERT INTO dbo.Assignments 
                             (CourseID, Title, Description, DueDate, MaxScore, CreatedBy) 
                             VALUES (@CourseID, @Title, @Description, @DueDate, @MaxScore, @CreatedBy)";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CourseID", assignment.CourseID);
            cmd.Parameters.AddWithValue("@Title", assignment.Title);
            cmd.Parameters.AddWithValue("@Description", assignment.Description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@DueDate", assignment.DueDate);
            cmd.Parameters.AddWithValue("@MaxScore", assignment.MaxScore);
            cmd.Parameters.AddWithValue("@CreatedBy", assignment.CreatedBy);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }

    public List<Assignment> GetAssignmentsByCourse(int courseID)
    {
        List<Assignment> list = new List<Assignment>();
        using (SqlConnection conn = DatabaseHelper.GetConnection())
        {
            string query = "SELECT * FROM Assignments WHERE CourseID = @CourseID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@CourseID", courseID);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Assignment
                {
                    AssignmentID = (int)reader["AssignmentID"],
                    CourseID = (int)reader["CourseID"],
                    Title = reader["Title"].ToString(),
                    Description = reader["Description"].ToString(),
                    DueDate = (DateTime)reader["DueDate"],
                    MaxScore = (decimal)reader["MaxScore"],
                    CreatedBy = (int)reader["CreatedBy"]
                });
            }
        }
        return list;
    }

}
