using CNPM.DAL;
using CNPM.Models.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.BLL
{
    public class CourseBLL
    {
        private readonly CourseDAL _courseDAL = new CourseDAL();
        // ✅ BỔ SUNG constructor có DI
        public CourseBLL(CourseDAL courseDAL)
        {
            _courseDAL = courseDAL;
        }
        public List<Course> GetAvailableCourses()
        {
            return _courseDAL.GetAllCourses().Where(c => c.EndDate > DateTime.Now).ToList();
        }

        public bool EnrollStudent(int studentID, int courseID)
        {
            // Validate logic trước khi gọi DAL
            if (studentID <= 0 || courseID <= 0) return false;
            return _courseDAL.EnrollStudent(studentID, courseID);
        }
        public List<Course> GetCoursesByTeacher(int teacherID)
        {
            return _courseDAL.GetCoursesByTeacher(teacherID);
        }
    }
}
