using System;
using System.Collections.Generic;
using CNPM.Models.Courses.Sessions;

namespace CNPM.Models.Courses
{
    public class Course
    {
        public int CourseID { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int TeacherID { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public int MaxEnrollment { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public List<SessionData> Sessions { get; set; } = new List<SessionData>();
    }
}
