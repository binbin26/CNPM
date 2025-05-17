using CNPM.BLL;
using CNPM.DAL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CNPM.Forms.Student
{
    public partial class ucDangKyHocPhan : UserControl
    {
        private int _userId;
        private UserBLL userBLL = new UserBLL();
        private CourseBLL courseBLL = new CourseBLL(new CourseDAL());
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
                DataTable dt = userBLL.GetAvailableCourses(_userId, search);
                dgvCourses.DataSource = dt;
                dgvCourses.Columns["CourseID"].Visible = false;
                dgvCourses.ReadOnly = true;
                dgvCourses.AllowUserToAddRows = false;
                dgvCourses.AllowUserToDeleteRows = false;
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
            int courseId = Convert.ToInt32(row.Cells["CourseID"].Value);

            try
            {
                string result = courseBLL.RegisterStudentToCourse(_userId, courseId);
                MessageBox.Show(result);
                LoadCourses(txtSearch.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng ký: " + ex.Message);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Parent?.Controls.Remove(this);
        }
    }
}
