using CNPM.BLL;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CNPM.Forms.Student
{
    public partial class Progress: Form
    {
        private readonly string _username;
        private readonly UserBLL _userBLL = new UserBLL();
        public Progress(string username)
        {
            _username = username;
            InitializeComponent();
            SetupDataGridView();
            LoadStudentProgress();
        }
        private void SetupDataGridView()
        {
            dtGProgress.Columns.Clear();
            dtGProgress.AutoGenerateColumns = false;
            dtGProgress.AllowUserToAddRows = false;
            dtGProgress.ReadOnly = true;
            dtGProgress.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dtGProgress.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Tên học phần",
                DataPropertyName = "CourseName",
                Width = 200
            });

            dtGProgress.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Số bài tập",
                DataPropertyName = "TotalAssignments",
                Width = 120
            });

            // Cột: Submitted
            dtGProgress.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Đã hoàn thành",
                DataPropertyName = "SubmittedAssignments",
                Width = 100
            });

            // Cột: Completion %
            dtGProgress.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Tỷ lệ hoàn thành",
                DataPropertyName = "CompletionRate",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "P0" } // 80% thay vì 0.8
            });

            // Cột: Grade
            dtGProgress.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Tổng điểm",
                DataPropertyName = "Grade",
                Width = 80
            });

            // Cột: Status
            dtGProgress.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Đánh giá",
                DataPropertyName = "Status",
                Width = 150
            });
        }

        private void LoadStudentProgress()
        {
            try
            {
                // Lấy họ tên
                string fullName = _userBLL.GetUserByUsername(_username)?.FullName;
                lblUsername.Text = $"Sinh viên: {fullName}";

                // Lấy danh sách tiến độ
                var data = _userBLL.GetStudentProgress(_username);
                dtGProgress.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải tiến độ học tập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
