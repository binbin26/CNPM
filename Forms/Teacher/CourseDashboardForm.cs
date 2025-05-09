using CNPM.BLL;
using CNPM.Forms.Shared;
using CNPM.Models.Courses;
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
    public partial class CourseDashboardForm : Form
    {
        private readonly CourseBLL _courseBLL;
        private readonly int _teacherID;

        public CourseDashboardForm(int teacherID)
        {
            InitializeComponent();
            _teacherID = teacherID;
            LoadCourses();
        }

        // Tải danh sách khóa học
        private void LoadCourses()
        {
            List<Course> courses = _courseBLL.GetCoursesByTeacher(_teacherID);
            dataGridViewCourses.DataSource = courses;
            ConfigureDataGridView();
        }

        // Cấu hình DataGridView
        private void ConfigureDataGridView()
        {
            dataGridViewCourses.AutoGenerateColumns = false;
            dataGridViewCourses.Columns.Clear();

            // Thêm các cột
            dataGridViewCourses.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CourseCode",
                HeaderText = "Mã Khóa Học"
            });

            dataGridViewCourses.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CourseName",
                HeaderText = "Tên Khóa Học"
            });

            dataGridViewCourses.Columns.Add(new DataGridViewButtonColumn
            {
                Text = "Quản Lý",
                UseColumnTextForButtonValue = true,
                HeaderText = "Thao Tác"
            });
        }

        // Xử lý click nút "Quản Lý"
        private void dataGridViewCourses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dataGridViewCourses.Columns["Quản Lý"].Index)
                return;

            int courseID = (int)dataGridViewCourses.Rows[e.RowIndex].Cells["CourseID"].Value;
            OpenCourseDetailFormForm(courseID);
        }

        // Mở form quản lý khóa học
        private void OpenCourseDetailFormForm(int courseID)
        {
            CourseDetailForm form = new CourseDetailForm(courseID);
            form.ShowDialog();
            LoadCourses(); // Làm mới danh sách sau khi đóng form
        }
    }
}
