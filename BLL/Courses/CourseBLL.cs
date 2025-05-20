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
        private CourseDAL _courseDAL;

        public CourseBLL()
        {
            _courseDAL = new CourseDAL();
        }

        public CourseBLL(CourseDAL courseDAL)
        {
            _courseDAL = courseDAL;
        }
        public List<Course> GetAvailableCourses()
        {
            return _courseDAL.GetAllCourses().Where(c => c.EndDate > DateTime.Now).ToList();
        }

        public string RegisterStudentToCourse(int studentId, int courseId)
        {
            return _courseDAL.RegisterCourse(studentId, courseId);
        }
        public bool EnrollStudent(int studentID, int courseID)
        {
            // ✅ Đảm bảo StudentID là UserID có Role = 'Student'
            if (!_courseDAL.UserExistsWithRole(studentID, "Student")) return false;
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

            // ✅ Đảm bảo TeacherID chính là UserID của Role = 'Teacher'
            if (!_courseDAL.UserExistsWithRole(course.TeacherID, "Teacher")) return false;

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

        public List<CourseGrade> GetGradesByStudent(int studentId)
        {
            return _courseDAL.GetGradesByStudent(studentId);
        }

        public bool RemoveStudent(int studentId, int courseId)
        {
            if (studentId <= 0 || courseId <= 0) return false;
            return _courseDAL.RemoveStudent(studentId, courseId);
        }

        public List<ViewModel1> GetFormattedGradesByStudent(int studentId)
        {
            var grades = _courseDAL.GetGradesByStudent(studentId);

            return grades.Select(g => new ViewModel1
            {
                CourseName = g.CourseName,
                ScoreText = g.Score.HasValue ? g.Score.Value.ToString("0.00") : "Chưa có điểm",
                GradedBy = g.GradedBy
            }).ToList();
        }
    }
}
