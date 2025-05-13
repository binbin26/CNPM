using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM.Forms.Student
{
    public partial class ucBaiTap : UserControl
    {
        private Panel mainPanel;
        private DataGridView dgvAssignments;
        public List<Assignment> assignments;
        private int selectedAssignmentId = -1;
        private frmTongQuan _parentForm;

        public ucBaiTap(frmTongQuan parentForm, List<Assignment> assignments = null)
        {
            _parentForm = parentForm;
            InitializeComponent();
            InitializeCustomComponents();
            if (assignments != null)
                this.assignments = assignments;
            else
                LoadAssignments();
            LoadDataToGrid();
        }



        private void LoadAssignments()
        {
            // Tạo dữ liệu mẫu
            assignments = new List<Assignment>
            {
                new Assignment { AssignmentID = 1, Title = "Bài tập 1: Giải phương trình", Description = "Giải phương trình bậc 2: x^2 - 5x + 6 = 0", DueDate = DateTime.Now.AddDays(3), Type = AssignmentType.TuLuan, HanNop = "Chưa làm" },
                new Assignment { AssignmentID = 2, Title = "Bài tập 2: Viết đoạn văn", Description = "Viết đoạn văn ngắn về bản thân bằng tiếng Anh.", DueDate = DateTime.Now.AddDays(5), Type = AssignmentType.TuLuan, HanNop = "Chưa làm" },
                new Assignment { AssignmentID = 3, Title = "Bài tập 3: Trắc nghiệm Toán", Description = "Chọn đáp án đúng: 2 + 2 = ?\nA. 3\nB. 4\nC. 5\nD. 6", DueDate = DateTime.Now.AddDays(2), Type = AssignmentType.TracNghiem, HanNop = "Chưa làm" }
            };
        }

        private void LoadDataToGrid()
        {
            dgvAssignments.DataSource = null;
            dgvAssignments.DataSource = assignments.Select(a => new { a.AssignmentID, a.Title, a.Description, a.Type, HanNop = a.HanNop }).ToList();
        }

        private void DgvAssignments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int assignmentId = Convert.ToInt32(dgvAssignments.Rows[e.RowIndex].Cells[0].Value);
                var assignment = assignments.FirstOrDefault(a => a.AssignmentID == assignmentId);
                if (assignment != null && assignment.Type == AssignmentType.TuLuan)
                {
                    _parentForm.LoadUserControl(new ucHienThiBaiTap(assignment, _parentForm, assignments));
                }
            }
        }

        public class Assignment
        {
            public int AssignmentID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime DueDate { get; set; }
            public AssignmentType Type { get; set; }
            public string HanNop { get; set; } // Trạng thái
        }
        public enum AssignmentType { TuLuan, TracNghiem }
    }
}
