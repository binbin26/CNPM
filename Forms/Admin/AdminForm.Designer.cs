using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using CNPM.DAL;
using CNPM.BLL;
using CNPM.Models.Users;
using CNPM.Models.Courses;
using CNPM.Forms.Shared;
using System.IO;
using System.Data.SqlClient;

namespace CNPM.Forms.Admin
{
    public partial class AdminForm : Form
    {
        private readonly UserBLL _userBLL;
        private readonly CourseBLL _courseBLL;
        private readonly int _courseId;
        private List<User> users;
        private List<Course> courses;
        private readonly IUserContext _userContext;
        private TextBox txtFullName;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private TextBox txtHometown;
        private TextBox txtRole;
        private Label lblFullNameValue;
        private Label lblEmailValue;
        private Label lblPhoneValue;
        private Label lblHometownValue;
        private Label lblRoleValue;
        private PictureBox picAvatar;

        public AdminForm()
        {
            InitializeComponent();
            _userBLL = new UserBLL();
            _courseBLL = new CourseBLL(new CourseDAL());
            _userContext = (IUserContext)Program.ServiceProvider.GetService(typeof(IUserContext));
            InitializeUI();
            LoadUsers();
            LoadCourses();
            UpdateDateLabel();
            LoadAdminInfo();
        }

        private void InitializeUI()
        {
            // Cấu hình form chính
            this.Text = "CNPM.Forms.Admin";
            this.Size = new Size(900, 650);
            this.BackColor = Color.PaleTurquoise;

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
            dateLabel.ForeColor = Color.FromArgb(44, 62, 80);
            this.Controls.Add(dateLabel);

            // Tạo tab control
            tabControl = new TabControl();
            tabControl.Location = new Point(20, 100);
            tabControl.Size = new Size(840, 480);

            // Tab Personal Information
            TabPage personalInfoTab = new TabPage("Personal Information");
            personalInfoTab.BackColor = Color.White;

            // Panel Personal Info: padding nhỏ hơn
            Panel infoPanel = new Panel();
            infoPanel.Dock = DockStyle.Fill;
            infoPanel.BackColor = Color.White;
            infoPanel.BorderStyle = BorderStyle.FixedSingle;
            infoPanel.Padding = new Padding(15, 15, 15, 15);
            personalInfoTab.Controls.Add(infoPanel);

            // Đặt lại yPos = 20 để kéo thấp phần thông tin vừa phải
            int yPos = 20;
            int labelWidth = 110;
            int valueWidth = 180;
            int spacing = 28;
            Color labelColor = Color.FromArgb(44, 62, 80);
            Color valueColor = Color.FromArgb(52, 73, 94);
            Font labelFont = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            Font valueFont = new Font("Segoe UI", 9.5f, FontStyle.Regular);

            // Tiêu đề căn giữa, font vừa phải
            Label titleLabel = new Label();
            titleLabel.Text = "Thông tin Admin";
            titleLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            titleLabel.ForeColor = labelColor;
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.Dock = DockStyle.Top;
            titleLabel.Height = 36;
            personalInfoTab.Controls.Add(titleLabel);

            // Họ và tên
            Label lblFullName = new Label();
            lblFullName.Text = "Họ và tên:";
            lblFullName.Location = new Point(25, yPos);
            lblFullName.AutoSize = true;
            lblFullName.Font = labelFont;
            lblFullName.ForeColor = labelColor;
            infoPanel.Controls.Add(lblFullName);

            lblFullNameValue = new Label();
            lblFullNameValue.Location = new Point(25 + labelWidth + 10, yPos);
            lblFullNameValue.Size = new Size(valueWidth, 22);
            lblFullNameValue.Name = "lblFullNameValue";
            lblFullNameValue.Font = valueFont;
            lblFullNameValue.ForeColor = valueColor;
            infoPanel.Controls.Add(lblFullNameValue);

            yPos += spacing;
            // Email
            Label lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.Location = new Point(25, yPos);
            lblEmail.AutoSize = true;
            lblEmail.Font = labelFont;
            lblEmail.ForeColor = labelColor;
            infoPanel.Controls.Add(lblEmail);

            lblEmailValue = new Label();
            lblEmailValue.Location = new Point(25 + labelWidth + 10, yPos);
            lblEmailValue.Size = new Size(valueWidth, 22);
            lblEmailValue.Name = "lblEmailValue";
            lblEmailValue.Font = valueFont;
            lblEmailValue.ForeColor = valueColor;
            infoPanel.Controls.Add(lblEmailValue);

            yPos += spacing;
            // Số điện thoại
            Label lblPhone = new Label();
            lblPhone.Text = "Số điện thoại:";
            lblPhone.Location = new Point(25, yPos);
            lblPhone.AutoSize = true;
            lblPhone.Font = labelFont;
            lblPhone.ForeColor = labelColor;
            infoPanel.Controls.Add(lblPhone);

            lblPhoneValue = new Label();
            lblPhoneValue.Location = new Point(25 + labelWidth + 10, yPos);
            lblPhoneValue.Size = new Size(valueWidth, 22);
            lblPhoneValue.Name = "lblPhoneValue";
            lblPhoneValue.Font = valueFont;
            lblPhoneValue.ForeColor = valueColor;
            infoPanel.Controls.Add(lblPhoneValue);

            yPos += spacing;
            // Quê quán
            Label lblHometown = new Label();
            lblHometown.Text = "Quê quán:";
            lblHometown.Location = new Point(25, yPos);
            lblHometown.AutoSize = true;
            lblHometown.Font = labelFont;
            lblHometown.ForeColor = labelColor;
            infoPanel.Controls.Add(lblHometown);

            lblHometownValue = new Label();
            lblHometownValue.Location = new Point(25 + labelWidth + 10, yPos);
            lblHometownValue.Size = new Size(valueWidth, 22);
            lblHometownValue.Name = "lblHometownValue";
            lblHometownValue.Font = valueFont;
            lblHometownValue.ForeColor = valueColor;
            infoPanel.Controls.Add(lblHometownValue);

            yPos += spacing;
            // Chức vụ
            Label lblRole = new Label();
            lblRole.Text = "Chức vụ:";
            lblRole.Location = new Point(25, yPos);
            lblRole.AutoSize = true;
            lblRole.Font = labelFont;
            lblRole.ForeColor = labelColor;
            infoPanel.Controls.Add(lblRole);

            lblRoleValue = new Label();
            lblRoleValue.Location = new Point(25 + labelWidth + 10, yPos);
            lblRoleValue.Size = new Size(valueWidth, 22);
            lblRoleValue.Name = "lblRoleValue";
            lblRoleValue.Font = valueFont;
            lblRoleValue.ForeColor = valueColor;
            infoPanel.Controls.Add(lblRoleValue);

            yPos += spacing * 2;
            // Đổi mật khẩu
            Label lblOldPassword = new Label();
            lblOldPassword.Text = "Mật khẩu cũ:";
            lblOldPassword.Location = new Point(25, yPos);
            lblOldPassword.AutoSize = true;
            lblOldPassword.Font = labelFont;
            infoPanel.Controls.Add(lblOldPassword);

            txtOldPassword = new TextBox();
            txtOldPassword.Location = new Point(25 + labelWidth + 10, yPos);
            txtOldPassword.Size = new Size(200, 20);
            txtOldPassword.Name = "txtOldPassword";
            txtOldPassword.PasswordChar = '*';
            txtOldPassword.Font = valueFont;
            infoPanel.Controls.Add(txtOldPassword);

            yPos += spacing;
            Label lblNewPassword = new Label();
            lblNewPassword.Text = "Mật khẩu mới:";
            lblNewPassword.Location = new Point(25, yPos);
            lblNewPassword.AutoSize = true;
            lblNewPassword.Font = labelFont;
            infoPanel.Controls.Add(lblNewPassword);

            txtChangePassword = new TextBox();
            txtChangePassword.Location = new Point(25 + labelWidth + 10, yPos);
            txtChangePassword.Size = new Size(200, 20);
            txtChangePassword.Name = "txtChangePassword";
            txtChangePassword.PasswordChar = '*';
            txtChangePassword.Font = valueFont;
            infoPanel.Controls.Add(txtChangePassword);

            Button btnChangePassword = new Button();
            btnChangePassword.Text = "Đổi mật khẩu";
            btnChangePassword.Location = new Point(25 + labelWidth + 220, yPos - 2);
            btnChangePassword.Size = new Size(100, 26);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.BackColor = Color.FromArgb(0, 122, 204);
            btnChangePassword.ForeColor = Color.White;
            btnChangePassword.FlatStyle = FlatStyle.Flat;
            btnChangePassword.FlatAppearance.BorderSize = 0;
            btnChangePassword.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnChangePassword.Click += BtnChangePassword_Click;
            infoPanel.Controls.Add(btnChangePassword);

            yPos += spacing * 2;
            Button btnLogout = new Button();
            btnLogout.Text = "Đăng xuất";
            btnLogout.Location = new Point(25, yPos);
            btnLogout.Size = new Size(100, 28);
            btnLogout.Name = "btnLogout";
            btnLogout.BackColor = Color.FromArgb(0, 122, 204); // xanh dương đậm
            btnLogout.ForeColor = Color.White;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnLogout.Click += (s, e) => { this.Close(); };
            infoPanel.Controls.Add(btnLogout);

            // Thêm PictureBox và nút Change Avatar ở bên phải panel
            int avatarBoxX = 420; // hoặc phù hợp với chiều rộng panel
            int avatarBoxY = 30;
            int avatarBoxSize = 100;

            picAvatar = new PictureBox();
            picAvatar.Name = "picAvatar";
            picAvatar.Location = new Point(avatarBoxX, avatarBoxY);
            picAvatar.Size = new Size(avatarBoxSize, avatarBoxSize);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picAvatar.BorderStyle = BorderStyle.FixedSingle;
            infoPanel.Controls.Add(picAvatar);

            Button btnChangeAvatar = new Button();
            btnChangeAvatar.Text = "Change Avatar";
            btnChangeAvatar.Name = "btnChangeAvatar";
            btnChangeAvatar.Location = new Point(avatarBoxX, avatarBoxY + avatarBoxSize + 10);
            btnChangeAvatar.Size = new Size(avatarBoxSize, 28);
            btnChangeAvatar.BackColor = Color.FromArgb(0, 122, 204);
            btnChangeAvatar.ForeColor = Color.White;
            btnChangeAvatar.FlatStyle = FlatStyle.Flat;
            btnChangeAvatar.FlatAppearance.BorderSize = 0;
            btnChangeAvatar.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnChangeAvatar.Click += BtnChangeAvatar_Click;
            infoPanel.Controls.Add(btnChangeAvatar);

            tabControl.TabPages.Add(personalInfoTab);

            // Tab 1: Account List
            TabPage accountListTab = new TabPage("Account List");
            accountListTab.BackColor = Color.AliceBlue;
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
            accountManagementTab.BackColor = Color.AliceBlue;
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
            addGroup.Size = new Size(380, 370);
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

            Label lblNewFullName = new Label();
            lblNewFullName.Text = "Full Name:";
            lblNewFullName.Location = new Point(10, 80);
            lblNewFullName.AutoSize = true;
            addGroup.Controls.Add(lblNewFullName);

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

            Label lblNewRole = new Label();
            lblNewRole.Text = "Role:";
            lblNewRole.Location = new Point(10, 180);
            lblNewRole.AutoSize = true;
            addGroup.Controls.Add(lblNewRole);

            cmbNewRole = new ComboBox();
            cmbNewRole.Location = new Point(10, 200);
            cmbNewRole.Size = new Size(350, 20);
            cmbNewRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNewRole.Items.AddRange(new string[] { "Admin", "Teacher", "Student" });
            addGroup.Controls.Add(cmbNewRole);

            Label lblQueQuan = new Label();
            lblQueQuan.Text = "Quê quán:";
            lblQueQuan.Location = new Point(10, 230);
            lblQueQuan.AutoSize = true;
            addGroup.Controls.Add(lblQueQuan);

            txtNewQueQuan = new TextBox();
            txtNewQueQuan.Location = new Point(10, 250);
            txtNewQueQuan.Size = new Size(350, 20);
            addGroup.Controls.Add(txtNewQueQuan);

            Label lblSoDienThoai = new Label();
            lblSoDienThoai.Text = "Số điện thoại:";
            lblSoDienThoai.Location = new Point(10, 280);
            lblSoDienThoai.AutoSize = true;
            addGroup.Controls.Add(lblSoDienThoai);

            txtNewSoDienThoai = new TextBox();
            txtNewSoDienThoai.Location = new Point(10, 300);
            txtNewSoDienThoai.Size = new Size(350, 20);
            addGroup.Controls.Add(txtNewSoDienThoai);

            btnAddAccount = new Button();
            btnAddAccount.Text = "Add Account";
            btnAddAccount.Location = new Point(250, 320);
            btnAddAccount.Size = new Size(100, 30);
            btnAddAccount.BackColor = Color.PowderBlue;
            btnAddAccount.ForeColor = Color.FromArgb(44, 62, 80);
            btnAddAccount.FlatStyle = FlatStyle.Flat;
            btnAddAccount.Click += BtnAddAccount_Click;
            addGroup.Controls.Add(btnAddAccount);

            // Nhóm chỉnh sửa tài khoản
            GroupBox editGroup = new GroupBox();
            editGroup.Text = "Edit Account";
            editGroup.Location = new Point(400, 10);
            editGroup.Size = new Size(380, 300);
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

            Label lblEditQueQuan = new Label();
            lblEditQueQuan.Text = "Quê quán:";
            lblEditQueQuan.Location = new Point(10, 180);
            lblEditQueQuan.AutoSize = true;
            editGroup.Controls.Add(lblEditQueQuan);

            txtEditQueQuan = new TextBox();
            txtEditQueQuan.Location = new Point(10, 200);
            txtEditQueQuan.Size = new Size(350, 20);
            editGroup.Controls.Add(txtEditQueQuan);

            Label lblEditSoDienThoai = new Label();
            lblEditSoDienThoai.Text = "Số điện thoại:";
            lblEditSoDienThoai.Location = new Point(10, 230);
            lblEditSoDienThoai.AutoSize = true;
            editGroup.Controls.Add(lblEditSoDienThoai);

            txtEditSoDienThoai = new TextBox();
            txtEditSoDienThoai.Location = new Point(10, 250);
            txtEditSoDienThoai.Size = new Size(350, 20);
            editGroup.Controls.Add(txtEditSoDienThoai);

            btnUpdateAccount = new Button();
            btnUpdateAccount.Text = "Update";
            btnUpdateAccount.Location = new Point(140, 270);
            btnUpdateAccount.Size = new Size(100, 30);
            btnUpdateAccount.BackColor = Color.PowderBlue;
            btnUpdateAccount.ForeColor = Color.FromArgb(44, 62, 80);
            btnUpdateAccount.FlatStyle = FlatStyle.Flat;
            btnUpdateAccount.Click += BtnUpdateAccount_Click;
            editGroup.Controls.Add(btnUpdateAccount);

            btnDeleteAccount = new Button();
            btnDeleteAccount.Text = "Delete";
            btnDeleteAccount.Location = new Point(250, 270);
            btnDeleteAccount.Size = new Size(100, 30);
            btnDeleteAccount.BackColor = Color.PowderBlue;
            btnDeleteAccount.ForeColor = Color.FromArgb(44, 62, 80);
            btnDeleteAccount.FlatStyle = FlatStyle.Flat;
            btnDeleteAccount.Click += BtnDeleteAccount_Click;
            editGroup.Controls.Add(btnDeleteAccount);

            // Tab 3: Course Management
            TabPage courseManagementTab = new TabPage("Course Management");
            courseManagementTab.BackColor = Color.AliceBlue;
            tabControl.TabPages.Add(courseManagementTab);

            // Panel quản lý khóa học
            Panel coursePanel = new Panel();
            coursePanel.Location = new Point(10, 10);
            coursePanel.Size = new Size(800, 400);
            courseManagementTab.Controls.Add(coursePanel);

            // Nhóm tạo khóa học mới
            GroupBox newCourseGroup = new GroupBox();
            newCourseGroup.Text = "Create New Course";
            newCourseGroup.Location = new Point(10, 10);
            newCourseGroup.Size = new Size(380, 300);
            coursePanel.Controls.Add(newCourseGroup);

            // Controls tạo khóa học
            Label lblCourseCode = new Label();
            lblCourseCode.Text = "Course Code:";
            lblCourseCode.Location = new Point(10, 30);
            lblCourseCode.AutoSize = true;
            newCourseGroup.Controls.Add(lblCourseCode);

            txtCourseCode = new TextBox();
            txtCourseCode.Location = new Point(10, 50);
            txtCourseCode.Size = new Size(350, 20);
            newCourseGroup.Controls.Add(txtCourseCode);

            Label lblCourseName = new Label();
            lblCourseName.Text = "Course Name:";
            lblCourseName.Location = new Point(10, 80);
            lblCourseName.AutoSize = true;
            newCourseGroup.Controls.Add(lblCourseName);

            txtCourseName = new TextBox();
            txtCourseName.Location = new Point(10, 100);
            txtCourseName.Size = new Size(350, 20);
            newCourseGroup.Controls.Add(txtCourseName);

            Label lblTeacher = new Label();
            lblTeacher.Text = "Teacher:";
            lblTeacher.Location = new Point(10, 130);
            lblTeacher.AutoSize = true;
            newCourseGroup.Controls.Add(lblTeacher);

            cmbTeachers = new ComboBox();
            cmbTeachers.Location = new Point(10, 150);
            cmbTeachers.Size = new Size(350, 20);
            cmbTeachers.DropDownStyle = ComboBoxStyle.DropDownList;
            newCourseGroup.Controls.Add(cmbTeachers);

            Label lblStartDate = new Label();
            lblStartDate.Text = "Start Date:";
            lblStartDate.Location = new Point(10, 180);
            lblStartDate.AutoSize = true;
            newCourseGroup.Controls.Add(lblStartDate);

            dtpStartDate = new DateTimePicker();
            dtpStartDate.Location = new Point(10, 200);
            dtpStartDate.Size = new Size(350, 20);
            newCourseGroup.Controls.Add(dtpStartDate);

            Label lblEndDate = new Label();
            lblEndDate.Text = "End Date:";
            lblEndDate.Location = new Point(10, 230);
            lblEndDate.AutoSize = true;
            newCourseGroup.Controls.Add(lblEndDate);

            dtpEndDate = new DateTimePicker();
            dtpEndDate.Location = new Point(10, 250);
            dtpEndDate.Size = new Size(350, 20);
            newCourseGroup.Controls.Add(dtpEndDate);

            btnCreateCourse = new Button();
            btnCreateCourse.Text = "Create Course";
            btnCreateCourse.Location = new Point(260, 320);
            btnCreateCourse.Size = new Size(100, 30);
            btnCreateCourse.BackColor = Color.PowderBlue;
            btnCreateCourse.ForeColor = Color.FromArgb(44, 62, 80);
            btnCreateCourse.FlatStyle = FlatStyle.Flat;
            btnCreateCourse.Click += BtnCreateCourse_Click;
            coursePanel.Controls.Add(btnCreateCourse);

            // Danh sách khóa học
            GroupBox courseListGroup = new GroupBox();
            courseListGroup.Text = "Course List";
            courseListGroup.Location = new Point(400, 10);
            courseListGroup.Size = new Size(380, 340);
            coursePanel.Controls.Add(courseListGroup);

            coursesDataGridView = new DataGridView();
            coursesDataGridView.Location = new Point(10, 20);
            coursesDataGridView.Size = new Size(360, 310);
            coursesDataGridView.BackgroundColor = Color.White;
            coursesDataGridView.BorderStyle = BorderStyle.None;
            coursesDataGridView.ReadOnly = true;
            coursesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            coursesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            coursesDataGridView.AllowUserToAddRows = false;
            coursesDataGridView.AllowUserToDeleteRows = false;
            coursesDataGridView.AllowUserToResizeRows = false;
            coursesDataGridView.RowHeadersVisible = false;
            coursesDataGridView.CellClick += CoursesDataGridView_CellClick;
            courseListGroup.Controls.Add(coursesDataGridView);

            // Nút quản lý học sinh
            btnManageStudents = new Button();
            btnManageStudents.Text = "Manage Students";
            btnManageStudents.Location = new Point(540, 360);
            btnManageStudents.Size = new Size(120, 30);
            btnManageStudents.BackColor = Color.PowderBlue;
            btnManageStudents.ForeColor = Color.FromArgb(44, 62, 80);
            btnManageStudents.FlatStyle = FlatStyle.Flat;
            btnManageStudents.Click += BtnManageStudents_Click;
            coursePanel.Controls.Add(btnManageStudents);

            this.Controls.Add(tabControl);
            this.picAvatar = picAvatar;
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

        private void LoadCourses()
        {
            try
            {
                courses = _courseBLL.GetAvailableCourses();
                RefreshCourseList();
                LoadTeachers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading courses: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            accountsDataGridView.Columns.Add("QueQuan", "Quê quán");
            accountsDataGridView.Columns.Add("SoDienThoai", "Số điện thoại");

            // Thêm dữ liệu từ danh sách users
            if (users != null)
            {
                foreach (var user in users)
                {
                    accountsDataGridView.Rows.Add(
                        user.UserID,
                        user.Username,
                        user.Email,
                        user.Role,
                        user.QueQuan,
                        user.SoDienThoai
                    );
                    cmbAccounts.Items.Add($"{user.Username} ({user.Role})");
                }
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
                txtEditQueQuan.Text = user.QueQuan;
                txtEditSoDienThoai.Text = user.SoDienThoai;
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

                if (string.IsNullOrWhiteSpace(txtNewQueQuan.Text))
                {
                    MessageBox.Show("Please enter quê quán", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNewQueQuan.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNewSoDienThoai.Text))
                {
                    MessageBox.Show("Please enter số điện thoại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNewSoDienThoai.Focus();
                    return;
                }

                var newUser = new User
                {
                    Username = txtNewUsername.Text.Trim(),
                    Password = txtNewPassword.Text.Trim(),
                    FullName = txtNewFullName.Text.Trim(),
                    Role = cmbNewRole.SelectedItem.ToString(),
                    Email = $"{txtNewUsername.Text.ToLower().Trim()}@school.com",
                    IsActive = true,
                    QueQuan = txtNewQueQuan.Text.Trim(),
                    SoDienThoai = txtNewSoDienThoai.Text.Trim()
                };

                if (_userBLL.AddUser(newUser))
                {
                    LoadUsers();
                    MessageBox.Show("Account added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear fields
                    txtNewUsername.Text = "";
                    txtNewPassword.Text = "";
                    txtNewFullName.Text = "";
                    txtNewQueQuan.Text = "";
                    txtNewSoDienThoai.Text = "";
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
                    user.QueQuan = txtEditQueQuan.Text;
                    user.SoDienThoai = txtEditSoDienThoai.Text;

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

        private void LoadTeachers()
        {
            cmbTeachers.Items.Clear();
            if (users != null)
            {
                var teachers = users.Where(u => u.Role == "Teacher").ToList();
                foreach (var teacher in teachers)
                {
                    cmbTeachers.Items.Add($"{teacher.FullName} ({teacher.Username})");
                }
            }
        }

        private void RefreshCourseList()
        {
            coursesDataGridView.Rows.Clear();
            coursesDataGridView.Columns.Clear();

            coursesDataGridView.Columns.Add("CourseID", "ID");
            coursesDataGridView.Columns.Add("CourseCode", "Code");
            coursesDataGridView.Columns.Add("CourseName", "Name");
            coursesDataGridView.Columns.Add("TeacherName", "Teacher");
            coursesDataGridView.Columns.Add("StartDate", "Start Date");
            coursesDataGridView.Columns.Add("EndDate", "End Date");
            coursesDataGridView.Columns.Add("MaxEnrollment", "Max Enrollment");

            if (courses != null)
            {
                foreach (var course in courses)
                {
                    var teacherName = "Unknown";
                    if (users != null)
                    {
                        var teacher = users.FirstOrDefault(u => u.UserID == course.TeacherID);
                        if (teacher != null)
                        {
                            teacherName = teacher.FullName;
                        }
                    }
                    coursesDataGridView.Rows.Add(
                        course.CourseID,
                        course.CourseCode,
                        course.CourseName,
                        teacherName,
                        course.StartDate.ToShortDateString(),
                        course.EndDate.ToShortDateString(),
                        course.MaxEnrollment
                    );
                }
            }
        }

        private void BtnCreateCourse_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCourseCode.Text))
                {
                    MessageBox.Show("Please enter course code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCourseCode.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCourseName.Text))
                {
                    MessageBox.Show("Please enter course name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCourseName.Focus();
                    return;
                }

                if (cmbTeachers.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a teacher", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbTeachers.Focus();
                    return;
                }

                if (dtpStartDate.Value >= dtpEndDate.Value)
                {
                    MessageBox.Show("End date must be after start date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var selectedTeacher = users.FirstOrDefault(u => 
                    u.Role == "Teacher" && 
                    cmbTeachers.SelectedItem.ToString().Contains(u.Username));

                if (selectedTeacher == null)
                {
                    MessageBox.Show("Invalid teacher selection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var newCourse = new Course
                {
                    CourseCode = txtCourseCode.Text.Trim(),
                    CourseName = txtCourseName.Text.Trim(),
                    TeacherID = selectedTeacher.UserID,
                    StartDate = dtpStartDate.Value,
                    EndDate = dtpEndDate.Value
                };

                if (_courseBLL.AddCourse(newCourse))
                {
                    LoadCourses();
                    MessageBox.Show("Course created successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear fields
                    txtCourseCode.Text = "";
                    txtCourseName.Text = "";
                    cmbTeachers.SelectedIndex = -1;
                    dtpStartDate.Value = DateTime.Now;
                    dtpEndDate.Value = DateTime.Now.AddMonths(3);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating course: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CoursesDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && coursesDataGridView.Rows[e.RowIndex].Cells[0].Value != null)
            {
                selectedCourseId = (int)coursesDataGridView.Rows[e.RowIndex].Cells[0].Value;
            }
        }

        private void BtnManageStudents_Click(object sender, EventArgs e)
        {
            if (selectedCourseId == -1)
            {
                MessageBox.Show("Please select a course first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var enrolledStudentsForm = new EnrolledStudentsForm(selectedCourseId, _courseBLL, _userBLL);
            enrolledStudentsForm.ShowDialog();
        }

        private void LoadAdminInfo()
        {
            var admin = _userContext.CurrentUser;
            if (admin != null && admin.Role == "Admin")
            {
                lblFullNameValue.Text = admin.FullName;
                lblEmailValue.Text = admin.Email;
                lblPhoneValue.Text = admin.SoDienThoai;
                lblHometownValue.Text = admin.QueQuan;
                lblRoleValue.Text = "Admin";
                string avatarPath = admin.AvatarPath;
                if (!string.IsNullOrEmpty(admin.AvatarPath) && File.Exists(admin.AvatarPath))
                {
                    try
                    {
                        // Dispose ảnh cũ trước khi load ảnh mới
                        if (picAvatar.Image != null)
                        {
                            picAvatar.Image.Dispose();
                            picAvatar.Image = null;
                        }
                        picAvatar.Image = new Bitmap(admin.AvatarPath);
                    }
                    catch
                    {
                        // Nếu lỗi, không load ảnh
                    }
                }
            }
            else
            {
                lblFullNameValue.Text = "N/A";
                lblEmailValue.Text = "N/A";
                lblPhoneValue.Text = "N/A";
                lblHometownValue.Text = "N/A";
                lblRoleValue.Text = "Admin";
            }
        }

        private void BtnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                var admin = _userContext.CurrentUser;
                if (admin == null)
                {
                    MessageBox.Show("User information not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy mật khẩu cũ và mới từ TextBox
                string oldPassword = txtOldPassword?.Text?.Trim() ?? string.Empty;
                string newPassword = txtChangePassword?.Text?.Trim() ?? string.Empty;

                if (string.IsNullOrEmpty(oldPassword))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu cũ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtOldPassword.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(newPassword))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu mới", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtChangePassword.Focus();
                    return;
                }

                // Kiểm tra mật khẩu cũ
                if (admin.PasswordHash != oldPassword)
                {
                    MessageBox.Show("Mật khẩu cũ không đúng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtOldPassword.Clear();
                    txtOldPassword.Focus();
                    return;
                }

                // Cập nhật mật khẩu mới vào PasswordHash
                admin.PasswordHash = newPassword;

                if (_userBLL.ChangeUserPassword(_userContext.CurrentUser.Username, oldPassword, newPassword))
                {
                    MessageBox.Show("Đổi mật khẩu thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOldPassword.Clear();
                    txtChangePassword.Clear();
                }
                else
                {
                    MessageBox.Show("Không thể đổi mật khẩu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đổi mật khẩu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnChangeAvatar_Click(object sender, EventArgs e)
        {
            try
            {
                var admin = _userContext.CurrentUser;
                if (admin == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Chọn ảnh đại diện";
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                    openFileDialog.Multiselect = false;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string selectedFile = openFileDialog.FileName;
                        FileInfo fileInfo = new FileInfo(selectedFile);
                        if (fileInfo.Length > 5 * 1024 * 1024) // 5MB
                        {
                            MessageBox.Show("Kích thước ảnh không được vượt quá 5MB", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        try
                        {
                            using (Image img = Image.FromFile(selectedFile))
                            {
                                // Nếu load được thì ảnh hợp lệ
                            }
                        }
                        catch
                        {
                            MessageBox.Show("File ảnh không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Dispose ảnh cũ trước khi thao tác file
                        if (picAvatar.Image != null)
                        {
                            picAvatar.Image.Dispose();
                            picAvatar.Image = null;
                        }

                        // Cập nhật avatar
                        bool success = _userBLL.ChangeUserAvatar(admin.Username, selectedFile);
                        if (success)
                        {
                            picAvatar.Image = Image.FromFile(selectedFile);
                            MessageBox.Show("Cập nhật ảnh đại diện thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không thể cập nhật ảnh đại diện. Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật ảnh đại diện: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Các controls
        private Label dateLabel;
        private DataGridView accountsDataGridView;
        private TextBox txtNewUsername;
        private TextBox txtNewFullName;
        private TextBox txtNewPassword;
        private TextBox txtChangePassword;
        private ComboBox cmbNewRole;
        private Button btnAddAccount;
        private ComboBox cmbAccounts;
        private TextBox txtEditUsername;
        private ComboBox cmbEditRole;
        private Button btnUpdateAccount;
        private Button btnDeleteAccount;
        private int selectedAccountId = -1;
        private TextBox txtCourseCode;
        private TextBox txtCourseName;
        private ComboBox cmbTeachers;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private Button btnCreateCourse;
        private DataGridView coursesDataGridView;
        private Button btnManageStudents;
        private int selectedCourseId = -1;
        private TextBox txtNewQueQuan;
        private TextBox txtNewSoDienThoai;
        private TextBox txtEditQueQuan;
        private TextBox txtEditSoDienThoai;
        private TabControl tabControl;
        private TextBox txtOldPassword;
    }
}