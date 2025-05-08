using CNPM.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM.Forms.Admin
{
    public partial class UserManagementForm : Form
    {
        private readonly UserDAL _userDAL = new UserDAL();

        public UserManagementForm()
        {
            InitializeComponent();
            ConfigureDataGridView();
            LoadUserList();
        }

        // Cấu hình DataGridView
        private void ConfigureDataGridView()
        {
            // Tắt tự động tạo cột
            dataGridViewUsers.AutoGenerateColumns = false;

            // Thêm cột thủ công
            dataGridViewUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UserID",  // Map đến thuộc tính UserID của lớp User
                HeaderText = "Mã người dùng"
            });

            dataGridViewUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Username",
                HeaderText = "Tên đăng nhập"
            });

            dataGridViewUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Role",
                HeaderText = "Vai trò"
            });

            // Thiết lập kích thước và style
            dataGridViewUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        // Tải dữ liệu từ database
        private void LoadUserList()
        {
            try
            {
                dataGridViewUsers.DataSource = _userDAL.GetAllUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUserList();
        }
    }
}
