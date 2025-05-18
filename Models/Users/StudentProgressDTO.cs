namespace CNPM.Models.Users
{
    public class StudentProgressDTO
    {
        public string CourseName { get; set; }
        public int TotalAssignments { get; set; }
        public int SubmittedAssignments { get; set; }
        public double CompletionRate { get; set; }
        public double? Grade { get; set; }
        public string Status { get; set; }
    }
}
