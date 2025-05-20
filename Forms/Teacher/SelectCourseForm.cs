using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CNPM.BLL;
using CNPM.Models.Courses;
using CNPM.Models.Users;

namespace CNPM.Forms.Teacher
{
    public partial class SelectCourseForm: Form
    {
        private readonly List<Course> _courses;
        private readonly string _username;
        private CourseBLL _courseBLL = new CourseBLL(new CNPM.DAL.CourseDAL());
        UserBLL userBLL = new UserBLL();
        public SelectCourseForm(List<Course> courses, string username)
        {
            _courses = courses;
            _username = username;
            InitializeComponent();
            dtGCourseList.CellClick += dtGCourseList_CellClick;
            LoadCoursesList();
        }

        private void dtGCourseList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dtGCourseList.Columns[e.ColumnIndex].Name == "btnThongke")
            {
                int courseId = Convert.ToInt32(dtGCourseList.Rows[e.RowIndex].Cells["CourseID"].Value);
                string courseName = dtGCourseList.Rows[e.RowIndex].Cells["CourseName"].Value.ToString();

                var frm = new CourseProgress(courseId, courseName);
                frm.ShowDialog();
            }
        }

        private void LoadCoursesList()
        {
            User user = userBLL.GetUserByUsername(_username);
            if (user == null || user.Role != "Teacher")
            {
                MessageBox.Show("Không thể lấy thông tin Giảng viên.");
                return;
            }
            int studentId = user.UserID;
            List<Course> enrolledCourses = _courseBLL.GetCoursesByStudent(studentId);

            dtGCourseList.DataSource = enrolledCourses;

            // Thêm cột nút nếu chưa có
            if (!dtGCourseList.Columns.Contains("btnThongke"))
            {
                DataGridViewButtonColumn btnXem = new DataGridViewButtonColumn
                {
                    HeaderText = "Thốg kê",
                    Text = "Xem thống kê",
                    UseColumnTextForButtonValue = true,
                    Name = "btnThongke",
                    Width = 120
                };
                dtGCourseList.Columns.Add(btnXem);
            }
        }
    }
}
