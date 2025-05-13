namespace CNPM.Forms.Student
{
    partial class frmTongQuan
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTongQuan));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnDangKy = new FontAwesome.Sharp.IconButton();
            this.btnDiem = new FontAwesome.Sharp.IconButton();
            this.btnDanhSach = new FontAwesome.Sharp.IconButton();
            this.btnBaiTap = new FontAwesome.Sharp.IconButton();
            this.btnTongQuan = new FontAwesome.Sharp.IconButton();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.lblChao = new System.Windows.Forms.Label();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.MidnightBlue;
            this.panelMenu.Controls.Add(this.btnLogOut);
            this.panelMenu.Controls.Add(this.btnDangKy);
            this.panelMenu.Controls.Add(this.btnDiem);
            this.panelMenu.Controls.Add(this.btnDanhSach);
            this.panelMenu.Controls.Add(this.btnBaiTap);
            this.panelMenu.Controls.Add(this.btnTongQuan);
            this.panelMenu.Controls.Add(this.lblTitle);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(241, 600);
            this.panelMenu.TabIndex = 1;
            // 
            // btnDangKy
            // 
            this.btnDangKy.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDangKy.FlatAppearance.BorderSize = 0;
            this.btnDangKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangKy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDangKy.ForeColor = System.Drawing.Color.White;
            this.btnDangKy.IconChar = FontAwesome.Sharp.IconChar.Edit;
            this.btnDangKy.IconColor = System.Drawing.Color.White;
            this.btnDangKy.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDangKy.IconSize = 30;
            this.btnDangKy.Location = new System.Drawing.Point(0, 455);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(241, 87);
            this.btnDangKy.TabIndex = 1;
            this.btnDangKy.Text = "Đăng ký học phần";
            this.btnDangKy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // btnDiem
            // 
            this.btnDiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDiem.FlatAppearance.BorderSize = 0;
            this.btnDiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDiem.ForeColor = System.Drawing.Color.White;
            this.btnDiem.IconChar = FontAwesome.Sharp.IconChar.BarsStaggered;
            this.btnDiem.IconColor = System.Drawing.Color.White;
            this.btnDiem.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDiem.IconSize = 30;
            this.btnDiem.Location = new System.Drawing.Point(0, 371);
            this.btnDiem.Name = "btnDiem";
            this.btnDiem.Size = new System.Drawing.Size(241, 84);
            this.btnDiem.TabIndex = 2;
            this.btnDiem.Text = "Xem điểm";
            this.btnDiem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDiem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // btnDanhSach
            // 
            this.btnDanhSach.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDanhSach.FlatAppearance.BorderSize = 0;
            this.btnDanhSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDanhSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnDanhSach.ForeColor = System.Drawing.Color.White;
            this.btnDanhSach.IconChar = FontAwesome.Sharp.IconChar.ClipboardList;
            this.btnDanhSach.IconColor = System.Drawing.Color.White;
            this.btnDanhSach.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDanhSach.IconSize = 30;
            this.btnDanhSach.Location = new System.Drawing.Point(0, 283);
            this.btnDanhSach.Name = "btnDanhSach";
            this.btnDanhSach.Size = new System.Drawing.Size(241, 88);
            this.btnDanhSach.TabIndex = 3;
            this.btnDanhSach.Text = "Danh sách khóa học";
            this.btnDanhSach.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // btnBaiTap
            // 
            this.btnBaiTap.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBaiTap.FlatAppearance.BorderSize = 0;
            this.btnBaiTap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaiTap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnBaiTap.ForeColor = System.Drawing.Color.White;
            this.btnBaiTap.IconChar = FontAwesome.Sharp.IconChar.Book;
            this.btnBaiTap.IconColor = System.Drawing.Color.White;
            this.btnBaiTap.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBaiTap.IconSize = 30;
            this.btnBaiTap.Location = new System.Drawing.Point(0, 195);
            this.btnBaiTap.Name = "btnBaiTap";
            this.btnBaiTap.Size = new System.Drawing.Size(241, 88);
            this.btnBaiTap.TabIndex = 4;
            this.btnBaiTap.Text = "Bài tập";
            this.btnBaiTap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // btnTongQuan
            // 
            this.btnTongQuan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTongQuan.FlatAppearance.BorderSize = 0;
            this.btnTongQuan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTongQuan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnTongQuan.ForeColor = System.Drawing.Color.White;
            this.btnTongQuan.IconChar = FontAwesome.Sharp.IconChar.User;
            this.btnTongQuan.IconColor = System.Drawing.Color.White;
            this.btnTongQuan.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnTongQuan.IconSize = 30;
            this.btnTongQuan.Location = new System.Drawing.Point(0, 110);
            this.btnTongQuan.Name = "btnTongQuan";
            this.btnTongQuan.Size = new System.Drawing.Size(241, 85);
            this.btnTongQuan.TabIndex = 5;
            this.btnTongQuan.Text = "Thông tin người dùng";
            this.btnTongQuan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.White;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Image = ((System.Drawing.Image)(resources.GetObject("lblTitle.Image")));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(241, 110);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.Color.DarkGray;
            this.btnLogOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnLogOut.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnLogOut.Location = new System.Drawing.Point(6, 553);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(226, 35);
            this.btnLogOut.TabIndex = 2;
            this.btnLogOut.Text = "Đăng xuất";
            this.btnLogOut.UseVisualStyleBackColor = false;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // lblChao
            // 
            this.lblChao.AutoSize = true;
            this.lblChao.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblChao.Location = new System.Drawing.Point(509, 9);
            this.lblChao.Name = "lblChao";
            this.lblChao.Size = new System.Drawing.Size(81, 29);
            this.lblChao.TabIndex = 3;
            this.lblChao.Text = "label1";
            // 
            // frmTongQuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.lblChao);
            this.Controls.Add(this.panelMenu);
            this.Name = "frmTongQuan";
            this.Text = "frmTongQuan";
            this.panelMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Label lblTitle;
        private FontAwesome.Sharp.IconButton btnDangKy;
        private FontAwesome.Sharp.IconButton btnDiem;
        private FontAwesome.Sharp.IconButton btnDanhSach;
        private FontAwesome.Sharp.IconButton btnBaiTap;
        private FontAwesome.Sharp.IconButton btnTongQuan;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Label lblChao;
    }
}