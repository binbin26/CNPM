using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class FormChooseAssignmentType : Form
    {
        private int TeacherID;
        private int CourseID;
        private int SessionID;
        public FormChooseAssignmentType(int teacherID, int courseId, int sessionId)
        {
            InitializeComponent();
            TeacherID = teacherID;
            CourseID = courseId;
            SessionID = sessionId;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (rbMultipleChoice.Checked)
            {
                var setupQuiz = new FormSetupQuiz(TeacherID, CourseID, SessionID);
                setupQuiz.ShowDialog();
            }
            else if (rbEssay.Checked)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = dialog.FileName;
                    string fileName = Path.GetFileName(filePath);
                    string destPath = Path.Combine(Application.StartupPath, "Assignments", fileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(destPath));
                    File.Copy(filePath, destPath, true);

                    using (var conn = DAL.DatabaseHelper.GetConnection())
                    {
                        conn.Open();

                        // Bước 1: Insert Assignments (và lấy ID)
                        string insertAssignment = @"
                    INSERT INTO Assignments (CourseID, Title, SessionID, CreatedBy)
                    OUTPUT INSERTED.AssignmentID
                    VALUES (@CID, @Title, @SID, @CreatedBy)";
                        int assignmentId;
                        using (var cmd = new SqlCommand(insertAssignment, conn))
                        {
                            cmd.Parameters.AddWithValue("@CID", CourseID);
                            cmd.Parameters.AddWithValue("@Title", Path.GetFileNameWithoutExtension(fileName));
                            cmd.Parameters.AddWithValue("@SID", SessionID);
                            cmd.Parameters.AddWithValue("@CreatedBy", TeacherID);
                            assignmentId = (int)cmd.ExecuteScalar();
                        }

                        // Bước 2: Insert AssignmentFiles
                        string insertFile = @"
                    INSERT INTO AssignmentFiles (AssignmentID, CourseID, FileName, FilePath, UploadDate)
                    VALUES (@ID, @CID, @FileName, @Path, GETDATE())";
                        using (var cmd = new SqlCommand(insertFile, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", assignmentId);
                            cmd.Parameters.AddWithValue("@CID", CourseID);
                            cmd.Parameters.AddWithValue("@FileName", fileName);
                            cmd.Parameters.AddWithValue("@Path", "Assignments/" + fileName);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Tạo bài tập tự luận thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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