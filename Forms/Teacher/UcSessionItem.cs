using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog.Targets;

namespace CNPM.Forms.Teacher
{
    public partial class UcSessionItem : UserControl
    {
        private SessionData currentSessionData;
        public string SessionTitle { get; set; }

        public UcSessionItem(string title)
        {
            InitializeComponent();
            SessionTitle = title;
            lblSessionTitle.Text = title;
            LoadSession();

            currentSessionData = new SessionData
            {
                Title = title
            };
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

                SaveSession();
            }
        }
        private void listBoxAssignments_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxAssignments.SelectedItem is ListBoxItem item)
            {
                // Nếu là bài trắc nghiệm
                if (item.Tag is List<Question> questions)
                {
                    var form = new FormViewQuestions(questions);
                    form.ShowDialog();
                }
                // Nếu là bài tự luận
                else if (item.Tag is string filepath && File.Exists(filepath))
                {
                    var confirm = MessageBox.Show("Bạn có muốn mở đề bài?", "Xem file", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(filepath); // Mở bằng app mặc định
                    }
                }
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
                    SaveSession();
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
                    SaveSession();
                }
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
                            item.Tag = questions; // 👈 Gắn danh sách câu hỏi vào đây
                            listBoxAssignments.Items.Add(item);

                            // ✅ Cập nhật sessionData
                            currentSessionData.Assignments.Add(new AssignmentData
                            {
                                Title = item.Display,
                                FilePath = "", // Không cần file path cho trắc nghiệm demo
                                AssignmentType = "MultipleChoice"
                            });

                            SaveSession(); // ✅ Gọi lưu
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
                        item.Tag = dialog.FileName; // 👈 Gắn đường dẫn thật
                        listBoxAssignments.Items.Add(item);

                        // ✅ Cập nhật sessionData
                        currentSessionData.Assignments.Add(new AssignmentData
                        {
                            Title = item.Display,
                            FilePath = dialog.FileName,
                            AssignmentType = "Essay"
                        });

                        SaveSession(); // ✅ Gọi lưu
                    }
                }

                // Đẩy các nút xuống cuối cùng
                Controls.SetChildIndex(listBoxAssignments, 0);
                Controls.SetChildIndex(btnCreateAssignment, Controls.Count - 1);
                Controls.SetChildIndex(btnAttachFile, Controls.Count - 1);
            }
            currentSessionData.Assignments.Clear();
            foreach (ListBoxItem item in listBoxAssignments.Items)
            {
                string type = "Essay";
                if (item.Tag is List<Question>)
                    type = "MultipleChoice";

                currentSessionData.Assignments.Add(new AssignmentData
                {
                    Title = item.Display,
                    FilePath = item.FilePath,
                    AssignmentType = type
                });
            }

            SaveSession();
        }
        private void SaveSession()
        {
            try
            {
                string safeTitle = string.Concat(SessionTitle.Split(System.IO.Path.GetInvalidFileNameChars()));
                string folder = System.IO.Path.Combine(Application.StartupPath, "Sessions");
                if (!System.IO.Directory.Exists(folder))
                    System.IO.Directory.CreateDirectory(folder);

                string filePath = System.IO.Path.Combine(folder, safeTitle + ".txt");

                SaveSessionToFileText(currentSessionData, filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu buổi học: " + ex.Message);
            }
        }

        private void LoadSession()
        {
            try
            {
                string safeTitle = string.Concat(SessionTitle.Split(System.IO.Path.GetInvalidFileNameChars()));
                string folder = System.IO.Path.Combine(Application.StartupPath, "Sessions");
                string filePath = System.IO.Path.Combine(folder, safeTitle + ".txt");

                var session = LoadSessionFromFileText(filePath);
                if (session != null)
                {
                    currentSessionData = session;
                    lblSessionTitle.Text = session.Title;

                    listBoxFiles.Items.Clear();
                    foreach (var f in session.AttachedFiles)
                    {
                        listBoxFiles.Items.Add(new ListBoxItem(f.FileName, f.FilePath));
                    }
                    listBoxAssignments.Items.Clear();
                    foreach (var a in session.Assignments)
                    {
                        var item = new ListBoxItem(a.Title, a.FilePath);
                        item.Tag = a.AssignmentType == "MultipleChoice" ? (object)new List<Question>() : null; // Tạm gán List<Question> trống nếu trắc nghiệm
                        listBoxAssignments.Items.Add(item);
                    }
                }
                else
                {
                    // Khởi tạo mới nếu chưa có file
                    currentSessionData = new SessionData { Title = SessionTitle };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu buổi học: " + ex.Message);
            }
        }
        public class ListBoxItem
        {
            public string Display { get; set; }
            public string FilePath { get; set; }
            public object Tag { get; set; } // ✨ Cho phép gắn dữ liệu bất kỳ, như List<Question>

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
        public void LoadFromSessionData(SessionData session)
        {
            currentSessionData = session;
            lblSessionTitle.Text = session.Title;

            listBoxFiles.Items.Clear();
            foreach (var f in session.AttachedFiles)
                listBoxFiles.Items.Add(new ListBoxItem(f.FileName, f.FilePath));

            listBoxAssignments.Items.Clear();
            foreach (var a in session.Assignments)
            {
                var item = new ListBoxItem(a.Title, a.FilePath);

                // Gắn Tag để xử lý khi double click
                if (a.AssignmentType == "MultipleChoice")
                    item.Tag = new List<Question>(); // gán danh sách trống tạm thời
                else if (a.AssignmentType == "Essay")
                    item.Tag = a.FilePath;

                listBoxAssignments.Items.Add(item);
            }
        }
        private void SaveSessionToFileText(SessionData session, string filePath)
        {
            var lines = new List<string>();

            lines.Add($"SessionTitle: {session.Title}");
            lines.Add("AttachedFiles:");
            foreach (var f in session.AttachedFiles)
            {
                lines.Add($" - {f.FileName}|{f.FilePath}");
            }

            lines.Add("Assignments:");
            foreach (var a in session.Assignments)
            {
                lines.Add($" - {a.Title} ({a.AssignmentType})");
            }

            System.IO.File.WriteAllLines(filePath, lines);
        }
        private SessionData LoadSessionFromFileText(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                return null;

            var lines = System.IO.File.ReadAllLines(filePath);
            SessionData session = new SessionData();

            int i = 0;

            // Đọc tiêu đề buổi học
            if (lines.Length > 0 && lines[0].StartsWith("SessionTitle: "))
                session.Title = lines[0].Substring("SessionTitle: ".Length).Trim();

            // Đọc danh sách file đính kèm
            i = 1;
            while (i < lines.Length && lines[i].Trim() != "AttachedFiles:")
                i++;

            i++; // qua dòng AttachedFiles:
            while (i < lines.Length && !lines[i].StartsWith("Assignments:"))
            {
                var line = lines[i].Trim();
                if (line.StartsWith("- "))
                {
                    var parts = line.Substring(2).Split('|');
                    if (parts.Length >= 2)
                    {
                        session.AttachedFiles.Add(new FileItem
                        {
                            FileName = parts[0],
                            FilePath = parts[1]
                        });
                    }
                }
                i++;
            }

            // Đọc danh sách bài tập
            if (i < lines.Length && lines[i].Trim() == "Assignments:")
            {
                i++;
                while (i < lines.Length)
                {
                    var line = lines[i].Trim();
                    if (line.StartsWith("- "))
                    {
                        var parts = line.Substring(2).Split('|');
                        if (parts.Length >= 2)
                        {
                            var assignment = new AssignmentData
                            {
                                Title = parts[0],
                                AssignmentType = parts[1],
                                FilePath = parts.Length > 2 ? parts[2] : null
                            };
                            session.Assignments.Add(assignment);
                        }
                    }
                    i++;
                }
            }

            return session;
        }
    }
}
