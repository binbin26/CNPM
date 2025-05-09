using CNPM.BLL;
using CNPM.Models;
using System;
using System.Windows.Forms;

namespace CNPM.Forms
{
    public partial class EnrolledStudentsForm : Form
    {
        private readonly int _courseID;
        private readonly EnrollmentBLL _enrollmentBLL = new EnrollmentBLL();

        public EnrolledStudentsForm(int courseID)
        {
            InitializeComponent();
            _courseID = courseID;
            LoadEnrolledStudents();
        }

        // Tải danh sách sinh viên
        public void LoadEnrolledStudents()
        {
            try
            {
                var students = _enrollmentBLL.GetEnrolledStudents(_courseID);
                dataGridViewStudents.DataSource = students;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cấu hình DataGridView
        public void ConfigureDataGridView()
        {
            dataGridViewStudents.AutoGenerateColumns = false;
            dataGridViewStudents.Columns.Clear();

            // Thêm các cột
            dataGridViewStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StudentID",
                HeaderText = "Mã Sinh Viên"
            });

            dataGridViewStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FullName",
                HeaderText = "Họ và Tên"
            });

            dataGridViewStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email"
            });

            dataGridViewStudents.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "EnrollmentDate",
                HeaderText = "Ngày Đăng Ký"
            });

            // Thiết lập kích thước
            dataGridViewStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}