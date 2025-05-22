using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CNPM.Models.Assignments;
using CNPM.Models.Courses;
using ClosedXML.Excel;

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

        public List<Assignments> GetAssignmentsForStudentWithStatus(string username)
        {
            var list = new List<Assignments>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                // Lấy StudentID từ Username
                string getIdQuery = "SELECT UserID FROM Users WHERE Username = @Username";
                int studentId;

                using (var getIdCmd = new SqlCommand(getIdQuery, conn))
                {
                    getIdCmd.Parameters.AddWithValue("@Username", username);
                    object result = getIdCmd.ExecuteScalar();
                    if (result == null) return list;
                    studentId = Convert.ToInt32(result);
                }

                // Truy vấn bài tập có trạng thái nộp bài
                string query = @"
            SELECT 
                a.AssignmentID,
                c.CourseName,
                a.Title,
                a.Description,
                a.DueDate,
                a.MaxScore,
                CASE 
                    WHEN s.SubmissionID IS NOT NULL THEN N'Đã nộp'
                    ELSE N'Chưa nộp'
                END AS SubmissionStatus
            FROM Assignments a
            INNER JOIN Courses c ON a.CourseID = c.CourseID
            INNER JOIN CourseEnrollments ce ON c.CourseID = ce.CourseID
            LEFT JOIN Submissions s ON a.AssignmentID = s.AssignmentID 
                AND s.StudentID = @StudentID
            WHERE ce.StudentID = @StudentID
            ORDER BY a.DueDate DESC";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
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

            using (SqlConnection conn = DatabaseHelper.GetConnection())
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
        //report 3
        public List<QuestionStatsDTO> GetQuestionPerformance(int assignmentId)
        {
            var list = new List<QuestionStatsDTO>();
            string query = @"
            SELECT 
    q.QuestionID,
    q.QuestionText,
    COUNT(sa.AnswerID) AS TotalAnswers,
    CAST(SUM(CASE WHEN sa.IsCorrect = 1 THEN 1 ELSE 0 END) AS FLOAT) / 
        NULLIF(COUNT(sa.AnswerID), 0) * 100 AS CorrectRate,
    CAST(SUM(CASE WHEN sa.IsCorrect = 0 THEN 1 ELSE 0 END) AS FLOAT) / 
        NULLIF(COUNT(sa.AnswerID), 0) * 100 AS IncorrectRate,
    CASE 
        WHEN CAST(SUM(CASE WHEN sa.IsCorrect = 1 THEN 1 ELSE 0 END) AS FLOAT) / NULLIF(COUNT(sa.AnswerID), 0) >= 0.8 THEN N'Dễ'
        WHEN CAST(SUM(CASE WHEN sa.IsCorrect = 1 THEN 1 ELSE 0 END) AS FLOAT) / NULLIF(COUNT(sa.AnswerID), 0) >= 0.5 THEN N'Trung bình'
        ELSE N'Khó'
    END AS Difficulty
FROM Questions q
LEFT JOIN StudentAnswers sa ON q.QuestionID = sa.QuestionID
WHERE q.AssignmentID = @AssignmentID
GROUP BY q.QuestionID, q.QuestionText
ORDER BY q.QuestionID";

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@assignmentId", assignmentId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dto = new QuestionStatsDTO
                        {
                            QuestionID = reader.GetInt32(0),
                            Content = reader.GetString(1),
                            TotalAnswers = reader.GetInt32(2),
                            CorrectRate = Math.Round(reader.GetDouble(3), 2)
                        };

                        dto.Difficulty = dto.CorrectRate >= 80 ? "Dễ" :
                                         dto.CorrectRate >= 50 ? "Trung bình" : "Khó";

                        list.Add(dto);
                    }
                }
            }
            return list;
        }
        //Lấy bài tập tự luận (UcSubmissions)
        public List<EssaySubmissionDTO> GetEssaySubmissions(int assignmentId, int teacherId)
        {
            List<EssaySubmissionDTO> submissions = new List<EssaySubmissionDTO>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = @"
            SELECT s.*
            FROM StudentSubmissions s
            JOIN Assignments a ON s.AssignmentID = a.AssignmentID
            WHERE s.AssignmentID = @AssignmentID AND a.TeacherID = @TeacherID AND a.AssignmentType = N'TuLuan'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AssignmentID", assignmentId);
                    cmd.Parameters.AddWithValue("@TeacherID", teacherId);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            submissions.Add(new EssaySubmissionDTO
                            {
                                StudentID = (int)reader["StudentID"],
                                AssignmentID = (int)reader["AssignmentID"],
                                FilePath = reader["FilePath"].ToString(),
                                Score = reader["Score"] == DBNull.Value ? null : (decimal?)reader["Score"]
                            });
                        }
                    }
                }
            }
            return submissions;
        }

        //Update điểm cho bài tập tự luận (UcSubmissions)
        public bool UpdateSubmissionScore(int assignmentId, int studentId, decimal score, int teacherId)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = @"
            UPDATE StudentSubmissions
            SET Score = @Score
            WHERE AssignmentID = @AssignmentID 
              AND StudentID = @StudentID 
              AND EXISTS (
                  SELECT 1 FROM Assignments 
                  WHERE AssignmentID = @AssignmentID AND TeacherID = @TeacherID
              )";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Score", score);
                    cmd.Parameters.AddWithValue("@AssignmentID", assignmentId);
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    cmd.Parameters.AddWithValue("@TeacherID", teacherId);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        //Tự chấm điểm trắc nghiệm (UcQuiz)
        public bool AutoGradeQuiz(int assignmentId, int studentId)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = @"
        SELECT COUNT(*) AS TotalQuestions,
               SUM(CASE WHEN sa.IsCorrect = 1 THEN 1 ELSE 0 END) AS CorrectAnswers
        FROM StudentAnswers sa
        JOIN Questions q ON sa.QuestionID = q.QuestionID
        WHERE sa.StudentID = @StudentID AND q.AssignmentID = @AssignmentID";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    cmd.Parameters.AddWithValue("@AssignmentID", assignmentId);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int total = reader.GetInt32(0);
                            int correct = reader.GetInt32(1);
                            decimal score = (decimal)correct / total * 10;

                            reader.Close();
                            // Cập nhật điểm
                            string update = @"
                        UPDATE StudentSubmissions 
                        SET Score = @Score
                        WHERE AssignmentID = @AssignmentID AND StudentID = @StudentID";

                            using (var updateCmd = new SqlCommand(update, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@Score", score);
                                updateCmd.Parameters.AddWithValue("@AssignmentID", assignmentId);
                                updateCmd.Parameters.AddWithValue("@StudentID", studentId);

                                return updateCmd.ExecuteNonQuery() > 0;
                            }
                        }
                    }
                }
            }
            return false;
        }
        //Lấy danh sách bài nộp trắc nghiệm(UcQuiz)
        public List<QuizSubmissionDTO> GetQuizSubmissions(int assignmentId, int teacherId)
        {
            List<QuizSubmissionDTO> submissions = new List<QuizSubmissionDTO>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = @"
        SELECT u.FullName, s.StudentID, s.Score
        FROM StudentSubmissions s
        JOIN Users u ON s.StudentID = u.UserID
        JOIN Assignments a ON s.AssignmentID = a.AssignmentID
        WHERE s.AssignmentID = @AssignmentID AND a.TeacherID = @TeacherID AND a.AssignmentType = N'TracNghiem'";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AssignmentID", assignmentId);
                    cmd.Parameters.AddWithValue("@TeacherID", teacherId);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            submissions.Add(new QuizSubmissionDTO
                            {
                                StudentID = (int)reader["StudentID"],
                                StudentName = reader["FullName"].ToString(),
                                Score = reader["Score"] == DBNull.Value ? null : (decimal?)reader["Score"]
                            });
                        }
                    }
                }
            }
            return submissions;
        }
    }
}
