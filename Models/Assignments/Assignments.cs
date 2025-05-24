using System;

namespace CNPM.Models.Assignments
{
    public class Assignments
    {
        public int AssignmentID { get; set; }
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal MaxScore { get; set; }
        public string CreatedBy { get; set; }
        public string SubmissionStatus { get; set; }
    }
}
