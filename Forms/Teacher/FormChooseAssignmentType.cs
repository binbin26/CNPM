using System;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class FormChooseAssignmentType : Form
    {
        private int SessionID, CourseID;

        public FormChooseAssignmentType(int sessionId, int courseId)
        {
            InitializeComponent();
            SessionID = sessionId;
            CourseID = courseId;
        }

        private void btnMultipleChoice_Click(object sender, EventArgs e)
        {
            FormSetupQuiz setup = new FormSetupQuiz(SessionID, CourseID);
            setup.ShowDialog();
            this.Close();
        }

        private void btnEssay_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = System.IO.Path.GetFileName(dialog.FileName);
                string destPath = System.IO.Path.Combine(Application.StartupPath, "Assignments", fileName);
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destPath));
                System.IO.File.Copy(dialog.FileName, destPath, true);

                string query = "INSERT INTO AssignmentFiles (AssignmentID, FileName, FilePath, UploadDate) VALUES (@AID, @File, @Path, GETDATE())";

                using (var conn = DAL.DatabaseHelper.GetConnection())
                using (var cmd = new System.Data.SqlClient.SqlCommand(query, conn))
                {
                    conn.Open();
                    // Create assignment record first
                    string q = "INSERT INTO Assignments (CourseID, Title, CreatedAt) OUTPUT INSERTED.AssignmentID VALUES (@CID, @Title, GETDATE())";
                    using (var cmd1 = new System.Data.SqlClient.SqlCommand(q, conn))
                    {
                        cmd1.Parameters.AddWithValue("@CID", CourseID);
                        cmd1.Parameters.AddWithValue("@Title", "Bài tập tự luận");
                        int newAID = (int)cmd1.ExecuteScalar();

                        cmd.Parameters.AddWithValue("@AID", newAID);
                        cmd.Parameters.AddWithValue("@File", fileName);
                        cmd.Parameters.AddWithValue("@Path", "Assignments/" + fileName);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Đã tạo bài tập tự luận thành công.");
            }
            this.Close();
        }
    }
}