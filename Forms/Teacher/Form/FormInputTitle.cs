// FormInputTitle.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class FormInputTitle : Form
    {
        public string InputTitle => txtTitle.Text.Trim();

        public FormInputTitle()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Tiêu đề không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}