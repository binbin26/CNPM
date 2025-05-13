using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CNPM.Forms.Student
{
    public partial class ucDangKyHocPhan : UserControl
    {
        private int _userId;
        private string _connectionString = "Data Source=.;Initial Catalog=EduMasterDB;Integrated Security=True";

        public ucDangKyHocPhan(int userId)
        {
            _userId = userId;
            InitializeComponent();
            LoadCourses();
        }

        private void LoadCourses(string search = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT c.CourseID, c.CourseName, u.FullName AS Teacher, 
                               c.MaxEnrollment - ISNULL(e.NumEnrolled,0) AS SlotsLeft,
                               c.RegistrationDeadline,
                               CASE 
                                   WHEN ce.EnrollmentID IS NOT NULL THEN N'Đã đăng ký'
                                   WHEN c.RegistrationDeadline < GETDATE() THEN N'Hết hạn'
                                   WHEN c.MaxEnrollment - ISNULL(e.NumEnrolled,0) <= 0 THEN N'Hết chỗ'
                                   ELSE N'Chưa đăng ký'
                               END AS Status
                        FROM Courses c
                        JOIN Users u ON c.TeacherID = u.UserID
                        LEFT JOIN (
                            SELECT CourseID, COUNT(*) AS NumEnrolled
                            FROM CourseEnrollments
                            GROUP BY CourseID
                        ) e ON c.CourseID = e.CourseID
                        LEFT JOIN CourseEnrollments ce ON ce.CourseID = c.CourseID AND ce.StudentID = @UserID
                        WHERE (@Search = '' OR c.CourseName LIKE '%' + @Search + '%')
                    ";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", _userId);
                        cmd.Parameters.AddWithValue("@Search", search);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvCourses.DataSource = dt;
                        dgvCourses.Columns["CourseID"].Visible = false;
                        dgvCourses.ReadOnly = true;
                        dgvCourses.AllowUserToAddRows = false;
                        dgvCourses.AllowUserToDeleteRows = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách học phần: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadCourses(txtSearch.Text.Trim());
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn học phần muốn đăng ký!");
                return;
            }
            var row = dgvCourses.SelectedRows[0];
            string status = row.Cells["Status"].Value.ToString();
            if (status != "Chưa đăng ký")
            {
                MessageBox.Show("Bạn không thể đăng ký học phần này!");
                return;
            }
            int courseId = Convert.ToInt32(row.Cells["CourseID"].Value);
            int slotsLeft = Convert.ToInt32(row.Cells["SlotsLeft"].Value);
            DateTime deadline = Convert.ToDateTime(row.Cells["RegistrationDeadline"].Value);
            if (slotsLeft <= 0)
            {
                MessageBox.Show("Học phần đã hết chỗ!");
                return;
            }
            if (deadline < DateTime.Now)
            {
                MessageBox.Show("Đã hết hạn đăng ký học phần này!");
                return;
            }
            // Đăng ký
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string insert = "INSERT INTO CourseEnrollments (StudentID, CourseID) VALUES (@StudentID, @CourseID)";
                    using (SqlCommand cmd = new SqlCommand(insert, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", _userId);
                        cmd.Parameters.AddWithValue("@CourseID", courseId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Đăng ký thành công!");
                LoadCourses(txtSearch.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng ký: " + ex.Message);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Đóng UserControl hoặc chuyển về màn hình trước
            this.Parent?.Controls.Remove(this);
        }
    }
}
