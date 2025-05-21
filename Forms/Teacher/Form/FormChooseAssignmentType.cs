using System;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class FormChooseAssignmentType : Form
    {
        private int SessionID;
        private int CourseID;
        private int TeacherID;

        public FormChooseAssignmentType(int sessionId, int courseId, int teacherID)
        {
            InitializeComponent();
            SessionID = sessionId;
            CourseID = courseId;
            TeacherID = teacherID;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (rbMultipleChoice.Checked)
            {
                var setupQuiz = new FormSetupQuiz(SessionID, CourseID, TeacherID);
                setupQuiz.ShowDialog();
            }
            else if (rbEssay.Checked)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = dialog.FileName;
                    string fileName = System.IO.Path.GetFileName(filePath);
                    string destPath = System.IO.Path.Combine(Application.StartupPath, "Assignments", fileName);
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destPath));
                    System.IO.File.Copy(filePath, destPath, true);

                    string query = "INSERT INTO AssignmentFiles (CourseID, Title, FilePath, UploadDate) " +
                                   "VALUES (@CID, @Title, @Path, GETDATE())";

                    using (var conn = DAL.DatabaseHelper.GetConnection())
                    using (var cmd = new System.Data.SqlClient.SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CID", CourseID);
                        cmd.Parameters.AddWithValue("@Title", fileName);
                        cmd.Parameters.AddWithValue("@Path", "Assignments/" + fileName);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Tải bài tập tự luận thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn loại bài tập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.Close();
        }
    }
}
