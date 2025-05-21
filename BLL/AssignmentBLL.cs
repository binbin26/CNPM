using ClosedXML.Excel;
using CNPM.DAL;
using CNPM.Models.Assignments;
using CNPM.Models.Courses;
using System;
using System.Collections.Generic;
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
    public List<Assignments> GetAssignmentsForStudent(string username)
    {
        return _assignmentDAL.GetAssignmentsForStudent(username);
    }

    public bool AddAssignment(Assignments assignment)
    {
        if (assignment == null) return false;
        if (assignment.CourseID <= 0) return false;
        if (string.IsNullOrWhiteSpace(assignment.Title)) return false;
        if (assignment.DueDate <= DateTime.Now) return false;
        if (assignment.MaxScore < 0) return false;
        if (assignment.CreatedBy <= 0) return false;

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
