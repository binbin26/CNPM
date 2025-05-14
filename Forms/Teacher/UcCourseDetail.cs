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
    public partial class UcCourseDetail : UserControl
    {
        private int sessionCount = 1;

        public UcCourseDetail()
        {
            InitializeComponent();
        }

        private void btnAddSession_Click(object sender, EventArgs e)
        {
            var ucSession = new UcSessionItem("Buổi " + sessionCount++);
            ucSession.Width = flowPanelSessions.Width - 30;
            flowPanelSessions.Controls.Add(ucSession);
        }
    }
}
