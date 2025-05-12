using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class UploadControl : UserControl
    {
        public UploadControl()
        {
            InitializeComponent();
        }

        private void btnUploadDocument_Click(object sender, EventArgs e)
        {
            // Mở hộp thoại chọn tài liệu để upload
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Xử lý upload tài liệu ở đây
                MessageBox.Show($"Đã chọn tài liệu: {openFileDialog.FileName}", "Thông báo");
            }
        }

        private void btnUploadAssignment_Click(object sender, EventArgs e)
        {
            // Mở hộp thoại chọn bài tập để upload
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Xử lý upload bài tập ở đây
                MessageBox.Show($"Đã chọn bài tập: {openFileDialog.FileName}", "Thông báo");
            }
        }
    }
}
