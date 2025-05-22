using System;
using System.Windows.Forms;
using System.Drawing;

namespace CNPM.Forms.Teacher
{
    partial class UcProfile
    {
        private PictureBox picAvatar;
        private Button btnChooseAvatar;
        private TextBox txtName, txtEmail, txtPhone, txtAddress, txtRole, txtOldPassword, txtNewPassword;
        private Button btnChangePassword;
        private Label lblHeader;

        private void InitializeComponent()
        {
            // Avatar
            picAvatar = new PictureBox
            {
                Size = new Size(120, 120),
                Location = new Point(30, 30),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };

            btnChooseAvatar = new Button
            {
                Text = "Chọn ảnh",
                Location = new Point(30, 160),
                Width = 120
            };
            btnChooseAvatar.Click += BtnChooseAvatar_Click;

            // Label Header
            lblHeader = new Label
            {
                Text = "Thông tin cá nhân",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Location = new Point(180, 20),
                AutoSize = true,
                ForeColor = Color.FromArgb(30, 30, 60)
            };

            // TextBoxes
            txtName = CreateTextBox(70);
            txtEmail = CreateTextBox(110);
            txtPhone = CreateTextBox(150);
            txtAddress = CreateTextBox(190);
            txtRole = CreateTextBox(230);
            txtOldPassword = CreateTextBox(300, password: true);
            txtNewPassword = CreateTextBox(340, password: true);

            // Labels
            this.Controls.Add(CreateLabel("Họ và tên:", 70));
            this.Controls.Add(CreateLabel("Email:", 110));
            this.Controls.Add(CreateLabel("Số điện thoại:", 150));
            this.Controls.Add(CreateLabel("Quê quán:", 190));
            this.Controls.Add(CreateLabel("Chức vụ:", 230));
            this.Controls.Add(CreateLabel("Mật khẩu cũ:", 300));
            this.Controls.Add(CreateLabel("Mật khẩu mới:", 340));

            // Button đổi mật khẩu
            btnChangePassword = new Button
            {
                Text = "Đổi mật khẩu",
                Location = new Point(180, 380),
                Width = 150
            };
            btnChangePassword.Click += new EventHandler(this.btnChangePassword_Click);

            // Thêm Controls
            this.Controls.Add(picAvatar);
            this.Controls.Add(btnChooseAvatar);
            this.Controls.Add(lblHeader);
            this.Controls.Add(txtName);
            this.Controls.Add(txtEmail);
            this.Controls.Add(txtPhone);
            this.Controls.Add(txtAddress);
            this.Controls.Add(txtRole);
            this.Controls.Add(txtOldPassword);
            this.Controls.Add(txtNewPassword);
            this.Controls.Add(btnChangePassword);

            this.Size = new Size(600, 440);
            this.BackColor = Color.WhiteSmoke;
        }

        private Label CreateLabel(string text, int top)
        {
            return new Label
            {
                Text = text,
                Location = new Point(180, top),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F)
            };
        }

        private TextBox CreateTextBox(int top, bool password = false)
        {
            return new TextBox
            {
                Location = new Point(300, top),
                Width = 250,
                ReadOnly = !password,
                PasswordChar = password ? '*' : '\0',
                Font = new Font("Segoe UI", 10F)
            };
        }
    }
}
