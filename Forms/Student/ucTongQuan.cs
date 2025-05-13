using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CNPM.Forms.Student
{
    public partial class ucTongQuan : UserControl
    {
        private Label lblTitle;
        private Label lblHoTen;
        private Label lblEmail;
        private Label lblHoTenValue;
        private Label lblEmailValue;
        private Panel panelInfo;
        private int _userId;

        public ucTongQuan(int userId)
        {
            _userId = userId;
            InitializeComponent();
            InitializeCustomComponents();
            LoadStudentInfo();
        }

        private void InitializeCustomComponents()
        {
            // Khởi tạo panel chứa thông tin
            panelInfo = new Panel();
            panelInfo.Dock = DockStyle.Top;
            panelInfo.Height = 200;
            panelInfo.Padding = new Padding(20);
            this.Controls.Add(panelInfo);

            // Tiêu đề
            lblTitle = new Label();
            lblTitle.Text = "THÔNG TIN CÁ NHÂN: SINH VIÊN";
            lblTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 20);
            panelInfo.Controls.Add(lblTitle);

            // Label Họ tên
            lblHoTen = new Label();
            lblHoTen.Text = "Họ và tên:";
            lblHoTen.Font = new Font("Arial", 12);
            lblHoTen.AutoSize = true;
            lblHoTen.Location = new Point(20, 70);
            panelInfo.Controls.Add(lblHoTen);

            // Giá trị Họ tên
            lblHoTenValue = new Label();
            lblHoTenValue.Font = new Font("Arial", 12);
            lblHoTenValue.AutoSize = true;
            lblHoTenValue.Location = new Point(150, 70);
            panelInfo.Controls.Add(lblHoTenValue);

            // Label Email
            lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.Font = new Font("Arial", 12);
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(20, 110);
            panelInfo.Controls.Add(lblEmail);

            // Giá trị Email
            lblEmailValue = new Label();
            lblEmailValue.Font = new Font("Arial", 12);
            lblEmailValue.AutoSize = true;
            lblEmailValue.Location = new Point(150, 110);
            panelInfo.Controls.Add(lblEmailValue);
        }

        private void LoadStudentInfo()
        {
            try
            {
                string connectionString = "Data Source=.;Initial Catalog=EduMasterDB;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT FullName, Email FROM Users WHERE UserID = @UserID AND Role = 'Student'";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", _userId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblHoTenValue.Text = reader["FullName"].ToString();
                                lblEmailValue.Text = reader["Email"].ToString();
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
    }
}
