using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace CNPM.Forms.Student
{
    public partial class ucHienThiBaiTap : UserControl
    {
        private Label lblTitle;
        private Label lblDescription;
        private TextBox txtTuLuan;
        private Button btnNopBai;
        private ucBaiTap.Assignment assignment;
        private frmTongQuan _parentForm;
        private System.Collections.Generic.List<ucBaiTap.Assignment> _assignments;

        public ucHienThiBaiTap(ucBaiTap.Assignment assignmentObj, frmTongQuan parentForm, System.Collections.Generic.List<ucBaiTap.Assignment> assignments)
        {
            InitializeComponent();
            assignment = assignmentObj;
            _parentForm = parentForm;
            _assignments = assignments;
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.Controls.Clear();
            this.Dock = DockStyle.Fill;

            lblTitle = new Label();
            lblTitle.Text = assignment.Title;
            lblTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.AutoSize = true;
            this.Controls.Add(lblTitle);

            lblDescription = new Label();
            lblDescription.Text = assignment.Description;
            lblDescription.Font = new Font("Arial", 12);
            lblDescription.Location = new Point(20, 60);
            lblDescription.AutoSize = true;
            this.Controls.Add(lblDescription);

            if (assignment.Type == ucBaiTap.AssignmentType.TuLuan)
            {
                txtTuLuan = new TextBox();
                txtTuLuan.Multiline = true;
                txtTuLuan.Width = 600;
                txtTuLuan.Height = 120;
                txtTuLuan.Location = new Point(20, 100);
                this.Controls.Add(txtTuLuan);
            }

            btnNopBai = new Button();
            btnNopBai.Text = "Nộp bài";
            btnNopBai.Location = new Point(20, 270);
            btnNopBai.Click += BtnNopBai_Click;
            this.Controls.Add(btnNopBai);
        }

        private void BtnNopBai_Click(object sender, EventArgs e)
        {
            if (assignment.Type == ucBaiTap.AssignmentType.TuLuan)
            {
                if (string.IsNullOrWhiteSpace(txtTuLuan.Text))
                {
                    MessageBox.Show("Vui lòng nhập bài làm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                assignment.HanNop = "Đã làm";
                MessageBox.Show($"Đã nộp bài tự luận!\nNội dung: {txtTuLuan.Text}", "Nộp bài thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _parentForm.LoadUserControl(new ucBaiTap(_parentForm, _assignments));
            }
        }
    }
}
