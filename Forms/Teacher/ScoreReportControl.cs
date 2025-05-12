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
    public partial class ScoreReportControl : UserControl
    {
        public ScoreReportControl()
        {
            InitializeComponent();
        }

        private void btnViewScores_Click(object sender, EventArgs e)
        {
            // Logic xem báo cáo điểm
            MessageBox.Show("Đã hiển thị báo cáo điểm!", "Thông báo");
        }
    }
}
