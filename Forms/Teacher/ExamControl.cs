using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class ExamControl : UserControl
    {
        public ExamControl()
        {
            InitializeComponent();
        }

        private void btnCreateExam_Click(object sender, EventArgs e)
        {
            if (radioBtnMultipleChoice.Checked)
            {
                var editor = new QuestionEditorForm();
                editor.ShowDialog();
            }
            else if (radioBtnEssay.Checked)
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Đã tạo thư mục lưu bài tự luận: " + dialog.SelectedPath);
                    // Save logic here
                }
            }
        }
    }
}
