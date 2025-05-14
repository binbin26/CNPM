using CNPM.BLL;
using CNPM.DAL;
using CNPM.Forms.Auth;
using CNPM.Models.Users;
using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CNPM.Forms.Student
{
    public partial class frmTongQuan : Form
    {
        private Panel panelMain;
        private int _userId;
        private string _username;
        UserBLL userBLL = new UserBLL();

        public frmTongQuan(int userId, string username)
        {
            _userId = userId;
            _username = username;
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

            // Load UserControl mặc định
            LoadUserControl(new ucTongQuan(_username));
                User user = userBLL.GetUserByUsername(_username);
                if (user != null)
                {
                    lblChao.Text = "Xin chào sinh viên " + user.FullName + "!";
                }
        }

        public void LoadUserControl(UserControl uc)
        {
            panelMain.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelMain.Controls.Add(uc);
        }

        private void btnMenuTongQuan_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucTongQuan(_username));
        }

        private void btnMenuBaiTap_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucBaiTap(this));
        }

        private void btnMenuDanhSach_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucDanhSach(_username));
        }

        private void btnMenuDiemTrungBinh_Click(object sender, EventArgs e)
        {
            LoadUserControl(new ucDiemTrungBinh(_username));
        }

        private void btnMenuThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
            // Mở lại form đăng nhập
            LoginForm loginForm = new LoginForm();
        }
    }
}
