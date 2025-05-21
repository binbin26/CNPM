using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CNPM.DAL;

namespace CNPM.Forms.Teacher
{
    public partial class UcProfile : UserControl
    {
        private int UserID;

        public UcProfile(int userId)
        {
            InitializeComponent();
            UserID = userId;
            LoadProfile();
        }

        private void LoadProfile()
        {
            string query = "SELECT FullName, Email, SoDienThoai, QueQuan, Role, AvatarPath FROM Users WHERE UserID = @ID";
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ID", UserID);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtName.Text = reader.IsDBNull(0) ? "" : reader.GetString(0);
                        txtEmail.Text = reader.IsDBNull(1) ? "" : reader.GetString(1);
                        txtPhone.Text = reader.IsDBNull(2) ? "" : reader.GetString(2);
                        txtAddress.Text = reader.IsDBNull(3) ? "" : reader.GetString(3);
                        txtRole.Text = reader.IsDBNull(4) ? "" : reader.GetString(4);
                        string avatarPath = reader.IsDBNull(5) ? null : reader.GetString(5);
                        if (!string.IsNullOrEmpty(avatarPath))
                        {
                            string fullPath = Path.Combine(Application.StartupPath, avatarPath.Replace("/", "\\"));
                            if (File.Exists(fullPath))
                                picAvatar.Image = Image.FromFile(fullPath);
                        }
                    }
                }
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            string oldPassword = txtOldPassword.Text;
            string newPassword = txtNewPassword.Text;

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Mật khẩu mới không được để trống.");
                return;
            }

            string checkQuery = "SELECT PasswordHash FROM Users WHERE UserID = @ID";
            string updateQuery = "UPDATE Users SET PasswordHash = @NewPassword WHERE UserID = @ID";

            using (var conn = DatabaseHelper.GetConnection())
            using (var checkCmd = new SqlCommand(checkQuery, conn))
            {
                checkCmd.Parameters.AddWithValue("@ID", UserID);
                conn.Open();
                var storedPassword = checkCmd.ExecuteScalar()?.ToString();
                if (storedPassword != oldPassword)
                {
                    MessageBox.Show("Mật khẩu cũ không chính xác.");
                    return;
                }

                using (var updateCmd = new SqlCommand(updateQuery, conn))
                {
                    updateCmd.Parameters.AddWithValue("@NewPassword", newPassword);
                    updateCmd.Parameters.AddWithValue("@ID", UserID);
                    updateCmd.ExecuteNonQuery();
                    MessageBox.Show("Đổi mật khẩu thành công.");
                }
            }
        }

        private void BtnChooseAvatar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Ảnh (*.jpg;*.png;*.jpeg)|*.jpg;*.png;*.jpeg";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string sourcePath = dialog.FileName;
                string fileName = Path.GetFileName(sourcePath);
                string avatarDir = Path.Combine(Application.StartupPath, "Avatars");
                if (!Directory.Exists(avatarDir)) Directory.CreateDirectory(avatarDir);

                string destPath = Path.Combine(avatarDir, fileName);
                File.Copy(sourcePath, destPath, true);
                picAvatar.Image = Image.FromFile(destPath);

                string relativePath = $"Avatars/{fileName}";
                using (var conn = DatabaseHelper.GetConnection())
                using (var cmd = new SqlCommand("UPDATE Users SET AvatarPath = @Path WHERE UserID = @ID", conn))
                {
                    cmd.Parameters.AddWithValue("@Path", relativePath);
                    cmd.Parameters.AddWithValue("@ID", UserID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Cập nhật ảnh đại diện thành công.");
            }
        }
    }
}
