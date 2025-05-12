using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

public class AdminForm : Form
{
    private DataGridView dgvUsers;
    private Button btnAdd;
    private Button btnEdit;
    private Button btnDelete;

    public AdminForm()
    {
        InitializeComponents();
        LoadUsers();
    }

    private void InitializeComponents()
    {
        this.Text = "Admin Management";
        this.Size = new System.Drawing.Size(800, 600);

        dgvUsers = new DataGridView();
        dgvUsers.Dock = DockStyle.Fill;
        dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvUsers.MultiSelect = false;
        dgvUsers.AllowUserToAddRows = false;

        btnAdd = new Button();
        btnAdd.Text = "Add User";
        btnAdd.Click += BtnAdd_Click;

        btnEdit = new Button();
        btnEdit.Text = "Edit User";
        btnEdit.Click += BtnEdit_Click;

        btnDelete = new Button();
        btnDelete.Text = "Delete User";
        btnDelete.Click += BtnDelete_Click;

        FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
        buttonPanel.Dock = DockStyle.Top;
        buttonPanel.Height = 40;
        buttonPanel.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete });

        this.Controls.Add(dgvUsers);
        this.Controls.Add(buttonPanel);
    }

    private void LoadUsers()
    {
        using (SqlConnection conn = DataHelper.GetConnection())
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM Users";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvUsers.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message);
            }
        }
    }

    private void BtnAdd_Click(object sender, EventArgs e)
    {
        // Implement add user logic
        UserForm userForm = new UserForm();
        if (userForm.ShowDialog() == DialogResult.OK)
        {
            LoadUsers();
        }
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        if (dgvUsers.SelectedRows.Count > 0)
        {
            DataGridViewRow row = dgvUsers.SelectedRows[0];
            User user = new User
            {
                Id = Convert.ToInt32(row.Cells["Id"].Value),
                Username = row.Cells["Username"].Value.ToString(),
                FullName = row.Cells["FullName"].Value.ToString(),
                Email = row.Cells["Email"].Value.ToString(),
                Role = row.Cells["Role"].Value.ToString(),
                IsActive = Convert.ToBoolean(row.Cells["IsActive"].Value)
            };

            UserForm userForm = new UserForm(user);
            if (userForm.ShowDialog() == DialogResult.OK)
            {
                LoadUsers();
            }
        }
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        if (dgvUsers.SelectedRows.Count > 0)
        {
            if (MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int userId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["Id"].Value);
                DeleteUser(userId);
                LoadUsers();
            }
        }
    }

    private bool DeleteUser(int userId)
    {
        using (SqlConnection conn = DataHelper.GetConnection())
        {
            conn.Open();
            using (SqlTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    string query = "DELETE FROM Users WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, conn, transaction);
                    cmd.Parameters.AddWithValue("@Id", userId);
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