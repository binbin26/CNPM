namespace CNPM.Forms.Auth
{
    partial class RegistrationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrationForm));
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnSignUp = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblFullname = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(212, 172);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(420, 22);
            this.txtUsername.TabIndex = 0;
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(212, 269);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(420, 22);
            this.txtFullName.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(212, 221);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(420, 22);
            this.txtPassword.TabIndex = 2;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(212, 313);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(420, 22);
            this.txtEmail.TabIndex = 3;
            // 
            // btnSignUp
            // 
            this.btnSignUp.Location = new System.Drawing.Point(650, 392);
            this.btnSignUp.Name = "btnSignUp";
            this.btnSignUp.Size = new System.Drawing.Size(138, 51);
            this.btnSignUp.TabIndex = 4;
            this.btnSignUp.Text = "Đăng ký";
            this.btnSignUp.UseVisualStyleBackColor = true;
            this.btnSignUp.Click += new System.EventHandler(this.btnSignUp_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(228, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(440, 162);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblUsername.Location = new System.Drawing.Point(90, 174);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(86, 20);
            this.lblUsername.TabIndex = 6;
            this.lblUsername.Text = "Username";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblPassword.Location = new System.Drawing.Point(90, 221);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(83, 20);
            this.lblPassword.TabIndex = 7;
            this.lblPassword.Text = "Password";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblEmail.Location = new System.Drawing.Point(90, 315);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(51, 20);
            this.lblEmail.TabIndex = 8;
            this.lblEmail.Text = "Email";
            // 
            // lblFullname
            // 
            this.lblFullname.AutoSize = true;
            this.lblFullname.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblFullname.Location = new System.Drawing.Point(90, 271);
            this.lblFullname.Name = "lblFullname";
            this.lblFullname.Size = new System.Drawing.Size(77, 20);
            this.lblFullname.TabIndex = 9;
            this.lblFullname.Text = "Fullname";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnCancel.Location = new System.Drawing.Point(2, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(91, 31);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "<Back";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblPhone.Location = new System.Drawing.Point(90, 362);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(106, 20);
            this.lblPhone.TabIndex = 11;
            this.lblPhone.Text = "Số điện thoại";
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblCity.Location = new System.Drawing.Point(90, 406);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(81, 20);
            this.lblCity.TabIndex = 12;
            this.lblCity.Text = "Quê quán";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(212, 362);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(420, 22);
            this.txtPhone.TabIndex = 13;
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(212, 406);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(420, 22);
            this.txtCity.TabIndex = 14;
            // 
            // RegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblCity);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblFullname);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnSignUp);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.txtUsername);
            this.Name = "RegistrationForm";
            this.Text = "RegistrationForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnSignUp;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblFullname;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtCity;
    }
}