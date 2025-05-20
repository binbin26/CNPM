using System;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class UcProfile
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtName, txtEmail, txtPhone, txtAddress, txtRole, txtOldPassword, txtNewPassword;
        private Button btnChangePassword;

        private void InitializeComponent()
        {
            txtName = new TextBox { ReadOnly = true, Top = 20, Left = 150, Width = 300 };
            txtEmail = new TextBox { ReadOnly = true, Top = 60, Left = 150, Width = 300 };
            txtPhone = new TextBox { ReadOnly = true, Top = 100, Left = 150, Width = 300 };
            txtAddress = new TextBox { ReadOnly = true, Top = 140, Left = 150, Width = 300 };
            txtRole = new TextBox { ReadOnly = true, Top = 180, Left = 150, Width = 300 };
            txtOldPassword = new TextBox { Top = 220, Left = 150, Width = 300, PasswordChar = '*' };
            txtNewPassword = new TextBox { Top = 260, Left = 150, Width = 300, PasswordChar = '*' };
            btnChangePassword = new Button { Text = "Đổi mật khẩu", Top = 300, Left = 150 };

            btnChangePassword.Click += new EventHandler(this.btnChangePassword_Click);

            this.Controls.Add(new Label { Text = "Họ và tên:", Top = 20, Left = 30 });
            this.Controls.Add(txtName);
            this.Controls.Add(new Label { Text = "Email:", Top = 60, Left = 30 });
            this.Controls.Add(txtEmail);
            this.Controls.Add(new Label { Text = "Số điện thoại:", Top = 100, Left = 30 });
            this.Controls.Add(txtPhone);
            this.Controls.Add(new Label { Text = "Quê quán:", Top = 140, Left = 30 });
            this.Controls.Add(txtAddress);
            this.Controls.Add(new Label { Text = "Chức vụ:", Top = 180, Left = 30 });
            this.Controls.Add(txtRole);
            this.Controls.Add(new Label { Text = "Mật khẩu cũ:", Top = 220, Left = 30 });
            this.Controls.Add(txtOldPassword);
            this.Controls.Add(new Label { Text = "Mật khẩu mới:", Top = 260, Left = 30 });
            this.Controls.Add(txtNewPassword);
            this.Controls.Add(btnChangePassword);

            this.Size = new System.Drawing.Size(500, 360);
        }
    }
}