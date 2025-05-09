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
    public partial class UploadContentForm: Form
    {
        private readonly int _courseID;
        public UploadContentForm(int courseID)
        {
            _courseID = courseID;
            InitializeComponent();
        }
    }
}
