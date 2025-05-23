using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Models.Courses
{
    public class CourseDocuments
    {
        public int DocumentID;
        public int CourseID;
        public string Title;
        public string FilePath;
        public string UploadedBy;
        public DateTime? UploadDate;
        public string DocumentType;
        public string Documents;
        public DateTime? DueDate;
        public int? SessionID;
        public string SessionTitle;
        public DateTime? CreatedAt;
    }
}
