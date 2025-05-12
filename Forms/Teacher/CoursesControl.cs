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
    public partial class CoursesControl : UserControl
    {
        public CoursesControl()
        {
            InitializeComponent();
        }

        private void btnSelectCourse_Click(object sender, EventArgs e)
        {
            if (listBoxCourses.SelectedItem != null)
            {
                MessageBox.Show($"Đã chọn khóa học: {listBoxCourses.SelectedItem.ToString()}", "Thông báo");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khóa học.", "Cảnh báo");
            }
        }
        private void listBoxCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCourse = listBoxCourses.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedCourse))
            {
                listBoxDocuments.Items.Clear();
                // Giả sử load dữ liệu từ db theo môn học
                if (selectedCourse == "Lập trình C#")
                {
                    listBoxDocuments.Items.Add("C# - Bài giảng số 1.pdf");
                    listBoxDocuments.Items.Add("C# - Bài tập tuần 1.docx");
                }
                else if (selectedCourse == "Cơ sở dữ liệu")
                {
                    listBoxDocuments.Items.Add("SQL - Bài giảng số 1.pdf");
                    listBoxDocuments.Items.Add("SQL - Bài tập tuần 1.docx");
                }
                else if (selectedCourse == "Cấu trúc dữ liệu")
                {
                    listBoxDocuments.Items.Add("Mạng - Slide bài 1.pptx");
                    listBoxDocuments.Items.Add("Mạng - Bài tập chương 1.docx");
                }
                else if (selectedCourse == "Hệ điều hành")
                {
                    listBoxDocuments.Items.Add("PTTK - Đề cương chương 1.pdf");
                    listBoxDocuments.Items.Add("PTTK - Bài tập số 1.docx");
                }
            }
        }
    }
}
