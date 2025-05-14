using System.Drawing;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class FormRenameAssignment
    {
        private TextBox txtName;
        private Button btnOK;

        private void InitializeComponent()
        {
            txtName = new TextBox { Location = new Point(10, 10), Width = 250 };
            btnOK = new Button { Text = "Đồng ý", Location = new Point(10, 40), Width = 80 };
            btnOK.Click += btnOK_Click;

            Controls.Add(txtName);
            Controls.Add(btnOK);

            Text = "Đổi tên bài tập";
            Size = new Size(280, 120);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
        }
    }
}