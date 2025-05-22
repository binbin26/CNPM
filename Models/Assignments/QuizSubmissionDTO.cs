using System;

namespace CNPM.Models.Assignments
{
    public class QuizSubmissionDTO
    {
        public int AssignmentID { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public DateTime SubmitDate { get; set; }
        public decimal? Score { get; set; }
    }
}
