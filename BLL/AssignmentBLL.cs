using ClosedXML.Excel;
using CNPM.DAL;
using CNPM.Models.Assignments;
using CNPM.Models.Courses;
using CNPM.Utilities;
using DocumentFormat.OpenXml.Office.CustomXsn;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

public class AssignmentBLL
{
    private readonly AssignmentDAL _assignmentDAL;
    private static AssignmentBLL _instance;

    public AssignmentBLL(AssignmentDAL assignmentDAL)
    {
        _assignmentDAL = assignmentDAL;
    }
    public AssignmentBLL() : this(new AssignmentDAL())
    {

    }
    public static AssignmentBLL Instance
    {
        get
        {
            if (_instance == null)
                _instance = new AssignmentBLL();
            return _instance;
        }
    }

    public string GetAssignmentType(int assignmentId)
    {
        try
        {
            return _assignmentDAL.GetAssignmentType(assignmentId);
        }
        catch (Exception ex)
        {
            Logger.LogError("Lỗi khi lấy loại bài tập: " + ex.Message);
            throw new Exception("Không thể xác định loại bài tập. Vui lòng thử lại.");
        }
    }

    public Result SaveStudentAnswers(int assignmentId, int studentId, Dictionary<int, string> answers)
    {
        try
        {
            _assignmentDAL.SaveStudentAnswers(assignmentId, studentId, answers);

            return new Result { IsSuccess = true };
        }
        catch (Exception ex)
        {
            return new Result
            {
                IsSuccess = false,
                ErrorMessage = $"Lỗi khi lưu đáp án: {ex.Message}"
            };
        }
    }


    public List<Question> LoadQuestions(int assignmentId)
    {
        try
        {
            return _assignmentDAL.GetQuestionsByAssignmentId(assignmentId);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi khi tải danh sách câu hỏi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return new List<Question>();
        }
    }

    public int GetDuration(int assignmentId)
    {
        try
        {
            return _assignmentDAL.GetDuration(assignmentId);
        }
        catch (Exception ex)
        {
            Logger.LogError("Lỗi khi lấy thời gian làm bài: " + ex.Message);
            throw new ApplicationException("Không thể lấy thời gian làm bài. Vui lòng thử lại.");
        }
    }
    //Xử lý file bài tập tự luận cho sinh viên
    public string SaveEssaySubmission(int assignmentId, int studentId, string sourceFilePath)
    {
        if (!File.Exists(sourceFilePath))
            return null;

        string fileName = Path.GetFileName(sourceFilePath);
        string submissionsFolder = Path.Combine(Application.StartupPath, "Submissions");

        if (!Directory.Exists(submissionsFolder))
            Directory.CreateDirectory(submissionsFolder);

        string uniqueFileName = $"{studentId}_{assignmentId}_{fileName}";
        string destinationPath = Path.Combine(submissionsFolder, uniqueFileName);

        try
        {
            File.Copy(sourceFilePath, destinationPath, true);

            string relativePath = Path.Combine("Submissions", uniqueFileName);
            _assignmentDAL.SaveSubmissionToDatabase(assignmentId, studentId, fileName, relativePath, DateTime.Now);

            return relativePath;
        }
        catch
        {
            return null;
        }
    }

    public string GetEssay(int assignmentId)
    {
        try
        {
            return _assignmentDAL.GetEssay(assignmentId);
        }
        catch (Exception ex)
        {
            Logger.LogError("Lỗi khi lấy đường dẫn file đề bài tự luận: " + ex.Message);
            throw new ApplicationException("Không thể lấy đường dẫn file đề bài tự luận. Vui lòng thử lại.");
        }
    }

    public List<CourseDocuments> GetCourseDocuments(int courseId)
    {
        try
        {
            return _assignmentDAL.GetCourseDocuments(courseId);
        }
        catch (Exception ex)
        {
            Logger.LogError("Lỗi khi lấy tài liệu khóa học: " + ex.Message);
            throw new ApplicationException("Không thể lấy tài liệu khóa học. Vui lòng thử lại.");
        }
    }

    public List<Assignments> GetAssignmentsForStudentWithStatus(string username)
    {
        try
        {
            var assignments = _assignmentDAL.GetAssignmentsForStudentWithStatus(username);

            if (assignments == null)
            {
                Logger.LogError($"Không tìm thấy username: {username} trong hệ thống.");
                throw new ArgumentException($"Không tìm thấy tài khoản sinh viên với tên đăng nhập: {username}");
            }

            return assignments;
        }
        catch (SqlException sqlEx)
        {
            Logger.LogError($"[SQL ERROR] Lỗi truy vấn CSDL cho sinh viên '{username}': {sqlEx}");
            throw new ApplicationException("Đã xảy ra lỗi truy vấn cơ sở dữ liệu. Vui lòng thử lại hoặc liên hệ hỗ trợ kỹ thuật.", sqlEx);
        }
        catch (ArgumentException argEx)
        {
            throw; 
        }
        catch (Exception ex)
        {
            Logger.LogError($"[UNKNOWN ERROR] Lỗi không xác định khi lấy bài tập cho sinh viên '{username}': {ex}");
            throw new ApplicationException("Đã xảy ra lỗi không xác định. Vui lòng thử lại hoặc liên hệ quản trị viên.", ex);
        }
    }

    public bool AddAssignment(Assignments assignment)
    {
        if (assignment == null) return false;
        if (assignment.CourseID <= 0) return false;
        if (string.IsNullOrWhiteSpace(assignment.Title)) return false;
        if (assignment.DueDate <= DateTime.Now) return false;
        if (assignment.MaxScore < 0) return false;

        return _assignmentDAL.AddAssignment(assignment);
    }

    public List<AssignmentMC> GetMultipleChoiceAssignmentIds(int teacherId, int courseId)
    {
        try
        {
            return _assignmentDAL.GetMultipleChoiceAssignmentIds(teacherId, courseId);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Lỗi khi lấy danh sách bài tập trắc nghiệm.", ex);
        }
    }

    public List<ProgressReportDTO> GetCourseProgress(int courseId)
    {
        var data = _assignmentDAL.GetProgressByCourse(courseId);

        foreach (var item in data)
        {
            item.CompletionRate = item.TotalAssignments == 0 ? 0 :
                (double)item.SubmittedAssignments / item.TotalAssignments;

            item.Rating = item.AverageGrade >= 8 ? "Giỏi" :
                          item.AverageGrade >= 6.5 ? "Khá" :
                          item.AverageGrade >= 5 ? "Trung bình" : "Yếu";
        }

        return data;
    }

    public List<QuestionStatsDTO> GetPerformance(int assignmentId)
    {
        return _assignmentDAL.GetQuestionPerformance(assignmentId);
    }
    // Lấy danh sách bài tự luận của một bài tập
    public List<EssaySubmissionDTO> GetEssaySubmissions(int assignmentId, int teacherId)
    {
        try
        {
            return _assignmentDAL.GetEssaySubmissions(assignmentId, teacherId);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi khi lấy bài tự luận: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return new List<EssaySubmissionDTO>();
        }
    }
    //Cập nhật điểm bài tự luận từ giảng viên
    public bool UpdateSubmissionScore(int assignmentId, int studentId, decimal score, int teacherId)
    {
        try
        {
            return _assignmentDAL.UpdateSubmissionScore(assignmentId, studentId, score, teacherId);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Lỗi khi cập nhật điểm bài nộp.", ex);
        }
    }

    //Chấm điểm tự động bài trắc nghiệm
    public decimal AutoGradeQuiz(int assignmentId, int teacherId)
    {
        try
        {
            return _assignmentDAL.AutoGradeQuiz(assignmentId, teacherId);
        }
        catch (Exception ex)
        {
            Logger.LogError("Lỗi khi tự động chấm điểm bài trắc nghiệm" + ex.Message);
            return 0;
        }
    }

    public bool HasExceededMaxAttempts(int assignmentId, int studentId)
    {
        try
        {
            return _assignmentDAL.HasExceededMaxAttempts(assignmentId, studentId);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Lỗi khi kiểm tra số lần làm bài", ex);
        }
    }

    //Lấy danh sách bài làm trắc nghiệm của một bài tập
    public List<QuizSubmissionDTO> GetQuizSubmissions(int assignmentId, int teacherId)
    {
        try
        {
            return _assignmentDAL.GetQuizSubmissions(assignmentId, teacherId);
        }
        catch (Exception ex)
        {
            Logger.LogError("Lỗi khi lấy danh sách bài làm trắc nghiệm: " + ex.Message);
            return new List<QuizSubmissionDTO>();
        }
    }


    public void ExportQuestionStatsToExcel(List<QuestionStatsDTO> data, string filePath)
    {
        var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Report");

        ws.Cell(1, 1).Value = "Câu hỏi";
        ws.Cell(1, 2).Value = "Lượt trả lời";
        ws.Cell(1, 3).Value = "Tỉ lệ đúng (%)";
        ws.Cell(1, 4).Value = "Tỉ lệ sai (%)";
        ws.Cell(1, 5).Value = "Độ khó";

        for (int i = 0; i < data.Count; i++)
        {
            ws.Cell(i + 2, 1).Value = data[i].Content;
            ws.Cell(i + 2, 2).Value = data[i].TotalAnswers;
            ws.Cell(i + 2, 3).Value = data[i].CorrectRate;
            ws.Cell(i + 2, 4).Value = Math.Round(100 - data[i].CorrectRate, 2);
            ws.Cell(i + 2, 5).Value = data[i].Difficulty;
        }

        ws.Columns().AdjustToContents();
        wb.SaveAs(filePath);
    }

}