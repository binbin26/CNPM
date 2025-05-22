using System.Drawing;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    partial class FormInputTitle
    {
        private TextBox txtTitle;
        private Button btnOK;

        private void InitializeComponent()
        {
            this.txtTitle = new TextBox();
            this.btnOK = new Button();

            // txtTitle
            this.txtTitle.Location = new Point(20, 20);
            this.txtTitle.Width = 240;
            this.txtTitle.Font = new Font("Segoe UI", 10);

            // btnOK
            this.btnOK.Text = "Xác nhận";
            this.btnOK.Location = new Point(20, 60);
            this.btnOK.Width = 100;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);

            // FormInputTitle
            this.ClientSize = new Size(300, 120);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.btnOK);
            this.Text = "Nhập tiêu đề buổi học";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }
    }
}
