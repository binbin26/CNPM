using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CNPM.BLL;
using CNPM.Models.Courses;

namespace CNPM.Forms.Student
{
    public partial class TaiLieu : Form
    {
        public TaiLieu(List<Assignment> assignments)
        {
            InitializeComponent();
            dataGridView1.DataSource = assignments; // hoặc gán vào controls phù hợp
        }
    }

}
