namespace CNPM.Forms.Admin
{
    partial class AdminForm
    {
        // Các thành phần được thiết kế tự động
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "AdminForm";
            this.Text = "FormAdmin";
            this.ResumeLayout(false);

        }
    }
}




//using System;
//using System.Collections.Generic;
//using System.Windows.Forms;
//using System.Drawing;

//namespace CNPM.Forms.Admin
//{
//    public partial class AdminForm : Form
//    {
//        private List<Account> accounts = new List<Account>();
//        private int selectedAccountId = -1;

//        public AdminForm()
//        {
//            InitializeComponent();
//            InitializeUI();
//            LoadSampleData();
//            RefreshAccountList();
//            UpdateDateLabel();
//        }

//        private void InitializeUI()
//        {
//            // Cấu hình form chính
//            this.Text = "CNPM.Forms.Admin";
//            this.Size = new Size(900, 650);
//            this.BackColor = Color.FromArgb(245, 245, 245);

//            // Header
//            Label headerLabel = new Label();
//            headerLabel.Text = "Welcome back, Admin!";
//            headerLabel.Font = new Font("Segoe UI", 20, FontStyle.Bold);
//            headerLabel.ForeColor = Color.FromArgb(44, 62, 80);
//            headerLabel.Location = new Point(20, 20);
//            headerLabel.AutoSize = true;
//            this.Controls.Add(headerLabel);

//            // Date label
//            dateLabel = new Label();
//            dateLabel.Location = new Point(20, 70);
//            dateLabel.AutoSize = true;
//            dateLabel.ForeColor = Color.FromArgb(127, 140, 141);
//            this.Controls.Add(dateLabel);

//            // Tạo tab control
//            TabControl tabControl = new TabControl();
//            tabControl.Location = new Point(20, 100);
//            tabControl.Size = new Size(840, 480);

//            // Tab 1: Account List
//            TabPage accountListTab = new TabPage("Account List");
//            accountListTab.BackColor = Color.White;
//            tabControl.TabPages.Add(accountListTab);

//            // DataGridView cho danh sách tài khoản
//            accountsDataGridView = new DataGridView();
//            accountsDataGridView.Location = new Point(10, 10);
//            accountsDataGridView.Size = new Size(800, 350);
//            accountsDataGridView.BackgroundColor = Color.White;
//            accountsDataGridView.BorderStyle = BorderStyle.None;
//            accountsDataGridView.ReadOnly = true;
//            accountsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
//            accountsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
//            accountsDataGridView.CellClick += AccountsDataGridView_CellClick;
//            accountListTab.Controls.Add(accountsDataGridView);

//            // Tab 2: Account Management
//            TabPage accountManagementTab = new TabPage("Account Management");
//            accountManagementTab.BackColor = Color.White;
//            tabControl.TabPages.Add(accountManagementTab);

//            // Panel quản lý tài khoản
//            Panel managementPanel = new Panel();
//            managementPanel.Location = new Point(10, 10);
//            managementPanel.Size = new Size(800, 400);
//            accountManagementTab.Controls.Add(managementPanel);

//            // Nhóm thêm tài khoản
//            GroupBox addGroup = new GroupBox();
//            addGroup.Text = "Add New Account";
//            addGroup.Location = new Point(10, 10);
//            addGroup.Size = new Size(380, 180);
//            managementPanel.Controls.Add(addGroup);

//            // Controls thêm tài khoản
//            Label lblUsername = new Label();
//            lblUsername.Text = "Username:";
//            lblUsername.Location = new Point(10, 30);
//            lblUsername.AutoSize = true;
//            addGroup.Controls.Add(lblUsername);

//            txtNewUsername = new TextBox();
//            txtNewUsername.Location = new Point(10, 50);
//            txtNewUsername.Size = new Size(350, 20);
//            addGroup.Controls.Add(txtNewUsername);

//            Label lblPassword = new Label();
//            lblPassword.Text = "Password:";
//            lblPassword.Location = new Point(10, 80);
//            lblPassword.AutoSize = true;
//            addGroup.Controls.Add(lblPassword);

//            txtNewPassword = new TextBox();
//            txtNewPassword.Location = new Point(10, 100);
//            txtNewPassword.Size = new Size(350, 20);
//            txtNewPassword.PasswordChar = '*';
//            addGroup.Controls.Add(txtNewPassword);

//            Label lblRole = new Label();
//            lblRole.Text = "Role:";
//            lblRole.Location = new Point(10, 130);
//            lblRole.AutoSize = true;
//            addGroup.Controls.Add(lblRole);

//            cmbNewRole = new ComboBox();
//            cmbNewRole.Location = new Point(10, 150);
//            cmbNewRole.Size = new Size(350, 20);
//            cmbNewRole.Items.AddRange(new string[] { "Admin", "Teacher", "Student" });
//            addGroup.Controls.Add(cmbNewRole);

//            btnAddAccount = new Button();
//            btnAddAccount.Text = "Add Account";
//            btnAddAccount.Location = new Point(260, 200);
//            btnAddAccount.Size = new Size(100, 30);
//            btnAddAccount.BackColor = Color.FromArgb(52, 152, 219);
//            btnAddAccount.ForeColor = Color.White;
//            btnAddAccount.FlatStyle = FlatStyle.Flat;
//            btnAddAccount.Click += BtnAddAccount_Click;
//            managementPanel.Controls.Add(btnAddAccount);

//            // Nhóm chỉnh sửa tài khoản
//            GroupBox editGroup = new GroupBox();
//            editGroup.Text = "Edit Account";
//            editGroup.Location = new Point(400, 10);
//            editGroup.Size = new Size(380, 180);
//            managementPanel.Controls.Add(editGroup);

//            Label lblSelectAccount = new Label();
//            lblSelectAccount.Text = "Select Account:";
//            lblSelectAccount.Location = new Point(10, 30);
//            lblSelectAccount.AutoSize = true;
//            editGroup.Controls.Add(lblSelectAccount);

//            cmbAccounts = new ComboBox();
//            cmbAccounts.Location = new Point(10, 50);
//            cmbAccounts.Size = new Size(350, 20);
//            cmbAccounts.DropDownStyle = ComboBoxStyle.DropDownList;
//            cmbAccounts.SelectedIndexChanged += CmbAccounts_SelectedIndexChanged;
//            editGroup.Controls.Add(cmbAccounts);

//            Label lblEditUsername = new Label();
//            lblEditUsername.Text = "Username:";
//            lblEditUsername.Location = new Point(10, 80);
//            lblEditUsername.AutoSize = true;
//            editGroup.Controls.Add(lblEditUsername);

//            txtEditUsername = new TextBox();
//            txtEditUsername.Location = new Point(10, 100);
//            txtEditUsername.Size = new Size(350, 20);
//            editGroup.Controls.Add(txtEditUsername);

//            Label lblEditRole = new Label();
//            lblEditRole.Text = "Role:";
//            lblEditRole.Location = new Point(10, 130);
//            lblEditRole.AutoSize = true;
//            editGroup.Controls.Add(lblEditRole);

//            cmbEditRole = new ComboBox();
//            cmbEditRole.Location = new Point(10, 150);
//            cmbEditRole.Size = new Size(350, 20);
//            cmbEditRole.Items.AddRange(new string[] { "Admin", "Teacher", "Student" });
//            editGroup.Controls.Add(cmbEditRole);

//            btnUpdateAccount = new Button();
//            btnUpdateAccount.Text = "Update";
//            btnUpdateAccount.Location = new Point(540, 200);
//            btnUpdateAccount.Size = new Size(100, 30);
//            btnUpdateAccount.BackColor = Color.FromArgb(52, 152, 219);
//            btnUpdateAccount.ForeColor = Color.White;
//            btnUpdateAccount.FlatStyle = FlatStyle.Flat;
//            btnUpdateAccount.Click += BtnUpdateAccount_Click;
//            managementPanel.Controls.Add(btnUpdateAccount);

//            btnDeleteAccount = new Button();
//            btnDeleteAccount.Text = "Delete";
//            btnDeleteAccount.Location = new Point(650, 200);
//            btnDeleteAccount.Size = new Size(100, 30);
//            btnDeleteAccount.BackColor = Color.FromArgb(231, 76, 60);
//            btnDeleteAccount.ForeColor = Color.White;
//            btnDeleteAccount.FlatStyle = FlatStyle.Flat;
//            btnDeleteAccount.Click += BtnDeleteAccount_Click;
//            managementPanel.Controls.Add(btnDeleteAccount);

//            // Tab 3: Role Management
//            TabPage roleManagementTab = new TabPage("Role Management");
//            roleManagementTab.BackColor = Color.White;
//            tabControl.TabPages.Add(roleManagementTab);

//            // Panel quản lý phân quyền
//            Panel rolePanel = new Panel();
//            rolePanel.Location = new Point(10, 10);
//            rolePanel.Size = new Size(800, 400);
//            roleManagementTab.Controls.Add(rolePanel);

//            // DataGridView cho phân quyền
//            rolesDataGridView = new DataGridView();
//            rolesDataGridView.Location = new Point(10, 10);
//            rolesDataGridView.Size = new Size(780, 300);
//            rolesDataGridView.BackgroundColor = Color.White;
//            rolesDataGridView.BorderStyle = BorderStyle.None;
//            rolesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
//            rolePanel.Controls.Add(rolesDataGridView);

//            // Nút cập nhật phân quyền
//            btnUpdateRoles = new Button();
//            btnUpdateRoles.Text = "Update Permissions";
//            btnUpdateRoles.Location = new Point(600, 320);
//            btnUpdateRoles.Size = new Size(180, 40);
//            btnUpdateRoles.BackColor = Color.FromArgb(52, 152, 219);
//            btnUpdateRoles.ForeColor = Color.White;
//            btnUpdateRoles.FlatStyle = FlatStyle.Flat;
//            btnUpdateRoles.Click += BtnUpdateRoles_Click;
//            rolePanel.Controls.Add(btnUpdateRoles);

//            this.Controls.Add(tabControl);
//        }

//        private void LoadSampleData()
//        {
//            accounts.Add(new Account(1, "admin", "admin@school.com", "Admin"));
//            accounts.Add(new Account(2, "teacher1", "teacher1@school.com", "Teacher"));
//            accounts.Add(new Account(3, "student1", "student1@school.com", "Student"));
//            accounts.Add(new Account(4, "student2", "student2@school.com", "Student"));
//        }

//        private void UpdateDateLabel()
//        {
//            dateLabel.Text = "Today is " + DateTime.Now.ToString("dddd, MMMM dd, yyyy");
//        }

//        private void RefreshAccountList()
//        {
//            accountsDataGridView.Rows.Clear();
//            cmbAccounts.Items.Clear();

//            foreach (var account in accounts)
//            {
//                accountsDataGridView.Rows.Add(
//                    account.Id,
//                    account.Username,
//                    account.Email,
//                    account.Role
//                );

//                cmbAccounts.Items.Add($"{account.Username} ({account.Role})");
//            }

//            if (cmbAccounts.Items.Count > 0)
//            {
//                cmbAccounts.SelectedIndex = 0;
//            }
//        }

//        private void RefreshRolesGrid()
//        {
//            rolesDataGridView.Rows.Clear();
//            rolesDataGridView.Columns.Clear();

//            // Thêm các cột
//            rolesDataGridView.Columns.Add("Permission", "Permission");
//            rolesDataGridView.Columns.Add("Admin", "Admin");
//            rolesDataGridView.Columns.Add("Teacher", "Teacher");
//            rolesDataGridView.Columns.Add("Student", "Student");

//            // Thêm dữ liệu mẫu
//            rolesDataGridView.Rows.Add("Manage Accounts", "Yes", "No", "No");
//            rolesDataGridView.Rows.Add("Create Courses", "Yes", "Yes", "No");
//            rolesDataGridView.Rows.Add("View Grades", "Yes", "Yes", "Yes (own only)");
//            rolesDataGridView.Rows.Add("Submit Assignments", "No", "No", "Yes");
//            rolesDataGridView.Rows.Add("Modify System Settings", "Yes", "No", "No");

//            // Đặt kiểu cho các ô
//            foreach (DataGridViewRow row in rolesDataGridView.Rows)
//            {
//                for (int i = 1; i < rolesDataGridView.Columns.Count; i++)
//                {
//                    row.Cells[i].Style.BackColor = Color.LightGray;
//                }
//            }
//        }

//        private void AccountsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.RowIndex >= 0)
//            {
//                selectedAccountId = (int)accountsDataGridView.Rows[e.RowIndex].Cells[0].Value;
//                cmbAccounts.SelectedIndex = e.RowIndex;
//            }
//        }

//        private void CmbAccounts_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            if (cmbAccounts.SelectedIndex >= 0 && cmbAccounts.SelectedIndex < accounts.Count)
//            {
//                var account = accounts[cmbAccounts.SelectedIndex];
//                selectedAccountId = account.Id;
//                txtEditUsername.Text = account.Username;
//                cmbEditRole.SelectedItem = account.Role;
//            }
//        }

//        private void BtnAddAccount_Click(object sender, EventArgs e)
//        {
//            if (string.IsNullOrWhiteSpace(txtNewUsername.Text) ||
//                string.IsNullOrWhiteSpace(txtNewPassword.Text) ||
//                cmbNewRole.SelectedItem == null)
//            {
//                MessageBox.Show("Please fill in all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            int newId = accounts.Count > 0 ? accounts[accounts.Count - 1].Id + 1 : 1;
//            accounts.Add(new Account(
//                newId,
//                txtNewUsername.Text,
//                $"{txtNewUsername.Text.ToLower()}@school.com",
//                cmbNewRole.SelectedItem.ToString()
//            ));

//            RefreshAccountList();
//            MessageBox.Show("Account added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

//            // Clear fields
//            txtNewUsername.Text = "";
//            txtNewPassword.Text = "";
//            cmbNewRole.SelectedIndex = -1;
//        }

//        private void BtnUpdateAccount_Click(object sender, EventArgs e)
//        {
//            if (selectedAccountId == -1 || string.IsNullOrWhiteSpace(txtEditUsername.Text) || cmbEditRole.SelectedItem == null)
//            {
//                MessageBox.Show("Please select an account and fill in all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            var account = accounts.Find(a => a.Id == selectedAccountId);
//            if (account != null)
//            {
//                account.Username = txtEditUsername.Text;
//                account.Role = cmbEditRole.SelectedItem.ToString();
//                RefreshAccountList();
//                MessageBox.Show("Account updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//        }

//        private void BtnDeleteAccount_Click(object sender, EventArgs e)
//        {
//            if (selectedAccountId == -1)
//            {
//                MessageBox.Show("Please select an account to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            var result = MessageBox.Show("Are you sure you want to delete this account?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
//            if (result == DialogResult.Yes)
//            {
//                accounts.RemoveAll(a => a.Id == selectedAccountId);
//                selectedAccountId = -1;
//                RefreshAccountList();
//                MessageBox.Show("Account deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//        }

//        private void BtnUpdateRoles_Click(object sender, EventArgs e)
//        {
//            MessageBox.Show("Role permissions updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//        }

//        // Các controls
//        private Label dateLabel;
//        private DataGridView accountsDataGridView;
//        private DataGridView rolesDataGridView;
//        private TextBox txtNewUsername;
//        private TextBox txtNewPassword;
//        private ComboBox cmbNewRole;
//        private Button btnAddAccount;
//        private ComboBox cmbAccounts;
//        private TextBox txtEditUsername;
//        private ComboBox cmbEditRole;
//        private Button btnUpdateAccount;
//        private Button btnDeleteAccount;
//        private Button btnUpdateRoles;

//        private void AdminForm_Load(object sender, EventArgs e)
//        {
//            RefreshRolesGrid();
//        }
//    }

//    public class Account
//    {
//        public int Id { get; set; }
//        public string Username { get; set; }
//        public string Email { get; set; }
//        public string Role { get; set; }

//        public Account(int id, string username, string email, string role)
//        {
//            Id = id;
//            Username = username;
//            Email = email;
//            Role = role;
//        }
//    }
//}