using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using CNPM.DAL;

namespace CNPM.Forms.Teacher
{
    public partial class UcSessionItem : UserControl
    {
        private int SessionID;
        private int CourseID;
        private string Title;

        public UcSessionItem(int sessionId, int courseId, string title)
        {
            InitializeComponent();
            SessionID = sessionId;
            CourseID = courseId;
            Title = title;

            lblSessionTitle.Text = Title;
        }

        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = dialog.FileName;
                string fileName = Path.GetFileName(filePath);

                string destPath = Path.Combine(Application.StartupPath, "Uploads", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(destPath));
                File.Copy(filePath, destPath, true);

                string query = "INSERT INTO CourseDocuments (CourseID, Title, FilePath, UploadDate) VALUES (@CourseID, @Title, @Path, GETDATE())";

                using (var conn = DatabaseHelper.GetConnection())
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", CourseID);
                    cmd.Parameters.AddWithValue("@Title", fileName);
                    cmd.Parameters.AddWithValue("@Path", "Uploads/" + fileName);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Đính kèm thành công!");
            }
        }
        private void btnCreateAssignment_Click(object sender, EventArgs e)
        {
            FormChooseAssignmentType choose = new FormChooseAssignmentType(SessionID, CourseID);
            choose.ShowDialog();
        }
    }
}