using System.Drawing;
using System.Windows.Forms;
using System;

namespace CNPM.Forms.Teacher
{
    partial class FormChooseAssignmentType
    {
        private RadioButton rbMultipleChoice;
        private RadioButton rbEssay;
        private Button btnConfirm;

        private void InitializeComponent()
        {
            this.Text = "Chọn loại bài tập";
            this.Size = new Size(280, 180);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            rbMultipleChoice = new RadioButton
            {
                Text = "Bài tập trắc nghiệm",
                Location = new Point(20, 20),
                Width = 200
            };

            rbEssay = new RadioButton
            {
                Text = "Bài tập tự luận",
                Location = new Point(20, 50),
                Width = 200
            };

            btnConfirm = new Button
            {
                Text = "Xác nhận",
                Location = new Point(20, 90),
                Width = 100
            };
            btnConfirm.Click += new EventHandler(this.btnConfirm_Click);

            this.Controls.Add(rbMultipleChoice);
            this.Controls.Add(rbEssay);
            this.Controls.Add(btnConfirm);
        }
    }
}
