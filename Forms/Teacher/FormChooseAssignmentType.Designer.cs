using System.Windows.Forms;
using System;
using System.Drawing;

namespace CNPM.Forms.Teacher
{
    partial class FormChooseAssignmentType
    {
        private System.Windows.Forms.RadioButton rbMultipleChoice;
        private System.Windows.Forms.RadioButton rbEssay;
        private System.Windows.Forms.Button btnConfirm;

        private void InitializeComponent()
        {
            rbMultipleChoice = new RadioButton();
            rbEssay = new RadioButton();
            btnConfirm = new Button();

            rbMultipleChoice.Text = "Bài tập trắc nghiệm";
            rbMultipleChoice.Size = new Size(200, 20);
            rbMultipleChoice.Location = new Point(20, 20);

            rbEssay.Text = "Bài tập tự luận";
            rbEssay.Size = new Size(200, 30);
            rbEssay.Location = new Point(20, 50);

            btnConfirm.Text = "Xác nhận";
            btnConfirm.Location = new Point(20, 90);
            btnConfirm.Click += new EventHandler(btnConfirm_Click);

            Controls.Add(rbMultipleChoice);
            Controls.Add(rbEssay);
            Controls.Add(btnConfirm);

            Text = "Chọn loại bài tập";
            Size = new Size(250, 180);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
        }
    }
}