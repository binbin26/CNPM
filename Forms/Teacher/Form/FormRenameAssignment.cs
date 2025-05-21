using System;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class FormRenameAssignment : Form
    {
        public string NewName => txtName.Text;

        public FormRenameAssignment(string currentName)
        {
            InitializeComponent();
            txtName.Text = currentName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Tên không được để trống.");
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}