using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CNPM.Forms.Student
{
    public partial class ucBaiTap : UserControl
    {
        private string connectionString = "Data Source=.;Initial Catalog=EduMasterDB;Integrated Security=True";

        public ucBaiTap()
        {
            InitializeComponent();
            LoadAssignments();
        }

        private void LoadAssignments()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            a.AssignmentID,
                            c.CourseName,
                            a.Title,
                            a.Description,
                            a.DueDate,
                            a.MaxScore,
                            CASE 
                                WHEN s.SubmissionID IS NOT NULL THEN 'Đã nộp'
                                ELSE 'Chưa nộp'
                            END as SubmissionStatus
                        FROM Assignments a
                        INNER JOIN Courses c ON a.CourseID = c.CourseID
                        INNER JOIN CourseEnrollments ce ON c.CourseID = ce.CourseID
                        LEFT JOIN Submissions s ON a.AssignmentID = s.AssignmentID 
                            AND s.StudentID = @StudentID
                        WHERE ce.StudentID = @StudentID
                        ORDER BY a.DueDate DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // TODO: Replace with actual student ID from login session
                        cmd.Parameters.AddWithValue("@StudentID", 1); // Temporary hardcoded value

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dtGAssign.DataSource = dt;

                            // Configure column headers
                            if (dtGAssign.Columns.Count > 0)
                            {
                                dtGAssign.Columns["AssignmentID"].HeaderText = "Mã bài tập";
                                dtGAssign.Columns["CourseName"].HeaderText = "Khóa học";
                                dtGAssign.Columns["Title"].HeaderText = "Tiêu đề";
                                dtGAssign.Columns["Description"].HeaderText = "Mô tả";
                                dtGAssign.Columns["DueDate"].HeaderText = "Hạn nộp";
                                dtGAssign.Columns["MaxScore"].HeaderText = "Điểm tối đa";
                                dtGAssign.Columns["SubmissionStatus"].HeaderText = "Trạng thái";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách bài tập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
