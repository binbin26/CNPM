using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CNPM.Forms.Student
{
    public partial class TaiLieu : Form
    {
        private int courseId;
        private string filePath;
        public TaiLieu(int courseId)
        {
            InitializeComponent();
            this.courseId = courseId;
            LoadDocuments();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            filePath = btn.Tag.ToString();

            if (File.Exists(filePath))
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    FileName = Path.GetFileName(filePath),
                    Filter = "All files (*.*)|*.*"
                };
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(filePath, saveDialog.FileName, true);
                    MessageBox.Show("Tải tài liệu thành công.");
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy tài liệu.");
            }
        }

        private void LoadDocuments()
        {
            var documents = AssignmentBLL.Instance.GetCourseDocuments(courseId);
            flowPanel.Controls.Clear();

            foreach (var doc in documents)
            {
                Panel panel = new Panel();
                panel.BorderStyle = BorderStyle.FixedSingle;
                panel.Width = 740;
                panel.Height = 80;
                panel.Padding = new Padding(10);
                panel.Margin = new Padding(10);

                Label label = new Label();
                lblTitle.Text = doc.Title + (string.IsNullOrEmpty(doc.SessionTitle) ? "" : " - " + doc.SessionTitle);
                lblTitle.Text = doc.Title + (string.IsNullOrEmpty(doc.SessionTitle) ? "" : " - " + doc.SessionTitle);
                lblTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lblTitle.AutoSize = true;

                Button btnDownload = new Button();
                btnDownload.Text = "Tải về";
                btnDownload.Tag = doc.FilePath;
                btnDownload.Click += btnDownload_Click;
                btnDownload.Anchor = AnchorStyles.Right;
                btnDownload.Location = new Point(650, 25);

                panel.Controls.Add(lblTitle);
                panel.Controls.Add(btnDownload);
                lblTitle.Location = new Point(10, 25);

                flowPanel.Controls.Add(panel);
            }
        }
    }

}
