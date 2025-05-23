using System;
using System.IO;
using System.Windows.Forms;

namespace CNPM.Forms.Student
{
    public partial class ucEssay : UserControl
    {
        private string selectedFilePath = "";
        private int assignmentId;
        private int studentId;
        private readonly AssignmentBLL _assignmentBLL = new AssignmentBLL();
        public ucEssay(int assignmentId, int studentId)
        {
            InitializeComponent();
            this.assignmentId = assignmentId;
            this.studentId = studentId;
            lblAssignmentTitle.Text = "Đề bài: " + _assignmentBLL.GetEssay(assignmentId);
        }

        private void btnDownloadPrompt_Click(object sender, EventArgs e)
        {
            string relativePath = _assignmentBLL.GetEssay(assignmentId);
            string fullPath = Path.Combine(Application.StartupPath, relativePath); 

            if (File.Exists(fullPath))
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    FileName = Path.GetFileName(fullPath),
                    Filter = "All files (*.*)|*.*"
                };
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(fullPath, saveDialog.FileName, true);
                    MessageBox.Show("Tải đề bài thành công.");
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy file đề bài.");
            }
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf|Word files (*.docx)|*.docx|ZIP files (*.zip)|*.zip|RAR files (*.rar)|*.rar|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFilePath = openFileDialog.FileName;
                txtSelectedFile.Text = selectedFilePath;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Vui lòng chọn file bài làm.");
                return;
            }

            string destinationPath = _assignmentBLL.SaveEssaySubmission(assignmentId, studentId, selectedFilePath);
            if (!string.IsNullOrEmpty(destinationPath))
            {
                MessageBox.Show("Nộp bài thành công.");
                btnSubmit.Enabled = false;
            }
            else
            {
                MessageBox.Show("Lỗi khi nộp bài.");
            }
        }
    }
}
