using CNPM.BLL;
using CNPM.Models.Assignments;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

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
            LoadAssignments();
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
                if (submissions == null || submissions.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy bài nộp nào.");
                }
                else
                {
                    foreach (var submission in submissions)
                    {
                        MessageBox.Show($"AssignmentID: {submission.AssignmentID}, TeacherID: {TeacherID}");
                    }

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

        private void LoadAssignments()
        {
            try
            {
                var allAssignments = assignmentBLL.GetAssignmentsCreatedByTeacher(TeacherID);
                if (allAssignments == null || allAssignments.Count == 0)
                {
                    MessageBox.Show("Giáo viên chưa tạo bài tập nào.");
                    return;
                }
                var essayAssignments = new List<Assignments>();
                foreach (var assignment in allAssignments)
                {
                    string type = assignmentBLL.GetAssignmentType(assignment.AssignmentID);
                    if (type == "TuLuan")
                    {
                        essayAssignments.Add(assignment);
                    }
                }

                if (essayAssignments.Count == 0)
                {
                    MessageBox.Show("Không có bài tập tự luận nào.");
                    return;
                }

                cboAssignments.DataSource = essayAssignments;
                cboAssignments.DisplayMember = "Title";
                cboAssignments.ValueMember = "AssignmentID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách bài tập: " + ex.Message);
            }
        }
    }
}