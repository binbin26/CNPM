using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

public class UserForm : Form
{
    private TextBox txtUsername;
    private TextBox txtPassword;
    private TextBox txtFullName;
    private TextBox txtEmail;
    private ComboBox cmbRole;
    private CheckBox chkIsActive;
    private Button btnSave;
    private Button btnCancel;
    private User currentUser;

    public UserForm(User user = null)
    {
        currentUser = user;
        InitializeComponents();
        if (user != null)
        {
            LoadUserData();
        }
    }

    private void InitializeComponents()
    {
        this.Text = currentUser == null ? "Add New User" : "Edit User";
        this.Size = new System.Drawing.Size(400, 400);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.StartPosition = FormStartPosition.CenterParent;

        TableLayoutPanel panel = new TableLayoutPanel();
        panel.Dock = DockStyle.Fill;
        panel.Padding = new Padding(10);
        panel.RowCount = 7;
        panel.ColumnCount = 2;

        // Username
        panel.Controls.Add(new Label { Text = "Username:" }, 0, 0);
        txtUsername = new TextBox { Width = 200 };
        panel.Controls.Add(txtUsername, 1, 0);

        // Password
        panel.Controls.Add(new Label { Text = "Password:" }, 0, 1);
        txtPassword = new TextBox { Width = 200, UseSystemPasswordChar = true };
        panel.Controls.Add(txtPassword, 1, 1);

        // Full Name
        panel.Controls.Add(new Label { Text = "Full Name:" }, 0, 2);
        txtFullName = new TextBox { Width = 200 };
        panel.Controls.Add(txtFullName, 1, 2);

        // Email
        panel.Controls.Add(new Label { Text = "Email:" }, 0, 3);
        txtEmail = new TextBox { Width = 200 };
        panel.Controls.Add(txtEmail, 1, 3);

        // Role
        panel.Controls.Add(new Label { Text = "Role:" }, 0, 4);
        cmbRole = new ComboBox { Width = 200 };
        cmbRole.Items.AddRange(new string[] { "Admin", "User" });
        panel.Controls.Add(cmbRole, 1, 4);

        // Is Active
        panel.Controls.Add(new Label { Text = "Is Active:" }, 0, 5);
        chkIsActive = new CheckBox();
        panel.Controls.Add(chkIsActive, 1, 5);

        // Buttons
        FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
        buttonPanel.FlowDirection = FlowDirection.RightToLeft;
        buttonPanel.Dock = DockStyle.Bottom;
        buttonPanel.Height = 40;

        btnSave = new Button { Text = "Save" };
        btnSave.Click += BtnSave_Click;

        btnCancel = new Button { Text = "Cancel" };
        btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

        buttonPanel.Controls.Add(btnCancel);
        buttonPanel.Controls.Add(btnSave);

        this.Controls.Add(panel);
        this.Controls.Add(buttonPanel);
    }

    private void LoadUserData()
    {
        txtUsername.Text = currentUser.Username;
        txtFullName.Text = currentUser.FullName;
        txtEmail.Text = currentUser.Email;
        cmbRole.Text = currentUser.Role;
        chkIsActive.Checked = currentUser.IsActive;
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (ValidateInput())
        {
            User user = new User
            {
                Username = txtUsername.Text,
                FullName = txtFullName.Text,
                Email = txtEmail.Text,
                Role = cmbRole.Text,
                IsActive = chkIsActive.Checked
            };

            if (currentUser != null)
            {
                user.Id = currentUser.Id;
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    user.PasswordHash = HashPassword(txtPassword.Text);
                }
                else
                {
                    user.PasswordHash = currentUser.PasswordHash;
                }
                UpdateUser(user);
            }
            else
            {
                user.PasswordHash = HashPassword(txtPassword.Text);
                AddUser(user);
            }

            this.DialogResult = DialogResult.OK;
        }
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
            (currentUser == null && string.IsNullOrWhiteSpace(txtPassword.Text)) ||
            string.IsNullOrWhiteSpace(txtFullName.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text) ||
            string.IsNullOrWhiteSpace(cmbRole.Text))
        {
            MessageBox.Show("Please fill in all required fields.");
            return false;
        }
        return true;
    }

    private string HashPassword(string password)
    {
        // Implement your password hashing logic here
        return password; // This is just a placeholder
    }

    private bool AddUser(User user)
    {
        using (SqlConnection conn = DataHelper.GetConnection())
        {
            conn.Open();
            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    string query = @"
                    INSERT INTO Users (Username, PasswordHash, Role, FullName, Email, CreatedAt, IsActive) 
                    VALUES (@Username, @PasswordHash, @Role, @FullName, @Email, @CreatedAt, @IsActive)";

                    SqlCommand cmd = new SqlCommand(query, conn, transaction);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@Role", user.Role);
                    cmd.Parameters.AddWithValue("@FullName", user.FullName);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@IsActive", user.IsActive);

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }

    private bool UpdateUser(User user)
    {
        using (SqlConnection conn = DataHelper.GetConnection())
        {
            conn.Open();
            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    string query = @"
                    UPDATE Users 
                    SET Username = @Username, 
                        PasswordHash = @PasswordHash,
                        Role = @Role, 
                        FullName = @FullName, 
                        Email = @Email, 
                        IsActive = @IsActive 
                    WHERE Id = @Id";

                    SqlCommand cmd = new SqlCommand(query, conn, transaction);
                    cmd.Parameters.AddWithValue("@Id", user.Id);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@Role", user.Role);
                    cmd.Parameters.AddWithValue("@FullName", user.FullName);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@IsActive", user.IsActive);

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
} 