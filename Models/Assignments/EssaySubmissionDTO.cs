using System;

namespace CNPM.Models.Assignments
{
    public class EssaySubmissionDTO
    {
        public int AssignmentID { get; set; }        
        public int StudentID { get; set; }           
        public string StudentName { get; set; }     
        public DateTime SubmissionDate { get; set; } 
        public string FilePath { get; set; }         
        public string EssayContent { get; set; }     
        public decimal? Score { get; set; }          
    }
}
