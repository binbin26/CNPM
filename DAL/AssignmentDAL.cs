using ClosedXML.Excel;
using CNPM.Models.Assignments;
using CNPM.Models.Courses;
using CNPM.Models.Courses.Sessions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
                        (CourseID, Title, Description, DueDate, MaxScore) 
                    VALUES 
                        (@CourseID, @Title, @Description, @DueDate, @MaxScore)";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", assignment.CourseID);
                    cmd.Parameters.AddWithValue("@Title", assignment.Title);
                    cmd.Parameters.AddWithValue("@Description", (object)assignment.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DueDate", assignment.DueDate);
                    cmd.Parameters.AddWithValue("@MaxScore", assignment.MaxScore);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        //Tải bài tập trắc nghiệm ucQuiz:
        public List<Question> GetQuestionsByAssignmentId(int assignmentId)
        {
            List<Question> questions = new List<Question>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = @"SELECT [QuestionID], [AssignmentID], [QuestionText], [OptionA], [OptionB], [OptionC], [OptionD], [CorrectAnswer]
                         FROM Questions
                         WHERE AssignmentID = @AssignmentID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AssignmentID", assignmentId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Question q = new Question
                    {
                        QuestionID = Convert.ToInt32(reader["QuestionID"]),
                        AssignmentID = Convert.ToInt32(reader["AssignmentID"]),
                        QuestionText = reader["QuestionText"].ToString(),
                        OptionA = reader["OptionA"].ToString(),
                        OptionB = reader["OptionB"].ToString(),
                        OptionC = reader["OptionC"].ToString(),
                        OptionD = reader["OptionD"].ToString(),
                        CorrectAnswer = reader["CorrectAnswer"].ToString()
                    };
                    questions.Add(q);
                }
                conn.Close();
                return questions;
            }
        }

        //Lưu câu trả lời trắc nghiệm của Sinh viên
        public void SaveStudentAnswers(int assignmentId, int studentId, Dictionary<int, string> answers)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        foreach (var entry in answers)
                        {
                            int questionId = entry.Key;
                            string selectedAnswer = entry.Value;

                            string query = @"
                        IF EXISTS (SELECT 1 FROM StudentAnswers 
                                   WHERE AssignmentID = @AssignmentID 
                                     AND StudentID = @StudentID 
                                     AND QuestionID = @QuestionID)
                        BEGIN
                            UPDATE StudentAnswers
                            SET SelectedAnswer = @SelectedAnswer
                            WHERE AssignmentID = @AssignmentID 
                              AND StudentID = @StudentID 
                              AND QuestionID = @QuestionID
                        END
                        ELSE
                        BEGIN
                            INSERT INTO StudentAnswers
                                (AssignmentID, StudentID, QuestionID, SelectedAnswer)
                            VALUES
                                (@AssignmentID, @StudentID, @QuestionID, @SelectedAnswer)
                        END";

                            using (SqlCommand command = new SqlCommand(query, conn, transaction))
                            {
                                command.Parameters.AddWithValue("@AssignmentID", assignmentId);
                                command.Parameters.AddWithValue("@StudentID", studentId);
                                command.Parameters.AddWithValue("@QuestionID", questionId);
                                command.Parameters.AddWithValue("@SelectedAnswer", selectedAnswer);

                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu danh sách câu trả lời: {ex.Message}");
                throw;
            }
        }

        public List<AssignmentMC> GetMultipleChoiceAssignmentIds(int teacherId, int courseId)
        {
            List<AssignmentMC> assignmentMCs = new List<AssignmentMC>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = @"
            SELECT a.AssignmentID
            FROM Assignments a
            JOIN AssignmentMC mc ON a.AssignmentID = mc.AssignmentID
            WHERE a.TeacherID = @TeacherID AND a.CourseID = @CourseID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TeacherID", teacherId);
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            assignmentMCs.Add(new AssignmentMC
                            {
                                AssignmentID = (int)reader["AssignmentID"]
                            });
                        }
                    }
                }
            }
            return assignmentMCs;
        }


        public List<Assignments> GetAssignmentsForStudentWithStatus(string username)
        {
            List<Assignments> assignments = new List<Assignments>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                using (var cmd = new SqlCommand(@"
            SELECT a.AssignmentID, a.CourseID, a.Title, a.Description, a.DueDate, a.MaxScore,
                   c.CourseName,
                   CASE
                       WHEN ss.SubmissionID IS NOT NULL THEN 'Submitted'
                       ELSE 'Not Submitted'
                   END AS SubmissionStatus
            FROM Assignments a
            INNER JOIN Courses c ON a.CourseID = c.CourseID
            INNER JOIN CourseEnrollments ce ON c.CourseID = ce.CourseID
            INNER JOIN Users u ON ce.StudentID = u.UserID
            LEFT JOIN StudentSubmissions ss ON a.AssignmentID = ss.AssignmentID AND ss.StudentID = u.UserID
            WHERE u.Username = @Username
            ORDER BY a.DueDate DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            assignments.Add(new Assignments
                            {
                                AssignmentID = reader.GetInt32(reader.GetOrdinal("AssignmentID")),
                                CourseID = reader.GetInt32(reader.GetOrdinal("CourseID")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString(reader.GetOrdinal("Description")),
                                DueDate = reader.IsDBNull(reader.GetOrdinal("DueDate"))
                                  ? (DateTime?)null
                                  : reader.GetDateTime(reader.GetOrdinal("DueDate")),
                                MaxScore = reader.GetDecimal(reader.GetOrdinal("MaxScore")),
                                SubmissionStatus = reader.GetString(reader.GetOrdinal("SubmissionStatus"))
                            });
                        }
                    }
                }
            }

            return assignments;
        }


        //Hàm kiểm tra loại bài tập:
        public string GetAssignmentType(int assignmentId)
        {
            if (IsMultipleChoice(assignmentId)) return "TracNghiem";
            if (IsEssay(assignmentId)) return "TuLuan";
            return "Unknown";
        }
        //Lấy thời gian làm bài trắc nghiệm
        public int GetDuration(int assignmentId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT Duration FROM AssignmentMC WHERE AssignmentID = @AssignmentID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AssignmentID", assignmentId);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }
        //Lấy đề tự luận cho sinh viên
        public string GetEssay(int assignmentId)
        {
            string filePath = null;
            string query = "SELECT FilePath FROM AssignmentFiles WHERE AssignmentID = @AssignmentID";

            using (var conn = DatabaseHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@AssignmentID", assignmentId);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    filePath = result.ToString();
                }
            }

            return filePath;
        }

        public List<CourseDocuments> GetCourseDocuments(int courseId)
        {
            List<CourseDocuments> documents = new List<CourseDocuments>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = @"
            SELECT 
                cd.DocumentID,
                cd.CourseID,
                cd.Title AS DocumentTitle,
                cd.FilePath,
                cd.UploadedBy,
                cd.UploadDate,
                cd.DocumentType,
                cd.Documents,
                cd.DueDate,
                cd.SessionID,
                s.Title AS SessionTitle,
                s.CreatedAt
            FROM CourseDocuments cd
            INNER JOIN Sessions s ON cd.SessionID = s.SessionID
            WHERE cd.CourseID = @CourseID
            ORDER BY s.CreatedAt;";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseID", courseId);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CourseDocuments doc = new CourseDocuments
                    {
                        DocumentID = reader.GetInt32(0),
                        CourseID = reader.GetInt32(1),
                        Title = reader.GetString(2),
                        FilePath = reader.IsDBNull(3) ? null : reader.GetString(3),
                        UploadedBy = reader.IsDBNull(4) ? null : reader.GetString(4),
                        UploadDate = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                        DocumentType = reader.IsDBNull(6) ? null : reader.GetString(6),
                        Documents = reader.IsDBNull(7) ? null : reader.GetString(7),
                        DueDate = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8),
                        SessionID = reader.GetInt32(9),
                        SessionTitle = reader.IsDBNull(10) ? null : reader.GetString(10),
                        CreatedAt = reader.IsDBNull(11) ? (DateTime?)null : reader.GetDateTime(11)
                    };

                    documents.Add(doc);
                }
            }

            return documents;
        }


        //Lưu bài tập tự luận cho sinh viên
        public void SaveSubmissionToDatabase(int assignmentId, int studentId, string fileName, string filePath, DateTime submitDate)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string query = "INSERT INTO StudentSubmissions (AssignmentID, StudentID, FileName, FilePath, SubmitDate) " +
                               "VALUES (@AssignmentID, @StudentID, @FileName, @FilePath, @SubmitDate)";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@AssignmentID", assignmentId);
                    command.Parameters.AddWithValue("@StudentID", studentId);
                    command.Parameters.AddWithValue("@FileName", fileName);
                    command.Parameters.AddWithValue("@FilePath", filePath);
                    command.Parameters.AddWithValue("@SubmitDate", submitDate);

                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private bool IsMultipleChoice(int assignmentId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM AssignmentMC WHERE AssignmentID = @AssignmentID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AssignmentID", assignmentId);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private bool IsEssay(int assignmentId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM AssignmentFiles WHERE AssignmentID = @AssignmentID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AssignmentID", assignmentId);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        //Report 2:
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
    ISNULL(s.AverageGrade, 0) AS AverageGrade
FROM CourseEnrollments ce
JOIN Users u ON ce.StudentID = u.UserID
LEFT JOIN (
    SELECT CourseID, COUNT(*) AS TotalAssignments
    FROM Assignments
    WHERE CourseID = @CourseID
    GROUP BY CourseID
) a ON a.CourseID = ce.CourseID
LEFT JOIN (
    SELECT 
        ss.StudentID, 
        a.CourseID, 
        COUNT(*) AS Submitted,
        AVG(CAST(ss.Score AS FLOAT)) AS AverageGrade
    FROM StudentSubmissions ss
    JOIN Assignments a ON ss.AssignmentID = a.AssignmentID
    WHERE a.CourseID = @CourseID
    GROUP BY ss.StudentID, a.CourseID
) s ON s.StudentID = ce.StudentID AND s.CourseID = ce.CourseID
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
            WHERE s.AssignmentID = @AssignmentID
            AND a.CreatedBy = @TeacherID
            AND EXISTS (
            SELECT 1
            FROM AssignmentFiles af
            WHERE af.AssignmentID = s.AssignmentID)";

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
        public decimal AutoGradeQuiz(int assignmentId, int studentId)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string updateIsCorrect = @"
                UPDATE sa 
                SET IsCorrect = CASE 
                WHEN sa.SelectedAnswer = q.CorrectAnswer THEN 1 
                ELSE 0 
                END
                FROM StudentAnswers sa
                JOIN Questions q ON sa.QuestionID = q.QuestionID
                WHERE sa.StudentID = @StudentID AND q.AssignmentID = @AssignmentID";

                using (var updateCmd = new SqlCommand(updateIsCorrect, conn))
                {
                    updateCmd.Parameters.AddWithValue("@StudentID", studentId);
                    updateCmd.Parameters.AddWithValue("@AssignmentID", assignmentId);
                    updateCmd.ExecuteNonQuery();
                }


                string query = @"
                SELECT COUNT(*) AS TotalQuestions,
                SUM(CASE WHEN sa.SelectedAnswer = q.CorrectAnswer THEN 1 ELSE 0 END) AS CorrectAnswers
                FROM StudentAnswers sa
                JOIN Questions q ON sa.QuestionID = q.QuestionID
                WHERE sa.StudentID = @StudentID AND q.AssignmentID = @AssignmentID";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    cmd.Parameters.AddWithValue("@AssignmentID", assignmentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int total = reader.GetInt32(0);
                            int correct = reader.GetInt32(1);

                            if (total == 0) return 0;

                            decimal score = (decimal)correct / total * 10;
                            reader.Close();

                            // Cập nhật vào bảng StudentSubmissions
                            string update = @"
                            UPDATE StudentSubmissions 
                            SET Score = @Score
                            WHERE AssignmentID = @AssignmentID AND StudentID = @StudentID";

                            using (var updateCmd = new SqlCommand(update, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@Score", score);
                                updateCmd.Parameters.AddWithValue("@AssignmentID", assignmentId);
                                updateCmd.Parameters.AddWithValue("@StudentID", studentId);
                                updateCmd.ExecuteNonQuery();
                            }

                            return score;
                        }
                    }
                }
            }

            return 0;
        }

        //Ràng buộc số lần làm trắc nghiệm
        public bool HasExceededMaxAttempts(int assignmentId, int studentId)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT a.MaxAttempts, 
                   COUNT(ss.SubmissionID) AS AttemptCount
            FROM AssignmentMC a
            LEFT JOIN StudentSubmissions ss 
                ON ss.AssignmentID = a.AssignmentID AND ss.StudentID = @StudentID
            WHERE a.AssignmentID = @AssignmentID
            GROUP BY a.MaxAttempts";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AssignmentID", assignmentId);
                    cmd.Parameters.AddWithValue("@StudentID", studentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int maxAttempts = reader.GetInt32(0);
                            int attemptCount = reader.GetInt32(1);
                            return attemptCount >= maxAttempts;
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
                WHERE s.AssignmentID = @AssignmentID 
                AND a.TeacherID = @TeacherID 
                AND EXISTS (
                SELECT 1 FROM AssignmentMC amc 
                WHERE amc.AssignmentID = a.AssignmentID)";

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


        public string GetUsernameByAssignmentId(int assignmentId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"
                        SELECT u.Username
                        FROM Users u
                        JOIN Enrollments e ON u.UserID = e.StudentID
                        JOIN Assignments a ON e.CourseID = a.CourseID
                        WHERE a.AssignmentID = @AssignmentId";

                    cmd.Parameters.AddWithValue("@AssignmentId", assignmentId);

                    return (string)cmd.ExecuteScalar();
                }
            }
        }
    }
}
