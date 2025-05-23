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
        private readonly UserBLL _userBLL;
        private string _username;
        public ucBaiTap(string username)
        {
            InitializeComponent();
            _assignmentBLL = new AssignmentBLL();
            _userBLL = new UserBLL();
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
                dtGAssign.Columns["CourseID"].HeaderText = "Mã khóa học";
                dtGAssign.Columns["Title"].HeaderText = "Tiêu đề";
                dtGAssign.Columns["Description"].HeaderText = "Mô tả";
                dtGAssign.Columns["DueDate"].HeaderText = "Hạn nộp";
                dtGAssign.Columns["MaxScore"].HeaderText = "Điểm tối đa";
                dtGAssign.Columns["SubmissionStatus"].HeaderText = "Trạng thái";
            }
        }
        private void dtGAssign_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;


            if (e.ColumnIndex == dtGAssign.Columns["btnLamBaiTap"].Index)
            {
                int assignmentId = Convert.ToInt32(dtGAssign.Rows[e.RowIndex].Cells["AssignmentID"].Value);
                DateTime dueDate = Convert.ToDateTime(dtGAssign.Rows[e.RowIndex].Cells["DueDate"].Value);
                string status = dtGAssign.Rows[e.RowIndex].Cells["SubmissionStatus"].Value?.ToString();
                if (DateTime.Now > dueDate)
                {
                    MessageBox.Show("Đã quá hạn nộp bài tập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (status == "Submitted")
                {
                    MessageBox.Show("Bạn đã nộp bài tập này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string loaiBaiTap = new AssignmentBLL().GetAssignmentType(assignmentId);
                var parent = this.Parent;
                int userId = _userBLL.GetUserId(_username);
                if (parent != null)
                {
                    try
                    {
                        if (loaiBaiTap == "TracNghiem")
                        {
                            if (_assignmentBLL.HasExceededMaxAttempts(assignmentId, userId))
                            {
                                MessageBox.Show("Bạn đã vượt quá số lần làm bài trắc nghiệm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            parent.Controls.Remove(this);
                            ucQuiz UcQuiz = new ucQuiz(assignmentId, userId);
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
                            MessageBox.Show("Không xác định được loại bài tập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi mở bài tập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
