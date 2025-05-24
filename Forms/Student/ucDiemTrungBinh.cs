using CNPM.BLL;
using CNPM.DAL;
using CNPM.Models.Users;
using System;
using System.Data;
using System.Windows.Forms;

namespace CNPM.Forms.Student
{
    public partial class ucDiemTrungBinh : UserControl
    {
        private string _username;
        UserBLL userBLL = new UserBLL(new UserDAL());
        public ucDiemTrungBinh(string username)
        {
            _username = username;
            InitializeComponent();
            LoadGradesByUsername();
        }

        private void LoadGradesByUsername()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_username))
                    return;

                User user = userBLL.GetUserByUsername(_username);

                if (user == null || user.Role != "Student")
                {
                    MessageBox.Show("Không tìm thấy sinh viên hoặc tài khoản không hợp lệ.");
                    return;
                }

                var courseBLL = new CourseBLL(new CourseDAL());
                var gradeVMs = courseBLL.GetGradesByStudent(user.UserID);

                var dt = new DataTable();
                dt.Columns.Add("Tên khóa học");
                dt.Columns.Add("Điểm trung bình");
                dt.Columns.Add("Giáo viên chấm điểm");

                foreach (var grade in gradeVMs)
                {
                    dt.Rows.Add(grade.CourseName, grade.ScoreText, grade.GradedBy);
                }

                dtGPoint.DataSource = dt;
                dtGPoint.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dtGPoint.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dtGPoint.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
    }
}
