using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using CNPM.DAL;
using CNPM.Models.Courses.Sessions;

namespace CNPM.Forms.Teacher
{
    public partial class UcSessionItem : UserControl
    {
        private int courseId;
        private int teacherId;
        private string sessionFilePath;
        public string SessionTitle { get; set; }
        private SessionData currentSessionData;

        public UcSessionItem(string title, int courseId, int teacherId)
        {
            InitializeComponent();
            SessionTitle = title;
            lblSessionTitle.Text = title;
            this.courseId = courseId;
            this.teacherId = teacherId;

            sessionFilePath = EnsureSessionFileAndInsertToDB();
            currentSessionData = new SessionData { Title = title };
        }

        private string EnsureSessionFileAndInsertToDB()
        {
            string safeCourse = "Course_" + courseId;
            string folder = Path.Combine(Application.StartupPath, "Sessions", safeCourse);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string fileName = string.Concat(SessionTitle.Split(Path.GetInvalidFileNameChars())) + ".txt";
            string filePath = Path.Combine(folder, fileName);

            string relativePath = Path.Combine("Sessions", safeCourse, fileName);

            // Insert vào bảng Sessions nếu chưa tồn tại
            string checkQuery = "SELECT COUNT(*) FROM Sessions WHERE CourseID = @CourseID AND Title = @Title";
            string insertQuery = @"INSERT INTO Sessions (CourseID, Title, FilePath, CreatedBy)
                                  SELECT @CourseID, @Title, @FilePath, @CreatedBy
                                  WHERE NOT EXISTS (SELECT 1 FROM Sessions WHERE CourseID = @CourseID AND Title = @Title)";

            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand(checkQuery, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@CourseID", courseId);
                cmd.Parameters.AddWithValue("@Title", SessionTitle);
                int count = (int)cmd.ExecuteScalar();

                if (count == 0)
                {
                    cmd.CommandText = insertQuery;
                    cmd.Parameters.AddWithValue("@FilePath", relativePath);
                    cmd.Parameters.AddWithValue("@CreatedBy", teacherId);
                    cmd.ExecuteNonQuery();
                }
            }

            return filePath;
        }

        public void LoadFromSessionData(SessionData session)
        {
            currentSessionData = session;
            SessionTitle = session.Title;
            lblSessionTitle.Text = session.Title;

            listBoxFiles.Items.Clear();
            foreach (var f in session.AttachedFiles)
                listBoxFiles.Items.Add(new ListBoxItem(f.FileName, f.FilePath));

            listBoxAssignments.Items.Clear();
            foreach (var a in session.Assignments)
            {
                var item = new ListBoxItem(a.Title, a.FilePath);
                item.Tag = a.AssignmentType == "MultipleChoice" ? (object)new List<Question>() : a.FilePath;
                listBoxAssignments.Items.Add(item);
            }
        }

        private void OnRenameAssignment(object sender, EventArgs e)
        {
            if (listBoxAssignments.SelectedItem is ListBoxItem item)
            {
                FormRenameAssignment frm = new FormRenameAssignment(item.Display);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    string newName = frm.NewName.Trim();
                    int index = listBoxAssignments.SelectedIndex;
                    item.Display = newName;
                    listBoxAssignments.Items[index] = item;

                    currentSessionData.Assignments[index].Title = newName;
                    SaveSessionToFileText(currentSessionData, sessionFilePath);
                }
            }
        }
        private void OnDeleteAssignment(object sender, EventArgs e)
        {
            if (listBoxAssignments.SelectedItem is ListBoxItem item)
            {
                var confirm = MessageBox.Show("Bạn có chắc muốn xóa bài tập này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    int index = listBoxAssignments.SelectedIndex;
                    listBoxAssignments.Items.RemoveAt(index);
                    currentSessionData.Assignments.RemoveAt(index);
                    SaveSessionToFileText(currentSessionData, sessionFilePath);
                }
            }
        }

        private void listBoxAssignments_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxAssignments.SelectedItem is ListBoxItem item)
            {
                if (item.Tag is List<Question> questions)
                {
                    var form = new FormViewQuestions(questions);
                    form.ShowDialog();
                }
                else if (item.Tag is string filepath && File.Exists(filepath))
                {
                    var confirm = MessageBox.Show("Bạn có muốn mở đề bài?", "Xem file", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(filepath);
                    }
                }
            }
        }
        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ListBoxItem item = new ListBoxItem(dialog.SafeFileName, dialog.FileName);
                listBoxFiles.Items.Add(item);

                currentSessionData.AttachedFiles.Add(new FileItem
                {
                    FileName = dialog.SafeFileName,
                    FilePath = dialog.FileName
                });

                SaveSessionToFileText(currentSessionData, sessionFilePath);
            }
        }

        private void btnCreateAssignment_Click(object sender, EventArgs e)
        {
            FormChooseAssignmentType chooseForm = new FormChooseAssignmentType();
            if (chooseForm.ShowDialog() == DialogResult.OK)
            {
                string assignmentTitle = "Bài tập " + (listBoxAssignments.Items.Count + 1);

                if (chooseForm.SelectedType == FormChooseAssignmentType.AssignmentType.MultipleChoice)
                {
                    FormSetupQuiz setup = new FormSetupQuiz();
                    if (setup.ShowDialog() == DialogResult.OK)
                    {
                        FormCreateQuizQuestions createQuiz = new FormCreateQuizQuestions(setup.QuestionCount);
                        if (createQuiz.ShowDialog() == DialogResult.OK)
                        {
                            List<Question> questions = createQuiz.CreatedQuestions;
                            var item = new ListBoxItem($"{assignmentTitle} (Trắc nghiệm - {setup.Duration} phút)", "");
                            item.Tag = questions;
                            listBoxAssignments.Items.Add(item);

                            currentSessionData.Assignments.Add(new AssignmentData
                            {
                                Title = item.Display,
                                AssignmentType = "MultipleChoice"
                            });

                            SaveSessionToFileText(currentSessionData, sessionFilePath);
                        }
                    }
                }
                else if (chooseForm.SelectedType == FormChooseAssignmentType.AssignmentType.Essay)
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string filename = Path.GetFileName(dialog.FileName);
                        var item = new ListBoxItem($"{assignmentTitle} (Tự luận)", filename);
                        item.Tag = dialog.FileName;
                        listBoxAssignments.Items.Add(item);

                        currentSessionData.Assignments.Add(new AssignmentData
                        {
                            Title = item.Display,
                            FilePath = dialog.FileName,
                            AssignmentType = "Essay"
                        });

                        SaveSessionToFileText(currentSessionData, sessionFilePath);
                    }
                }
            }
        }

        private void SaveSessionToFileText(SessionData session, string filePath)
        {
            var lines = new List<string>
            {
                $"SessionTitle: {session.Title}",
                "AttachedFiles:"
            };

            foreach (var f in session.AttachedFiles)
                lines.Add($"- {f.FileName}|{f.FilePath}");

            lines.Add("Assignments:");
            foreach (var a in session.Assignments)
                lines.Add($"- {a.Title}|{a.AssignmentType}|{a.FilePath}");

            File.WriteAllLines(filePath, lines);
        }
    }

    public class ListBoxItem
    {
        public string Display { get; set; }
        public string FilePath { get; set; }
        public object Tag { get; set; }

        public ListBoxItem(string display, string path)
        {
            Display = display;
            FilePath = path;
        }

        public override string ToString()
        {
            return Display;
        }
    }
}
