using CNPM.BLL;
using CNPM.Forms.Teacher.Usercontrol;
using CNPM.Models.Assignments;
using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Forms;

namespace CNPM.Forms.Student
{
    public partial class ucBaiTap : UserControl
    {
        private readonly AssignmentBLL _assignmentBLL;
        private string _username;
        public ucBaiTap(string username)
        {
            InitializeComponent();
            _assignmentBLL = new AssignmentBLL();
            _username = username;
            LoadAssignments();
            AddButtonColumn();
        }

        private void LoadAssignments()
        {
            List<Assignments> assignments = _assignmentBLL.GetAssignmentsForStudentWithStatus(_username);

            dtGAssign.DataSource = assignments;

            if (dtGAssign.Columns.Count > 0)
            {
                dtGAssign.Columns["AssignmentID"].HeaderText = "Mã bài tập";
                dtGAssign.Columns["CourseName"].HeaderText = "Khóa học";
                dtGAssign.Columns["Title"].HeaderText = "Tiêu đề";
                dtGAssign.Columns["Description"].HeaderText = "Mô tả";
                dtGAssign.Columns["DueDate"].HeaderText = "Hạn nộp";
                dtGAssign.Columns["MaxScore"].HeaderText = "Điểm tối đa";
                dtGAssign.Columns["CreatedBy"].HeaderText = "Người tạo";
                dtGAssign.Columns["AssignmentType"].HeaderText = "Loại bài tập";
                dtGAssign.Columns["SubmissionStatus"].HeaderText = "Trạng thái";
            }
        }
        private void dtGAssign_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var parent = this.Parent;
            if (e.RowIndex >= 0 && e.ColumnIndex == dtGAssign.Columns["btnLamBaiTap"].Index)
            {
                int assignmentId = Convert.ToInt32(dtGAssign.Rows[e.RowIndex].Cells["AssignmentID"].Value);
                string loaiBaiTap = dtGAssign.Rows[e.RowIndex].Cells["AssignmentType"].Value?.ToString();
                if (parent != null)
                {
                    if (loaiBaiTap == "TracNghiem")
                    {

                        parent.Controls.Remove(this);
                        ucQuiz UcQuiz = new ucQuiz(assignmentId);
                        UcQuiz.Dock = DockStyle.Fill;
                        parent.Controls.Add(UcQuiz);
                    }
                    else if (loaiBaiTap == "TuLuan")
                    {
                        parent.Controls.Remove(this);
                        ucEssay UcEssay = new ucEssay(assignmentId);
                        UcEssay.Dock = DockStyle.Fill;
                        parent.Controls.Add(UcEssay);
                    }
                    else
                    {
                        MessageBox.Show("Không xác định được loại bài tập.");
                    }
                }
            }
        }
    }
}
