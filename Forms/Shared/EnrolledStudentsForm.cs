using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using CNPM.BLL;
using CNPM.Models;
using CNPM.Models.Users;

namespace CNPM.Forms.Shared
{
    public partial class EnrolledStudentsForm : Form
    {
        private readonly int _courseId;
        private readonly CourseBLL _courseBLL;
        private readonly UserBLL _userBLL;
        private List<EnrolledStudent> enrolledStudents;
        private List<User> availableStudents;

        public EnrolledStudentsForm(int courseId,CourseBLL courseBLL, UserBLL userBLL)
        {
            InitializeComponent();
            _courseId = courseId;
            _courseBLL = courseBLL;
            _userBLL = userBLL;
            InitializeUI();
            LoadData();
        }

        private void InitializeUI()
        {
            this.Text = "Quản lý sinh viên";
            this.Size = new Size(800, 600);
            this.BackColor = Color.PaleTurquoise;

            // Panel chính
            Panel mainPanel = new Panel();
            mainPanel.Location = new Point(10, 10);
            mainPanel.Size = new Size(760, 540);
            this.Controls.Add(mainPanel);

            // Nhóm danh sách học sinh đã đăng ký
            GroupBox enrolledGroup = new GroupBox();
            enrolledGroup.Text = "Enrolled Students";
            enrolledGroup.Location = new Point(10, 10);
            enrolledGroup.Size = new Size(360, 400);
            mainPanel.Controls.Add(enrolledGroup);

            enrolledStudentsGrid = new DataGridView();
            enrolledStudentsGrid.Location = new Point(10, 20);
            enrolledStudentsGrid.Size = new Size(340, 370);
            enrolledStudentsGrid.BackgroundColor = Color.White;
            enrolledStudentsGrid.BorderStyle = BorderStyle.None;
            enrolledStudentsGrid.ReadOnly = true;
            enrolledStudentsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            enrolledStudentsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            enrolledStudentsGrid.AllowUserToAddRows = false;
            enrolledStudentsGrid.AllowUserToDeleteRows = false;
            enrolledStudentsGrid.AllowUserToResizeRows = false;
            enrolledStudentsGrid.RowHeadersVisible = false;
            enrolledGroup.Controls.Add(enrolledStudentsGrid);

            // Nhóm danh sách học sinh có thể đăng ký
            GroupBox availableGroup = new GroupBox();
            availableGroup.Text = "Available Students";
            availableGroup.Location = new Point(390, 10);
            availableGroup.Size = new Size(360, 400);
            mainPanel.Controls.Add(availableGroup);

            availableStudentsGrid = new DataGridView();
            availableStudentsGrid.Location = new Point(10, 20);
            availableStudentsGrid.Size = new Size(340, 370);
            availableStudentsGrid.BackgroundColor = Color.White;
            availableStudentsGrid.BorderStyle = BorderStyle.None;
            availableStudentsGrid.ReadOnly = true;
            availableStudentsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            availableStudentsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            availableStudentsGrid.AllowUserToAddRows = false;
            availableStudentsGrid.AllowUserToDeleteRows = false;
            availableStudentsGrid.AllowUserToResizeRows = false;
            availableStudentsGrid.RowHeadersVisible = false;
            availableGroup.Controls.Add(availableStudentsGrid);

            // Nút thêm học sinh
            btnAddStudent = new Button();
            btnAddStudent.Text = "Add Student";
            btnAddStudent.Location = new Point(390, 420);
            btnAddStudent.Size = new Size(120, 30);
            btnAddStudent.BackColor = Color.PowderBlue;
            btnAddStudent.ForeColor = Color.FromArgb(44, 62, 80);
            btnAddStudent.FlatStyle = FlatStyle.Flat;
            btnAddStudent.Click += BtnAddStudent_Click;
            mainPanel.Controls.Add(btnAddStudent);

            // Nút xóa học sinh
            btnRemoveStudent = new Button();
            btnRemoveStudent.Text = "Remove Student";
            btnRemoveStudent.Location = new Point(520, 420);
            btnRemoveStudent.Size = new Size(120, 30);
            btnRemoveStudent.BackColor = Color.PowderBlue;
            btnRemoveStudent.ForeColor = Color.FromArgb(44, 62, 80);
            btnRemoveStudent.FlatStyle = FlatStyle.Flat;
            btnRemoveStudent.Click += BtnRemoveStudent_Click;
            mainPanel.Controls.Add(btnRemoveStudent);
        }

        private void LoadData()
        {
            try
            {
                enrolledStudents = _courseBLL.GetEnrolledStudents(_courseId);
                RefreshEnrolledStudentsGrid();
                var allStudents = _userBLL.GetAllUsers().Where(u => u.Role == "Student").ToList();
                var enrolledStudentIds = enrolledStudents.Select(es => es.StudentID).ToList();
                availableStudents = allStudents.Where(s => !enrolledStudentIds.Contains(s.UserID)).ToList();
                RefreshAvailableStudentsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải dữ liệu: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshEnrolledStudentsGrid()
        {
            enrolledStudentsGrid.Rows.Clear();
            enrolledStudentsGrid.Columns.Clear();
            enrolledStudentsGrid.Columns.Add("StudentID", "ID");
            enrolledStudentsGrid.Columns.Add("FullName", "Full Name");
            enrolledStudentsGrid.Columns.Add("Email", "Email");

            foreach (var student in enrolledStudents)
            {
                enrolledStudentsGrid.Rows.Add(
                    student.StudentID,
                    student.FullName,
                    student.Email
                );
            }
        }

        private void RefreshAvailableStudentsGrid()
        {
            availableStudentsGrid.Rows.Clear();
            availableStudentsGrid.Columns.Clear();
            availableStudentsGrid.Columns.Add("UserID", "ID");
            availableStudentsGrid.Columns.Add("FullName", "Full Name");
            availableStudentsGrid.Columns.Add("Email", "Email");

            foreach (var student in availableStudents)
            {
                availableStudentsGrid.Rows.Add(
                    student.UserID,
                    student.FullName,
                    student.Email
                );
            }
        }

        private void BtnAddStudent_Click(object sender, EventArgs e)
        {
            if (availableStudentsGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hãy điền thông tiên Sinh viên cần thêm", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int studentId = (int)availableStudentsGrid.SelectedRows[0].Cells["UserID"].Value;
                if (_courseBLL.EnrollStudent(studentId, _courseId))
                {
                    LoadData();
                    MessageBox.Show("Thêm Sinh viên thành công", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm Sinh viên: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRemoveStudent_Click(object sender, EventArgs e)
        {
            if (enrolledStudentsGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hãy chọn Sinh viên muốn xóa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int studentId = (int)enrolledStudentsGrid.SelectedRows[0].Cells["StudentID"].Value;
                if (_courseBLL.RemoveStudent(studentId, _courseId))
                {
                    LoadData();
                    MessageBox.Show("Xóa Sinh viên thành công", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa Sinh viên: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataGridView enrolledStudentsGrid;
        private DataGridView availableStudentsGrid;
        private Button btnAddStudent;
        private Button btnRemoveStudent;
    }
}