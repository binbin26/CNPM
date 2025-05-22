using System;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class FormRenameSession : Form
    {
        public string NewTitle => txtNewTitle.Text;

        public FormRenameSession(string currentTitle)
        {
            InitializeComponent();
            txtNewTitle.Text = currentTitle;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewTitle.Text))
            {
                MessageBox.Show("Tiêu đề không được để trống.");
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
