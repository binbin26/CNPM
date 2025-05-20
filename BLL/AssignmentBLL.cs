using CNPM.DAL;
using CNPM.Models.Assignments;
using System;
using System.Collections.Generic;
using CNPM.Models.Courses;

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
}
