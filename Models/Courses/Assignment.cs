using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Models.Courses
{
    public class Assignment
    {
        public string CourseId { get; set; }      // Mã môn học
        public string CourseCode { get; set; }      // Mã môn học
        public string Title { get; set; }      // Mã môn học
        public string ExerciseName { get; set; }  // Tên bài tập
        public DateTime DueDate { get; set; }   // Mã sinh viên nộp
        public string StudentName { get; set; }   // Tên sinh viên nộp
        public string FilePath { get; set; }      // Đường dẫn file đã nộp
        public bool IsQuiz { get; set; }         // true: bài trắc nghiệm, false: bài tự luận
    }
}
