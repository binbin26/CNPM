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

            dtGProgress.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Đã hoàn thành",
                DataPropertyName = "SubmittedAssignments",
                Width = 100
            });

            dtGProgress.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Tỷ lệ hoàn thành",
                DataPropertyName = "CompletionRate",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "P0" }
            });

            dtGProgress.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Tổng điểm",
                DataPropertyName = "Grade",
                Width = 80
            });

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
                string fullName = _userBLL.GetUserByUsername(_username)?.FullName;
                lblUsername.Text = $"Sinh viên: {fullName}";

                var data = _userBLL.GetStudentProgress(_username);
                dtGProgress.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
