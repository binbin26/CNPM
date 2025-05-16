using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Forms.Teacher
{
    public class SessionData
    {
        public string Title { get; set; }
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
        public string AssignmentType { get; set; } // "MultipleChoice" hoặc "Essay"
                                                   // Nếu bài trắc nghiệm có thể thêm các thông tin câu hỏi ở đây (nếu muốn)
    }
}
