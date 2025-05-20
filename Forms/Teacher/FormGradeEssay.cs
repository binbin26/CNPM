using System;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class FormGradeEssay : Form
    {
        public int? Score { get; private set; }

        public FormGradeEssay(int? currentScore = null)
        {
            InitializeComponent();

            if (currentScore.HasValue)
            {
                txtScore.Text = currentScore.ToString();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtScore.Text, out int score) || score < 0 || score > 10)
            {
                MessageBox.Show("Điểm phải là số nguyên từ 0 đến 10.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Score = score;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}