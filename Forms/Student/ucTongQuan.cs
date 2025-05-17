using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using CNPM.BLL;
using System.IO;
using System.Drawing;
using CNPM.DAL;


namespace CNPM.Forms.Student
{
    public partial class ucTongQuan : UserControl
    {

        private string _username;
        UserBLL userBLL = new UserBLL();

        public ucTongQuan(string username)
        {
            _username = username;
            InitializeComponent();
            InitializeCustomComponents();
            LoadStudentInfo(_username);
        }

        private void InitializeCustomComponents()
        {
            
        }

        private void LoadStudentInfo(string username)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Users WHERE Username = @Username";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", _username);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblFullName.Text = reader["FullName"].ToString();
                                lblEmail.Text = reader["Email"].ToString();
                                lblRole.Text = reader["Role"].ToString();
                                lblCity.Text = reader["QueQuan"].ToString();
                                lblPhone.Text = reader["SoDienThoai"].ToString();
                            }
                            if (reader["AvatarPath"] != DBNull.Value)
                            {
                                string avatarPath = reader["AvatarPath"].ToString();
                                if (File.Exists(avatarPath))
                                {
                                    AvatarPict.Image = new Bitmap(avatarPath);
                                }
                                else
                                {
                                    AvatarPict.Image = new Bitmap(@"C:\Users\baong\OneDrive\Desktop\CNPM\Resources\Avatar\defaultAvatar.png");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin học sinh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPass_Click(object sender, EventArgs e)
        {
            string oldPass = txtOP.Text.Trim();
            string newPass = txtNP.Text.Trim();

            if (string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool success = userBLL.ChangeUserPassword(_username, oldPass, newPass);

            if (success)
            {
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOP.Clear();
                txtNP.Clear();
            }
            else
            {
                MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AvatarPict_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn ảnh đại diện";
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = openFileDialog.FileName;
                string fileExtension = Path.GetExtension(selectedFile);
                string newFileName = _username + fileExtension;
                string saveDirectory = @"C:\Users\baong\OneDrive\Desktop\CNPM\Resources\Avatar\Student";
                string savePath = Path.Combine(saveDirectory, newFileName);

                try
                {
                    Directory.CreateDirectory(saveDirectory); 
                    if (AvatarPict.Image != null)
                    {
                        AvatarPict.Image.Dispose();
                        AvatarPict.Image = null;
                    }
                    File.Copy(selectedFile, savePath, true);
                    bool updated = userBLL.ChangeUserAvatar(_username, savePath);

                    if (updated)
                    {
                        if (AvatarPict.Image != null)
                        {
                            AvatarPict.Image.Dispose();
                            AvatarPict.Image = null;
                        }
                        AvatarPict.Image = new Bitmap(savePath);
                        MessageBox.Show("Ảnh đại diện đã được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else
                    {
                        MessageBox.Show("Cập nhật không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật ảnh đại diện: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
