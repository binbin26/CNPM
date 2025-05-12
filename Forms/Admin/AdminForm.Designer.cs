using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using CNPM.DAL;
using CNPM.BLL;
using CNPM.Models.Users;

namespace CNPM.Forms.Admin
{
    public partial class AdminForm : Form
    {
        private readonly UserBLL _userBLL;
        private List<User> users;

        public AdminForm()
        {
            InitializeComponent();
            _userBLL = new UserBLL();
            InitializeUI();
            LoadUsers();
            UpdateDateLabel();
        }

        private void InitializeUI()
        {
            // Cấu hình form chính
            this.Text = "CNPM.Forms.Admin";
            this.Size = new Size(900, 650);
            this.BackColor = Color.FromArgb(245, 245, 245);

            // Header
            Label headerLabel = new Label();
            headerLabel.Text = "Welcome back, Admin!";
            headerLabel.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            headerLabel.ForeColor = Color.FromArgb(44, 62, 80);
            headerLabel.Location = new Point(20, 20);
            headerLabel.AutoSize = true;
            this.Controls.Add(headerLabel);

            // Date label
            dateLabel = new Label();
            dateLabel.Location = new Point(20, 70);
            dateLabel.AutoSize = true;
            dateLabel.ForeColor = Color.FromArgb(127, 140, 141);
            this.Controls.Add(dateLabel);

            // Tạo tab control
            TabControl tabControl = new TabControl();
            tabControl.Location = new Point(20, 100);
            tabControl.Size = new Size(840, 480);

            // Tab 1: Account List
            TabPage accountListTab = new TabPage("Account List");
            accountListTab.BackColor = Color.White;
            tabControl.TabPages.Add(accountListTab);

            // DataGridView cho danh sách tài khoản
            accountsDataGridView = new DataGridView();
            accountsDataGridView.Location = new Point(10, 10);
            accountsDataGridView.Size = new Size(800, 350);
            accountsDataGridView.BackgroundColor = Color.White;
            accountsDataGridView.BorderStyle = BorderStyle.None;
            accountsDataGridView.ReadOnly = true;
            accountsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            accountsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            accountsDataGridView.AllowUserToAddRows = false;
            accountsDataGridView.AllowUserToDeleteRows = false;
            accountsDataGridView.AllowUserToResizeRows = false;
            accountsDataGridView.RowHeadersVisible = false;
            accountsDataGridView.CellClick += AccountsDataGridView_CellClick;
            accountListTab.Controls.Add(accountsDataGridView);

            // Tab 2: Account Management
            TabPage accountManagementTab = new TabPage("Account Management");
            accountManagementTab.BackColor = Color.White;
            tabControl.TabPages.Add(accountManagementTab);

            // Panel quản lý tài khoản
            Panel managementPanel = new Panel();
            managementPanel.Location = new Point(10, 10);
            managementPanel.Size = new Size(800, 400);
            accountManagementTab.Controls.Add(managementPanel);

            // Nhóm thêm tài khoản
            GroupBox addGroup = new GroupBox();
            addGroup.Text = "Add New Account";
            addGroup.Location = new Point(10, 10);
            addGroup.Size = new Size(380, 250);
            managementPanel.Controls.Add(addGroup);

            // Controls thêm tài khoản
            Label lblUsername = new Label();
            lblUsername.Text = "Username:";
            lblUsername.Location = new Point(10, 30);
            lblUsername.AutoSize = true;
            addGroup.Controls.Add(lblUsername);

            txtNewUsername = new TextBox();
            txtNewUsername.Location = new Point(10, 50);
            txtNewUsername.Size = new Size(350, 20);
            addGroup.Controls.Add(txtNewUsername);

            Label lblFullName = new Label();
            lblFullName.Text = "Full Name:";
            lblFullName.Location = new Point(10, 80);
            lblFullName.AutoSize = true;
            addGroup.Controls.Add(lblFullName);

            txtNewFullName = new TextBox();
            txtNewFullName.Location = new Point(10, 100);
            txtNewFullName.Size = new Size(350, 20);
            addGroup.Controls.Add(txtNewFullName);

            Label lblPassword = new Label();
            lblPassword.Text = "Password:";
            lblPassword.Location = new Point(10, 130);
            lblPassword.AutoSize = true;
            addGroup.Controls.Add(lblPassword);

            txtNewPassword = new TextBox();
            txtNewPassword.Location = new Point(10, 150);
            txtNewPassword.Size = new Size(350, 20);
            txtNewPassword.PasswordChar = '*';
            addGroup.Controls.Add(txtNewPassword);

            Label lblRole = new Label();
            lblRole.Text = "Role:";
            lblRole.Location = new Point(10, 180);
            lblRole.AutoSize = true;
            addGroup.Controls.Add(lblRole);

            cmbNewRole = new ComboBox();
            cmbNewRole.Location = new Point(10, 200);
            cmbNewRole.Size = new Size(350, 20);
            cmbNewRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNewRole.Items.AddRange(new string[] { "Admin", "Teacher", "Student" });
            addGroup.Controls.Add(cmbNewRole);

            btnAddAccount = new Button();
            btnAddAccount.Text = "Add Account";
            btnAddAccount.Location = new Point(260, 270);
            btnAddAccount.Size = new Size(100, 30);
            btnAddAccount.BackColor = Color.FromArgb(52, 152, 219);
            btnAddAccount.ForeColor = Color.White;
            btnAddAccount.FlatStyle = FlatStyle.Flat;
            btnAddAccount.Click += BtnAddAccount_Click;
            managementPanel.Controls.Add(btnAddAccount);

            // Nhóm chỉnh sửa tài khoản
            GroupBox editGroup = new GroupBox();
            editGroup.Text = "Edit Account";
            editGroup.Location = new Point(400, 10);
            editGroup.Size = new Size(380, 180);
            managementPanel.Controls.Add(editGroup);

            Label lblSelectAccount = new Label();
            lblSelectAccount.Text = "Select Account:";
            lblSelectAccount.Location = new Point(10, 30);
            lblSelectAccount.AutoSize = true;
            editGroup.Controls.Add(lblSelectAccount);

            cmbAccounts = new ComboBox();
            cmbAccounts.Location = new Point(10, 50);
            cmbAccounts.Size = new Size(350, 20);
            cmbAccounts.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAccounts.SelectedIndexChanged += CmbAccounts_SelectedIndexChanged;
            editGroup.Controls.Add(cmbAccounts);

            Label lblEditUsername = new Label();
            lblEditUsername.Text = "Username:";
            lblEditUsername.Location = new Point(10, 80);
            lblEditUsername.AutoSize = true;
            editGroup.Controls.Add(lblEditUsername);

            txtEditUsername = new TextBox();
            txtEditUsername.Location = new Point(10, 100);
            txtEditUsername.Size = new Size(350, 20);
            editGroup.Controls.Add(txtEditUsername);

            Label lblEditRole = new Label();
            lblEditRole.Text = "Role:";
            lblEditRole.Location = new Point(10, 130);
            lblEditRole.AutoSize = true;
            editGroup.Controls.Add(lblEditRole);

            cmbEditRole = new ComboBox();
            cmbEditRole.Location = new Point(10, 150);
            cmbEditRole.Size = new Size(350, 20);
            cmbEditRole.Items.AddRange(new string[] { "Admin", "Teacher", "Student" });
            editGroup.Controls.Add(cmbEditRole);

            btnUpdateAccount = new Button();
            btnUpdateAccount.Text = "Update";
            btnUpdateAccount.Location = new Point(540, 200);
            btnUpdateAccount.Size = new Size(100, 30);
            btnUpdateAccount.BackColor = Color.FromArgb(52, 152, 219);
            btnUpdateAccount.ForeColor = Color.White;
            btnUpdateAccount.FlatStyle = FlatStyle.Flat;
            btnUpdateAccount.Click += BtnUpdateAccount_Click;
            managementPanel.Controls.Add(btnUpdateAccount);

            btnDeleteAccount = new Button();
            btnDeleteAccount.Text = "Delete";
            btnDeleteAccount.Location = new Point(650, 200);
            btnDeleteAccount.Size = new Size(100, 30);
            btnDeleteAccount.BackColor = Color.FromArgb(231, 76, 60);
            btnDeleteAccount.ForeColor = Color.White;
            btnDeleteAccount.FlatStyle = FlatStyle.Flat;
            btnDeleteAccount.Click += BtnDeleteAccount_Click;
            managementPanel.Controls.Add(btnDeleteAccount);

            this.Controls.Add(tabControl);
        }

        private void LoadUsers()
        {
            try
            {
                users = _userBLL.GetAllUsers();
                RefreshAccountList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDateLabel()
        {
            dateLabel.Text = "Today is " + DateTime.Now.ToString("dddd, MMMM dd, yyyy");
        }

        private void RefreshAccountList()
        {
            accountsDataGridView.Rows.Clear();
            accountsDataGridView.Columns.Clear();
            cmbAccounts.Items.Clear();

            // Thêm các cột vào DataGridView
            accountsDataGridView.Columns.Add("UserID", "ID");
            accountsDataGridView.Columns.Add("Username", "Username");
            accountsDataGridView.Columns.Add("Email", "Email");
            accountsDataGridView.Columns.Add("Role", "Role");

            // Thêm dữ liệu từ danh sách users
            foreach (var user in users)
            {
                accountsDataGridView.Rows.Add(
                    user.UserID,
                    user.Username,
                    user.Email,
                    user.Role
                );
                cmbAccounts.Items.Add($"{user.Username} ({user.Role})");
            }

            if (cmbAccounts.Items.Count > 0)
            {
                cmbAccounts.SelectedIndex = 0;
            }
        }

        private void AccountsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && accountsDataGridView.Rows[e.RowIndex].Cells[0].Value != null)
            {
                selectedAccountId = (int)accountsDataGridView.Rows[e.RowIndex].Cells[0].Value;
            }
        }

        private void CmbAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAccounts.SelectedIndex >= 0 && cmbAccounts.SelectedIndex < users.Count)
            {
                var user = users[cmbAccounts.SelectedIndex];
                selectedAccountId = user.UserID;
                txtEditUsername.Text = user.Username;
                cmbEditRole.SelectedItem = user.Role;
            }
        }

        private void BtnAddAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNewUsername.Text))
                {
                    MessageBox.Show("Please enter username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNewUsername.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNewFullName.Text))
                {
                    MessageBox.Show("Please enter full name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNewFullName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
                {
                    MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNewPassword.Focus();
                    return;
                }

                if (cmbNewRole.SelectedItem == null)
                {
                    MessageBox.Show("Please select a role", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbNewRole.Focus();
                    return;
                }

                var newUser = new User
                {
                    Username = txtNewUsername.Text.Trim(),
                    Password = txtNewPassword.Text.Trim(),
                    FullName = txtNewFullName.Text.Trim(),
                    Role = cmbNewRole.SelectedItem.ToString(),
                    Email = $"{txtNewUsername.Text.ToLower().Trim()}@school.com",
                    IsActive = true
                };

                if (_userBLL.AddUser(newUser))
                {
                    LoadUsers();
                    MessageBox.Show("Account added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear fields
                    txtNewUsername.Text = "";
                    txtNewPassword.Text = "";
                    txtNewFullName.Text = "";
                    cmbNewRole.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding account: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdateAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedAccountId == -1 || string.IsNullOrWhiteSpace(txtEditUsername.Text) || cmbEditRole.SelectedItem == null)
                {
                    MessageBox.Show("Please select an account and fill in all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var user = users.Find(u => u.UserID == selectedAccountId);
                if (user != null)
                {
                    user.Username = txtEditUsername.Text;
                    user.Role = cmbEditRole.SelectedItem.ToString();

                    if (_userBLL.UpdateUser(user))
                    {
                        LoadUsers();
                        MessageBox.Show("Account updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating account: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDeleteAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedAccountId == -1)
                {
                    MessageBox.Show("Please select an account to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var result = MessageBox.Show("Are you sure you want to delete this account?", "Confirm Delete", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                if (result == DialogResult.Yes)
                {
                    if (_userBLL.DeleteUser(selectedAccountId))
                    {
                        LoadUsers();
                        MessageBox.Show("Account deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting account: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Các controls
        private Label dateLabel;
        private DataGridView accountsDataGridView;
        private TextBox txtNewUsername;
        private TextBox txtNewFullName;
        private TextBox txtNewPassword;
        private ComboBox cmbNewRole;
        private Button btnAddAccount;
        private ComboBox cmbAccounts;
        private TextBox txtEditUsername;
        private ComboBox cmbEditRole;
        private Button btnUpdateAccount;
        private Button btnDeleteAccount;
        private int selectedAccountId = -1;
    }
}