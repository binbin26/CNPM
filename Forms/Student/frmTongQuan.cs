using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CNPM.Forms.Student
{
    public partial class frmTongQuan : Form
    {
        private Panel panelMain;
        private int _userId;

        public frmTongQuan(int userId)
        {
            _userId = userId;
            InitializeComponent();
            InitCustomComponent();
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Hệ Thống Quản Lý Sinh Viên";
        }

        private void InitCustomComponent()
        {
            // Khởi tạo panelMain (bên phải)
            panelMain = new Panel();
            panelMain.Dock = DockStyle.Fill;
            panelMain.BackColor = Color.White;
            this.Controls.Add(panelMain);
            // panelMenu đã được khởi tạo sẵn ở Designer, Dock = Left
            this.Controls.SetChildIndex(panelMain, 0); // Đảm bảo panelMain nằm bên phải panelMenu

            // Gán sự kiện cho các nút
            btnTongQuan.Click += btnMenuTongQuan_Click;
            btnBaiTap.Click += btnMenuBaiTap_Click;
            btnDanhSach.Click += btnMenuDanhSach_Click;
            btnDiem.Click += btnMenuDiemTrungBinh_Click;
            btnDangKy.Click += (s, e) => LoadUserControl(new ucDangKyHocPhan(_userId));
            btnThoat.Click += btnMenuThoat_Click;

            // Load UserControl mặc định
            LoadUserControl(new ucTongQuan(_userId));
        }

        public void LoadUserControl(UserControl uc)
        {
            panelMain.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelMain.Controls.Add(uc);
        }

        private void btnMenuTongQuan_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucTongQuan(_userId));
        }

        private void btnMenuBaiTap_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucBaiTap(this));
        }

        private void btnMenuDanhSach_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucDanhSach());
        }

        private void btnMenuDiemTrungBinh_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucDiemTrungBinh());
        }

        private void btnMenuThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBaiTap_Click(object sender, EventArgs e)
        {

        }
    }
}
