using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Models.Courses
{
    public class CourseGrade
    {
        public string CourseName { get; set; }
        public float? Score { get; set; }
        public int? TeacherID { get; set; }
        public string GradedBy { get; set; } 
    }
}
