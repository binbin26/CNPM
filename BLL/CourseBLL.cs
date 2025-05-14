using CNPM.DAL;
using CNPM.Models.Courses;
using CNPM.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
        // Trong CourseBLL.cs
        public Course GetCourseByID(int courseID)
        {
            return _courseDAL.GetCourseByID(courseID);
        }

        public bool AddCourse(Course course)
        {
            if (course == null) return false;
            if (string.IsNullOrWhiteSpace(course.CourseCode)) return false;
            if (string.IsNullOrWhiteSpace(course.CourseName)) return false;
            if (course.TeacherID <= 0) return false;
            if (course.StartDate >= course.EndDate) return false;

            return _courseDAL.AddCourse(course);
        }

        public List<EnrolledStudent> GetEnrolledStudents(int courseId)
        {
            return _courseDAL.GetEnrolledStudents(courseId);
        }

        public List<Course> GetCoursesByStudent(int userId)
        {
            return _courseDAL.GetCoursesByStudent(userId);
        }


        public bool RemoveStudent(int studentId, int courseId)
        {
            if (studentId <= 0 || courseId <= 0) return false;
            return _courseDAL.RemoveStudent(studentId, courseId);
        }
    }
}
