using CNPM.BLL;
using CNPM.Models.Assignments;
using System;
using System.Windows.Forms;
using System.IO;

namespace CNPM.Forms.Teacher.Usercontrol
{
    public partial class UcSubmissions : UserControl
    {
        private int TeacherID;
        private AssignmentBLL assignmentBLL = new AssignmentBLL();
        public UcSubmissions(int teacherId)
        {
            InitializeComponent();
            TeacherID = teacherId;
        }

        private void dgvSubmissions_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSubmissions.CurrentRow?.DataBoundItem is EssaySubmissionDTO submission)
            {
                nudScore.Value = submission.Score.HasValue
                    ? Math.Min(nudScore.Maximum, submission.Score.Value)
                    : 0;
            }
        }

        private void cboAssignments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAssignments.SelectedItem is Assignments assignment)
            {
                LoadSubmissions(assignment.AssignmentID, TeacherID);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một bài tập.");
            }
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (cboAssignments.SelectedValue is int assignmentId)
                LoadSubmissions(assignmentId, TeacherID);
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (dgvSubmissions.CurrentRow?.DataBoundItem is EssaySubmissionDTO submission)
            {
                if (!string.IsNullOrEmpty(submission.FilePath) && File.Exists(submission.FilePath))
                {
                    System.Diagnostics.Process.Start(submission.FilePath);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy file.");
                }
            }
        }

        private void btnSubmitScore_Click(object sender, EventArgs e)
        {
            if (dgvSubmissions.CurrentRow?.DataBoundItem is EssaySubmissionDTO submission)
            {
                decimal score = nudScore.Value;
                try
                {
                    assignmentBLL.UpdateSubmissionScore(submission.AssignmentID, submission.StudentID, score, TeacherID);
                    MessageBox.Show("Chấm điểm thành công.");
                    LoadSubmissions(submission.AssignmentID, TeacherID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chấm điểm: " + ex.Message);
                }
            }
        }

        private void LoadSubmissions(int assignmentId, int teacherID)
        {
            try
            {
                var submissions = assignmentBLL.GetEssaySubmissions(assignmentId, teacherID);

                // Kiểm tra nếu danh sách rỗng hoặc null
                if (submissions == null || submissions.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy bài nộp nào.");
                }
                else
                {
                    // In ra các giá trị của AssignmentID và TeacherID để debug bằng MessageBox
                    foreach (var submission in submissions)
                    {
                        MessageBox.Show($"AssignmentID: {submission.AssignmentID}, TeacherID: {TeacherID}");
                    }

                    // Gán danh sách bài nộp vào DataGridView
                    dgvSubmissions.DataSource = submissions;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách bài nộp: " + ex.Message);
            }
        }

        private void btnQuiz_Click(object sender, EventArgs e)
        {
            var parent = this.Parent;
            parent.Controls.Remove(this);
            UcQuiz ucQuiz = new UcQuiz(TeacherID);
            ucQuiz.Dock = DockStyle.Fill;
            parent.Controls.Add(ucQuiz);
        }
    }
}