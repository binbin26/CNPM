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
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnThoat = new FontAwesome.Sharp.IconButton();
            this.btnDangKy = new FontAwesome.Sharp.IconButton();
            this.btnDiem = new FontAwesome.Sharp.IconButton();
            this.btnDanhSach = new FontAwesome.Sharp.IconButton();
            this.btnBaiTap = new FontAwesome.Sharp.IconButton();
            this.btnTongQuan = new FontAwesome.Sharp.IconButton();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelTopRight = new System.Windows.Forms.Panel();
            this.lblTenNguoiDung = new System.Windows.Forms.Label();
            this.iconBell = new FontAwesome.Sharp.IconButton();
            this.panelMenu.SuspendLayout();
            this.panelTopRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.MidnightBlue;
            this.panelMenu.Controls.Add(this.btnThoat);
            this.panelMenu.Controls.Add(this.btnDangKy);
            this.panelMenu.Controls.Add(this.btnDiem);
            this.panelMenu.Controls.Add(this.btnDanhSach);
            this.panelMenu.Controls.Add(this.btnBaiTap);
            this.panelMenu.Controls.Add(this.btnTongQuan);
            this.panelMenu.Controls.Add(this.lblTitle);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(220, 600);
            this.panelMenu.TabIndex = 1;
            // 
            // btnThoat
            // 
            this.btnThoat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnThoat.FlatAppearance.BorderSize = 0;
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.IconChar = FontAwesome.Sharp.IconChar.Globe;
            this.btnThoat.IconColor = System.Drawing.Color.White;
            this.btnThoat.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnThoat.IconSize = 30;
            this.btnThoat.Location = new System.Drawing.Point(0, 550);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(220, 50);
            this.btnThoat.TabIndex = 0;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // btnDangKy
            // 
            this.btnDangKy.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDangKy.FlatAppearance.BorderSize = 0;
            this.btnDangKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangKy.ForeColor = System.Drawing.Color.White;
            this.btnDangKy.IconChar = FontAwesome.Sharp.IconChar.Edit;
            this.btnDangKy.IconColor = System.Drawing.Color.White;
            this.btnDangKy.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDangKy.IconSize = 30;
            this.btnDangKy.Location = new System.Drawing.Point(0, 260);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(220, 50);
            this.btnDangKy.TabIndex = 1;
            this.btnDangKy.Text = "Đăng ký học phần";
            this.btnDangKy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // btnDiem
            // 
            this.btnDiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDiem.FlatAppearance.BorderSize = 0;
            this.btnDiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDiem.ForeColor = System.Drawing.Color.White;
            this.btnDiem.IconChar = FontAwesome.Sharp.IconChar.BarsStaggered;
            this.btnDiem.IconColor = System.Drawing.Color.White;
            this.btnDiem.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDiem.IconSize = 30;
            this.btnDiem.Location = new System.Drawing.Point(0, 210);
            this.btnDiem.Name = "btnDiem";
            this.btnDiem.Size = new System.Drawing.Size(220, 50);
            this.btnDiem.TabIndex = 2;
            this.btnDiem.Text = "Điểm trung bình";
            this.btnDiem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // btnDanhSach
            // 
            this.btnDanhSach.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDanhSach.FlatAppearance.BorderSize = 0;
            this.btnDanhSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDanhSach.ForeColor = System.Drawing.Color.White;
            this.btnDanhSach.IconChar = FontAwesome.Sharp.IconChar.ClipboardList;
            this.btnDanhSach.IconColor = System.Drawing.Color.White;
            this.btnDanhSach.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDanhSach.IconSize = 30;
            this.btnDanhSach.Location = new System.Drawing.Point(0, 160);
            this.btnDanhSach.Name = "btnDanhSach";
            this.btnDanhSach.Size = new System.Drawing.Size(220, 50);
            this.btnDanhSach.TabIndex = 3;
            this.btnDanhSach.Text = "Danh sách";
            this.btnDanhSach.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // btnBaiTap
            // 
            this.btnBaiTap.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBaiTap.FlatAppearance.BorderSize = 0;
            this.btnBaiTap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaiTap.ForeColor = System.Drawing.Color.White;
            this.btnBaiTap.IconChar = FontAwesome.Sharp.IconChar.Book;
            this.btnBaiTap.IconColor = System.Drawing.Color.White;
            this.btnBaiTap.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBaiTap.IconSize = 30;
            this.btnBaiTap.Location = new System.Drawing.Point(0, 110);
            this.btnBaiTap.Name = "btnBaiTap";
            this.btnBaiTap.Size = new System.Drawing.Size(220, 50);
            this.btnBaiTap.TabIndex = 4;
            this.btnBaiTap.Text = "Bài tập";
            this.btnBaiTap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBaiTap.Click += new System.EventHandler(this.btnBaiTap_Click);
            // 
            // btnTongQuan
            // 
            this.btnTongQuan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTongQuan.FlatAppearance.BorderSize = 0;
            this.btnTongQuan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTongQuan.ForeColor = System.Drawing.Color.White;
            this.btnTongQuan.IconChar = FontAwesome.Sharp.IconChar.User;
            this.btnTongQuan.IconColor = System.Drawing.Color.White;
            this.btnTongQuan.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnTongQuan.IconSize = 30;
            this.btnTongQuan.Location = new System.Drawing.Point(0, 60);
            this.btnTongQuan.Name = "btnTongQuan";
            this.btnTongQuan.Size = new System.Drawing.Size(220, 50);
            this.btnTongQuan.TabIndex = 5;
            this.btnTongQuan.Text = "Tổng quan";
            this.btnTongQuan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(220, 60);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "EduStudy LMS";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTopRight
            // 
            this.panelTopRight.BackColor = System.Drawing.Color.Transparent;
            this.panelTopRight.Controls.Add(this.lblTenNguoiDung);
            this.panelTopRight.Controls.Add(this.iconBell);
            this.panelTopRight.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopRight.Location = new System.Drawing.Point(220, 0);
            this.panelTopRight.Name = "panelTopRight";
            this.panelTopRight.Size = new System.Drawing.Size(680, 60);
            this.panelTopRight.TabIndex = 0;
            // 
            // lblTenNguoiDung
            // 
            this.lblTenNguoiDung.AutoSize = true;
            this.lblTenNguoiDung.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTenNguoiDung.Location = new System.Drawing.Point(740, 20);
            this.lblTenNguoiDung.Name = "lblTenNguoiDung";
            this.lblTenNguoiDung.Size = new System.Drawing.Size(39, 23);
            this.lblTenNguoiDung.TabIndex = 0;
            this.lblTenNguoiDung.Text = "Ben";
            // 
            // iconBell
            // 
            this.iconBell.FlatAppearance.BorderSize = 0;
            this.iconBell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBell.IconChar = FontAwesome.Sharp.IconChar.Bell;
            this.iconBell.IconColor = System.Drawing.Color.Gray;
            this.iconBell.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBell.IconSize = 24;
            this.iconBell.Location = new System.Drawing.Point(700, 15);
            this.iconBell.Name = "iconBell";
            this.iconBell.Size = new System.Drawing.Size(32, 32);
            this.iconBell.TabIndex = 1;
            // 
            // frmTongQuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panelTopRight);
            this.Controls.Add(this.panelMenu);
            this.Name = "frmTongQuan";
            this.Text = "frmTongQuan";
            this.panelMenu.ResumeLayout(false);
            this.panelTopRight.ResumeLayout(false);
            this.panelTopRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelTopRight;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTenNguoiDung;
        private FontAwesome.Sharp.IconButton iconBell;
        private FontAwesome.Sharp.IconButton btnThoat;
        private FontAwesome.Sharp.IconButton btnDangKy;
        private FontAwesome.Sharp.IconButton btnDiem;
        private FontAwesome.Sharp.IconButton btnDanhSach;
        private FontAwesome.Sharp.IconButton btnBaiTap;
        private FontAwesome.Sharp.IconButton btnTongQuan;
    }
}