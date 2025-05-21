using System.Windows.Forms;
using System.Drawing;

namespace CNPM.Forms.Teacher
{
    partial class FormRenameSession
    {
        private TextBox txtNewTitle;
        private Button btnOK;

        private void InitializeComponent()
        {
            this.txtNewTitle = new TextBox { Location = new Point(15, 15), Width = 300 };
            this.btnOK = new Button { Text = "Đồng ý", Location = new Point(15, 50), Width = 100 };
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);

            this.Controls.Add(txtNewTitle);
            this.Controls.Add(btnOK);

            this.Text = "Sửa tiêu đề buổi học";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ClientSize = new Size(340, 100);
            this.StartPosition = FormStartPosition.CenterParent;
        }
    }
}
