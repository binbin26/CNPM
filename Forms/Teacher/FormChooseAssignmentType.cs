using System;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class FormChooseAssignmentType : Form
    {
        public enum AssignmentType { MultipleChoice, Essay }

        public AssignmentType SelectedType { get; private set; }

        public FormChooseAssignmentType()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (rbMultipleChoice.Checked)
                SelectedType = AssignmentType.MultipleChoice;
            else if (rbEssay.Checked)
                SelectedType = AssignmentType.Essay;
            else
            {
                MessageBox.Show("Vui lòng chọn loại bài tập.");
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
