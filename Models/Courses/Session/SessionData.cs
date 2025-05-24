using System.Collections.Generic;

namespace CNPM.Models.Courses.Sessions
{
    public class SessionData
    {
        public string Title { get; set; }
        public int CourseID { get; set; }
        public List<FileItem> AttachedFiles { get; set; } = new List<FileItem>();
        public List<AssignmentData> Assignments { get; set; } = new List<AssignmentData>();
    }

    public class FileItem
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }

    public class AssignmentData
    {
        public string Title { get; set; }
        public string FilePath { get; set; }
        public string AssignmentType { get; set; }
    }
}
