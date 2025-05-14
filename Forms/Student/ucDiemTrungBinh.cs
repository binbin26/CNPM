using CNPM.BLL;
using CNPM.DAL;
using CNPM.Models.Users;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
            if (string.IsNullOrWhiteSpace(_username))
                return;
            User user = userBLL.GetUserByUsername(_username);

            if (user == null || user.Role != "Student")
            {
                MessageBox.Show("Không tìm thấy sinh viên hoặc tài khoản không hợp lệ.");
                return;
            }

            var courseBLL = new CourseBLL(new CourseDAL());
            var grades = courseBLL.GetGradesByStudent(user.UserID); // dùng đúng UserID từ username

            var dt = new DataTable();
            dt.Columns.Add("Tên khóa học");
            dt.Columns.Add("Điểm");
            dt.Columns.Add("Giáo viên chấm điểm");

            foreach (var grade in grades)
            {
                dt.Rows.Add(
                    grade.CourseName,
                    grade.Score.HasValue ? grade.Score.Value.ToString("0.00") : "Chưa có điểm",
                    grade.GradedBy
                );
            }

            dtGPoint.DataSource = dt;
        }
    }
}
