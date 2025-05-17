using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class UcSubmissions : UserControl
    {
        private Dictionary<string, List<Assignment>> courseAssignments;

        public UcSubmissions()
        {
            InitializeComponent();
            LoadCourses();
        }

        private void LoadCourses()
        {
            List<string> courses = new List<string> { "Lập trình C#", "Cơ sở dữ liệu" };

            foreach (var course in courses)
            {
                Panel pnl = new Panel
                {
                    Height = 60,
                    Width = flowPanelCourses.Width - 25,
                    BackColor = Color.LightGray,
                    Margin = new Padding(5),
                    Cursor = Cursors.Hand,
                    Tag = course
                };

                Label lbl = new Label
                {
                    Text = course,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(10),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Tag = course
                };

                pnl.Click += Course_Click;
                lbl.Click += Course_Click;

                pnl.Controls.Add(lbl);
                flowPanelCourses.Controls.Add(pnl);
            }

            // Demo bài tập với cả 2 loại
            courseAssignments = new Dictionary<string, List<Assignment>>
            {
                {
                    "Lập trình C#",
                    new List<Assignment>
                    {
                        new Assignment("BT Trắc nghiệm 1", AssignmentType.MultipleChoice,
                            new List<StudentSubmission>
                            {
                                new StudentSubmission("Nguyễn Văn A", selectedAnswers: new List<string>{"A","B","C"}),
                                new StudentSubmission("Trần Thị B", selectedAnswers: new List<string>{"B","B","D"})
                            },
                            new List<Question>
                            {
                                new Question{Content="Câu 1?", OptionA="A1", OptionB="B1", OptionC="C1", OptionD="D1", CorrectAnswer="A"},
                                new Question{Content="Câu 2?", OptionA="A2", OptionB="B2", OptionC="C2", OptionD="D2", CorrectAnswer="B"},
                                new Question{Content="Câu 3?", OptionA="A3", OptionB="B3", OptionC="C3", OptionD="D3", CorrectAnswer="C"}
                            }
                        ),
                        new Assignment("BT Tự luận 1", AssignmentType.Essay,
                            new List<StudentSubmission>
                            {
                                new StudentSubmission("Nguyễn Văn A", @"C:\FakePath\BT_TuLuan_NVA.docx"),
                                new StudentSubmission("Trần Thị B", @"C:\FakePath\BT_TuLuan_TTB.docx")
                            })
                    }
                },
                {
                    "Cơ sở dữ liệu",
                    new List<Assignment>
                    {
                        new Assignment("BT Trắc nghiệm SQL", AssignmentType.MultipleChoice,
                            new List<StudentSubmission>
                            {
                                new StudentSubmission("Lê Văn C", selectedAnswers: new List<string>{"C","B","A"}),
                                new StudentSubmission("Phạm Thị D", selectedAnswers: new List<string>{"D","C","B"})
                            },
                            new List<Question>
                            {
                                new Question{Content="Câu SQL 1?", OptionA="A", OptionB="B", OptionC="C", OptionD="D", CorrectAnswer="C"},
                                new Question{Content="Câu SQL 2?", OptionA="A", OptionB="B", OptionC="C", OptionD="D", CorrectAnswer="B"},
                                new Question{Content="Câu SQL 3?", OptionA="A", OptionB="B", OptionC="C", OptionD="D", CorrectAnswer="A"}
                            }
                        )
                    }
                }
            };
        }

        private void Course_Click(object sender, EventArgs e)
        {
            panelStudents.Controls.Clear();
            flowPanelMCQAssignments.Controls.Clear();
            flowPanelEssayAssignments.Controls.Clear();

            Control clicked = sender as Control;
            string course = clicked.Tag as string ?? clicked.Parent?.Tag as string;

            if (string.IsNullOrEmpty(course))
            {
                MessageBox.Show("Không lấy được tên môn học.");
                return;
            }

            if (courseAssignments.ContainsKey(course))
            {
                foreach (var assignment in courseAssignments[course])
                {
                    Label lbl = new Label
                    {
                        Text = assignment.Title,
                        Height = 40,
                        AutoSize = false,
                        Width = flowPanelMCQAssignments.Width - 25,
                        Padding = new Padding(10),
                        Margin = new Padding(5),
                        BorderStyle = BorderStyle.FixedSingle,
                        Tag = assignment,
                        Cursor = Cursors.Hand
                    };

                    lbl.Click += Assignment_Click;

                    if (assignment.Type == AssignmentType.MultipleChoice)
                        flowPanelMCQAssignments.Controls.Add(lbl);
                    else if (assignment.Type == AssignmentType.Essay)
                        flowPanelEssayAssignments.Controls.Add(lbl);
                }
            }
        }

        private void Assignment_Click(object sender, EventArgs e)
        {
            panelStudents.Controls.Clear();

            if ((sender as Control).Tag is Assignment assignment)
            {
                foreach (var sub in assignment.Submissions)
                {
                    Button btn = new Button
                    {
                        Text = sub.StudentName,
                        AutoSize = false,
                        Height = 35,
                        Width = panelStudents.Width - 25,
                        Margin = new Padding(5),
                        Tag = new Tuple<Assignment, StudentSubmission>(assignment, sub),
                        Cursor = Cursors.Hand
                    };

                    btn.Click += Submission_Click;

                    panelStudents.Controls.Add(btn);
                }
            }
        }

        private void Submission_Click(object sender, EventArgs e)
        {
            var tag = (sender as Control).Tag as Tuple<Assignment, StudentSubmission>;
            if (tag == null) return;

            var assignment = tag.Item1;
            var submission = tag.Item2;

            if (assignment.Type == AssignmentType.MultipleChoice)
            {
                // Mở form xem bài tập trắc nghiệm của sinh viên
                var formView = new FormViewStudentQuiz(assignment.Questions, submission.SelectedAnswers);
                formView.ShowDialog();
            }
            else if (assignment.Type == AssignmentType.Essay)
            {
                // Hiển thị SaveFileDialog để lưu file bài nộp tự luận
                if (File.Exists(submission.FilePath))
                {
                    SaveFileDialog dlg = new SaveFileDialog
                    {
                        FileName = Path.GetFileName(submission.FilePath)
                    };
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        File.Copy(submission.FilePath, dlg.FileName, true);
                        MessageBox.Show("Đã lưu bài nộp.");
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy file bài nộp.");
                }
            }
        }
    }

    public enum AssignmentType
    {
        MultipleChoice,
        Essay
    }

    public class Assignment
    {
        public string Title { get; set; }
        public AssignmentType Type { get; set; }
        public List<StudentSubmission> Submissions { get; set; }
        public List<Question> Questions { get; set; } // Dùng cho trắc nghiệm

        public Assignment(string title, AssignmentType type, List<StudentSubmission> submissions = null, List<Question> questions = null)
        {
            Title = title;
            Type = type;
            Submissions = submissions ?? new List<StudentSubmission>();
            Questions = questions;
        }
    }

    public class StudentSubmission
    {
        public string StudentName { get; set; }
        public string FilePath { get; set; } // Dùng cho tự luận

        // Dữ liệu bài làm trắc nghiệm
        public List<string> SelectedAnswers { get; set; } // Ví dụ: ["A", "C", "B"]

        public StudentSubmission(string name, string path = null, List<string> selectedAnswers = null)
        {
            StudentName = name;
            FilePath = path;
            SelectedAnswers = selectedAnswers;
        }
    }
}
