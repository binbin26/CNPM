using System;

namespace CNPM.Models.Assignments
{
    public class AssignmentFile
    {
        public int FileID { get; set; }
        public int AssignmentID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
        public int CourseID { get; set; }
    }
}
