using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CNPM.BLL;
using CNPM.Models.Courses;
using CNPM.Models.Users;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CNPM.Forms.Student
{
    public partial class ucDanhSach : UserControl
    {
        private readonly string _username;
        private CourseBLL _courseBLL = new CourseBLL(new CNPM.DAL.CourseDAL());
        UserBLL userBLL = new UserBLL();
        public ucDanhSach(string username)
        {
            _username = username;
            InitializeComponent();
            dtGDanhSach.CellClick += dtGDanhSach_CellClick;
            LoadStudentCourses();
        }

        private void ShowAssignments(int courseID)
        {
            AssignmentBLL assignmentBLL = new AssignmentBLL();
            List<Assignment> assignments = assignmentBLL.GetAssignmentsByCourse(courseID);

            // Gọi form hiển thị bài tập, hoặc hiển thị ở nơi khác tùy ý bạn
            var frm = new TaiLieu(assignments); // cần tạo form này
            frm.ShowDialog();
        }

        private void dtGDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dtGDanhSach.Columns[e.ColumnIndex].Name == "btnXemTaiLieu")
            {
                int courseId = Convert.ToInt32(dtGDanhSach.Rows[e.RowIndex].Cells["CourseID"].Value);
                ShowAssignments(courseId);
            }
        }

        private void LoadStudentCourses()
        {
            User user = userBLL.GetUserByUsername(_username);
            if (user == null || user.Role != "Student")
            {
                MessageBox.Show("Không thể lấy thông tin sinh viên.");
                return;
            }
            int studentId = user.UserID;
            List<Course> enrolledCourses = _courseBLL.GetCoursesByStudent(studentId);

            dtGDanhSach.DataSource = enrolledCourses;

            // Thêm cột nút nếu chưa có
            if (!dtGDanhSach.Columns.Contains("btnXemTaiLieu"))
            {
                DataGridViewButtonColumn btnXem = new DataGridViewButtonColumn
                {
                    HeaderText = "Bài tập",
                    Text = "Xem tài liệu",
                    UseColumnTextForButtonValue = true,
                    Name = "btnXemTaiLieu",
                    Width = 120
                };
                dtGDanhSach.Columns.Add(btnXem);
            }
        }
    }
}
