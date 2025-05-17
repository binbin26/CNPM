using CNPM.DAL;
using CNPM.Models.Assignments;
using System;
using System.Collections.Generic;

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
}
