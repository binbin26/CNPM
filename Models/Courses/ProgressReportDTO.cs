namespace CNPM.Models.Courses
{
    public class ProgressReportDTO
    {
        public string FullName { get; set; }
        public int TotalAssignments { get; set; }
        public int SubmittedAssignments { get; set; }
        public double CompletionRate { get; set; }
        public double AverageGrade { get; set; }
        public string Classification { get; set; }
        public string Rating { get; set; }
    }
}
