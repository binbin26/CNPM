using ClosedXML.Excel;
using CNPM.DAL;
using CNPM.Models.Assignments;
using CNPM.Models.Courses;
using CNPM.Models.Courses.Sessions;
using CNPM.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

public class AssignmentBLL
{
    private readonly AssignmentDAL _assignmentDAL;

    public AssignmentBLL(AssignmentDAL assignmentDAL)
    {
        _assignmentDAL = assignmentDAL;
    }
    public AssignmentBLL() : this(new AssignmentDAL())
    {

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
            // Đã nêu ở trên
            throw; // giữ nguyên thông báo gốc
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

    public List<Assignments> GetAssignmentsByCourse(int courseID)
    {
        if (courseID <= 0) return new List<Assignments>();
        return _assignmentDAL.GetAssignmentsByCourse(courseID);
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
    public bool AutoGradeQuiz(int assignmentId, int teacherId)
    {
        try
        {
            return _assignmentDAL.AutoGradeQuiz(assignmentId, teacherId);
        }
        catch (Exception ex)
        {
            Logger.LogError("Lỗi khi tự động chấm điểm bài trắc nghiệm" + ex.Message);
            return false;
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