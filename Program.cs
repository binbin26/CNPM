using CNPM.DAL;
using CNPM.Forms.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!DatabaseHelper.TestConnection())
            {
                MessageBox.Show("Không thể kết nối đến database!");
                return;
            }
            Application.Run(new LoginForm());
            Application.EnableVisualStyles();
        }
    }
}
