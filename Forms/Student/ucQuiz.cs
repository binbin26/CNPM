using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM.Forms.Student
{
    public partial class ucQuiz : UserControl
    {
        private int assignmentId;
        public ucQuiz(int assignmentId)
        {
            InitializeComponent();
            this.assignmentId = assignmentId;
        }
    }
}
