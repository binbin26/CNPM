using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CNPM.DAL;
using CNPM.Models.Assignments;
using FontAwesome.Sharp;
using Microsoft.VisualBasic;

namespace CNPM.Forms.Teacher
{
    public partial class UcSessionItem : UserControl
    {
        private int TeacherID;
        private int SessionID;
        private int CourseID;
        private string Title;
        private static int sessionCounter = 0;

        public UcSessionItem(int teacherID, int courseId, int sessionId, string title)
        {
            InitializeComponent();
            TeacherID = teacherID;
            CourseID = courseId;
            SessionID = sessionId;
            Title = title;
            lblSessionTitle.Text = title;
            StyleModernButton(btnAttachFile, Color.FromArgb(100, 149, 237));
            StyleModernButton(btnCreateAssignment, Color.FromArgb(72, 201, 176));
            btnAttachFile.Click += btnAttachFile_Click;
            btnCreateAssignment.Click += btnCreateAssignment_Click;
            LoadAssignments();
            Color[] sessionColors = new Color[]
            {
                Color.FromArgb(255, 245, 225),
                Color.FromArgb(225, 255, 245),
                Color.FromArgb(225, 245, 255),
                Color.FromArgb(245, 225, 255)
            };
            this.BackColor = sessionColors[sessionCounter % sessionColors.Length];
            sessionCounter++;
        }

        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Tệp được hỗ trợ (*.pdf;*.docx;*.xlsx;*.txt;*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.pdf;*.docx;*.xlsx;*.txt;*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog.Title = "Chọn tệp để đính kèm";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string fileName = Path.GetFileName(filePath);
                string extension = Path.GetExtension(filePath).ToLower();

                // Xác định loại tài liệu
                string documentType;
                switch (extension)
                {
                    case ".pdf": documentType = "PDF"; break;
                    case ".docx": documentType = "DOCX"; break;
                    case ".xlsx": documentType = "EXCEL"; break;
                    case ".txt": documentType = "TXT"; break;
                    case ".jpg": case ".jpeg": case ".png": case ".bmp": case ".gif": documentType = "IMAGE"; break;
                    default:
                        MessageBox.Show("Loại tệp không được hỗ trợ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }
                try
                {
                    // Đường dẫn lưu file trong thư mục ứng dụng
                    string uploadsFolder = Path.Combine(Application.StartupPath, "Uploads");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    string destPath = Path.Combine(uploadsFolder, fileName);
                    File.Copy(filePath, destPath, true);

                    using (var conn = DAL.DatabaseHelper.GetConnection())
                    {
                        conn.Open();

                        string query = "INSERT INTO CourseDocuments (CourseID, Title, FilePath, UploadDate, UploadedBy, DocumentType, SessionID) " +
                                       "VALUES (@CourseID, @Title, @FilePath, GETDATE(), @UploadedBy, @DocumentType, @SessionID)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@CourseID", CourseID);
                            cmd.Parameters.AddWithValue("@Title", Path.GetFileNameWithoutExtension(fileName));
                            cmd.Parameters.AddWithValue("@FilePath", $"Uploads/{fileName}");
                            cmd.Parameters.AddWithValue("@UploadedBy", TeacherID); // ID người dùng đăng nhập
                            cmd.Parameters.AddWithValue("@DocumentType", documentType);
                            cmd.Parameters.AddWithValue("@SessionID", SessionID); // Gắn với buổi học nếu có

                            cmd.ExecuteNonQuery();
                        }
                        LoadAssignments();
                    }

                    MessageBox.Show("Tệp đã được đính kèm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đính kèm tệp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCreateAssignment_Click(object sender, EventArgs e)
        {
            FormChooseAssignmentType choose = new FormChooseAssignmentType(TeacherID, CourseID, SessionID);
            choose.FormClosed += (s, args) => LoadAssignments();
            choose.ShowDialog();
        }
        private void LoadAssignments()
        {
            flowPanelAssignments.Controls.Clear();

            // === HIỂN THỊ BÀI TẬP ===
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand("SELECT AssignmentID, Title FROM Assignments WHERE SessionID = @SID", conn))
            {
                cmd.Parameters.AddWithValue("@SID", SessionID);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int assignmentId = reader.GetInt32(0);
                        string title = reader.GetString(1);

                        var lbl = CreateItemLabel(title, IconChar.ClipboardList);
                        lbl.Click += (s, e) =>
                        {
                            bool isMultipleChoice = CheckIfAssignmentIsMultipleChoice(assignmentId);
                            if (isMultipleChoice)
                            {
                                new FormViewQuestions(LoadQuestionsFromDatabase(assignmentId)).ShowDialog();
                            }
                            else
                            {
                                // Thay thế bằng mở file PDF/docx đã đính kèm
                                string filePath = GetEssayFilePath(assignmentId);
                                string fullPath = Path.Combine(Application.StartupPath, filePath);
                                if (File.Exists(fullPath))
                                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(fullPath) { UseShellExecute = true });
                                else
                                    MessageBox.Show("Không tìm thấy file bài tập tự luận.");
                            }
                        };

                        var btnRename = CreateItemButton("Đổi tên", () =>
                        {
                            var f = new FormRenameAssignment(title);
                            if (f.ShowDialog() == DialogResult.OK)
                            {
                                using (var c = DatabaseHelper.GetConnection())
                                {
                                    c.Open();
                                    var cmdu = new SqlCommand("UPDATE Assignments SET Title = @Title WHERE AssignmentID = @ID", c);
                                    cmdu.Parameters.AddWithValue("@Title", f.NewName);
                                    cmdu.Parameters.AddWithValue("@ID", assignmentId);
                                    cmdu.ExecuteNonQuery();
                                }
                                LoadAssignments();
                            }
                        });

                        var btnDel = CreateItemButton("Xóa", () =>
                        {
                            if (MessageBox.Show("Xóa bài tập này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                using (var c = DatabaseHelper.GetConnection())
                                {
                                    c.Open();
                                    // Xóa câu hỏi trước
                                    var cmdQ = new SqlCommand("DELETE FROM Questions WHERE AssignmentID = @ID", c);
                                    cmdQ.Parameters.AddWithValue("@ID", assignmentId);
                                    cmdQ.ExecuteNonQuery();

                                    // Xóa bài tập trắc nghiệm (nếu có)
                                    var cmdMC = new SqlCommand("DELETE FROM AssignmentMC WHERE AssignmentID = @ID", c);
                                    cmdMC.Parameters.AddWithValue("@ID", assignmentId);
                                    cmdMC.ExecuteNonQuery();

                                    // Xóa bài tập tự luận (nếu có)
                                    var cmdFile = new SqlCommand("DELETE FROM AssignmentFiles WHERE AssignmentID = @ID", c);
                                    cmdFile.Parameters.AddWithValue("@ID", assignmentId);
                                    cmdFile.ExecuteNonQuery();

                                    // Cuối cùng xóa bài tập
                                    var cmdDel = new SqlCommand("DELETE FROM Assignments WHERE AssignmentID = @ID", c);
                                    cmdDel.Parameters.AddWithValue("@ID", assignmentId);
                                    cmdDel.ExecuteNonQuery();
                                }
                                LoadAssignments();
                            }
                        });

                        flowPanelAssignments.Controls.Add(lbl);
                        flowPanelAssignments.Controls.Add(btnRename);
                        flowPanelAssignments.Controls.Add(btnDel);
                    }
                }
            }

            // === HIỂN THỊ FILE ĐÍNH KÈM ===
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand("SELECT DocumentID, Title, FilePath FROM CourseDocuments WHERE SessionID = @SID", conn))
            {
                cmd.Parameters.AddWithValue("@SID", SessionID);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int docId = reader.GetInt32(0);
                        string title = reader.GetString(1);
                        string path = reader.GetString(2);

                        var lblFile = CreateItemLabel(title, IconChar.Paperclip);
                        lblFile.Click += (s, e) =>
                        {
                            try
                            {
                                string fullPath = Path.Combine(Application.StartupPath, path.Replace("/", "\\"));
                                if (File.Exists(fullPath))
                                {
                                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                                    {
                                        FileName = fullPath,
                                        UseShellExecute = true
                                    });
                                }
                                else
                                {
                                    MessageBox.Show("Không tìm thấy tệp tại: " + fullPath);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi khi mở tệp: " + ex.Message);
                            }
                        };

                        var btnDelFile = CreateItemButton("Xóa file", () =>
                        {
                            if (MessageBox.Show("Xóa file này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                using (var c = DatabaseHelper.GetConnection())
                                {
                                    c.Open();
                                    var cmdDel = new SqlCommand("DELETE FROM CourseDocuments WHERE DocumentID = @ID", c);
                                    cmdDel.Parameters.AddWithValue("@ID", docId);
                                    cmdDel.ExecuteNonQuery();
                                }
                                LoadAssignments();
                            }
                        });

                        flowPanelAssignments.Controls.Add(lblFile);
                        flowPanelAssignments.Controls.Add(btnDelFile);
                    }
                }
            }
            flowPanelAssignments.Controls.Add(btnAttachFile);
            flowPanelAssignments.Controls.Add(btnCreateAssignment);
        }
        private void btnEditTitle_Click(object sender, EventArgs e)
        {
            var form = new FormRenameSession(this.Title);
            if (form.ShowDialog() == DialogResult.OK)
            {
                string newTitle = form.NewTitle;
                if (newTitle != this.Title)
                {
                    using (var conn = DAL.DatabaseHelper.GetConnection())
                    using (var cmd = new SqlCommand("UPDATE Sessions SET Title = @Title WHERE SessionID = @ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", newTitle);
                        cmd.Parameters.AddWithValue("@ID", SessionID);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }

                    this.Title = newTitle;
                    lblSessionTitle.Text = newTitle;
                    MessageBox.Show("Đã cập nhật tiêu đề buổi học.");
                }
            }
        }
        private Label CreateItemLabel(string text, IconChar icon = IconChar.None)
        {
            var lbl = new Label
            {
                AutoSize = false,
                Width = flowPanelAssignments.Width - 40,
                Height = 40,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.WhiteSmoke,
                ForeColor = Color.Black,
                Padding = new Padding(10, 6, 10, 6),
                Margin = new Padding(5),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleLeft
            };

            if (icon != IconChar.None)
            {
                var ic = new IconPictureBox
                {
                    IconChar = icon,
                    IconColor = Color.Gray,
                    IconSize = 18,
                    Location = new Point(10, 10),
                    Size = new Size(20, 20),
                };

                lbl.Padding = new Padding(32, 0, 0, 0);
                lbl.Controls.Add(ic);
            }

            lbl.Text = "   " + text; // giữ khoảng trống cho icon
            return lbl;
        }
        private Button CreateItemButton(string text, Action onClick)
        {
            var btn = new Button
            {
                Text = text,
                Height = 30,
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Padding = new Padding(10, 5, 10, 5),
                BackColor = text.Contains("Xóa") ? Color.IndianRed : Color.SteelBlue,
                Margin = new Padding(5)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) => onClick();
            return btn;
        }
        private List<Question> LoadQuestionsFromDatabase(int assignmentId)
        {
            List<Question> questions = new List<Question>();
            string query = "SELECT QuestionID, QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectAnswer FROM Questions WHERE AssignmentID = @AID";

            using (var conn = DAL.DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@AID", assignmentId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        questions.Add(new Question
                        {
                            QuestionID = reader.GetInt32(0),
                            QuestionText = reader.GetString(1),
                            OptionA = reader.GetString(2),
                            OptionB = reader.GetString(3),
                            OptionC = reader.GetString(4),
                            OptionD = reader.GetString(5),
                            CorrectAnswer = reader.GetString(6)
                        });
                    }
                }
            }
            return questions;
        }
        private void btnDeleteSession_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa buổi học này? Tất cả bài tập, câu hỏi và tài liệu đính kèm sẽ bị xóa.",
       "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // 1. Lấy tất cả AssignmentID thuộc buổi học này
                    List<int> assignmentIds = new List<int>();
                    using (var cmd = new SqlCommand("SELECT AssignmentID FROM Assignments WHERE SessionID = @SID", conn))
                    {
                        cmd.Parameters.AddWithValue("@SID", SessionID);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                assignmentIds.Add(reader.GetInt32(0));
                        }
                    }

                    foreach (int assignmentId in assignmentIds)
                    {
                        // 2. Xóa câu hỏi
                        using (var cmd = new SqlCommand("DELETE FROM Questions WHERE AssignmentID = @AID", conn))
                        {
                            cmd.Parameters.AddWithValue("@AID", assignmentId);
                            cmd.ExecuteNonQuery();
                        }

                        // 3. Xóa dữ liệu bài tập trắc nghiệm (AssignmentMC)
                        using (var cmd = new SqlCommand("DELETE FROM AssignmentMC WHERE AssignmentID = @AID", conn))
                        {
                            cmd.Parameters.AddWithValue("@AID", assignmentId);
                            cmd.ExecuteNonQuery();
                        }

                        // 4. Xóa file bài tập tự luận (AssignmentFiles)
                        using (var cmd = new SqlCommand("DELETE FROM AssignmentFiles WHERE AssignmentID = @AID", conn))
                        {
                            cmd.Parameters.AddWithValue("@AID", assignmentId);
                            cmd.ExecuteNonQuery();
                        }

                        // 5. Xóa bài tập chính
                        using (var cmd = new SqlCommand("DELETE FROM Assignments WHERE AssignmentID = @AID", conn))
                        {
                            cmd.Parameters.AddWithValue("@AID", assignmentId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // 6. Xóa tài liệu đính kèm của buổi học
                    using (var cmd = new SqlCommand("DELETE FROM CourseDocuments WHERE SessionID = @SID", conn))
                    {
                        cmd.Parameters.AddWithValue("@SID", SessionID);
                        cmd.ExecuteNonQuery();
                    }

                    // 7. Cuối cùng, xóa buổi học
                    using (var cmd = new SqlCommand("DELETE FROM Sessions WHERE SessionID = @SID", conn))
                    {
                        cmd.Parameters.AddWithValue("@SID", SessionID);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Xóa khỏi giao diện
                this.Parent?.Controls.Remove(this);
            }
        }
        private bool CheckIfAssignmentIsMultipleChoice(int assignmentId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand("SELECT COUNT(*) FROM AssignmentMC WHERE AssignmentID = @ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", assignmentId);
                conn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        private string GetEssayFilePath(int assignmentId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand("SELECT FilePath FROM AssignmentFiles WHERE AssignmentID = @ID", conn))
            {
                cmd.Parameters.AddWithValue("@ID", assignmentId);
                conn.Open();
                return (string)cmd.ExecuteScalar();
            }
        }
        private void StyleModernButton(Button button, Color backColor)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.MouseEnter += (s, e) => button.BackColor = ControlPaint.Light(backColor);
            button.MouseLeave += (s, e) => button.BackColor = backColor;
            button.FlatAppearance.BorderSize = 0;
            Color backcolor = Color.FromArgb(60, 179, 113); // MediumSeaGreen
            button.BackColor = backColor;
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button.Padding = new Padding(6);
            button.Margin = new Padding(8, 5, 8, 5);
            button.AutoSize = true;
        }
    }
}