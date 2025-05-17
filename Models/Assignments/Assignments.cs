using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Models.Assignments
{
    public class Assignments
    {
        public int AssignmentID { get; set; }
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Decimal MaxScore { get; set; }
        public int CreatedBy { get; set; }
        public AssignmentTypes AssignmentType { get; set; }
    }
}
