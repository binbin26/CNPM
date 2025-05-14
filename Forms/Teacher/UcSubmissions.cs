using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            // Demo dữ liệu môn học
            List<string> courses = new List<string> { "Lập trình C#", "Cơ sở dữ liệu", "Phân tích hệ thống" };

            foreach (var course in courses)
            {
                Button btn = new Button();
                btn.Text = course;
                btn.Height = 50;
                btn.Dock = DockStyle.Top;
                btn.Tag = course;
                btn.Click += Course_Click;
                flowPanelCourses.Controls.Add(btn);
            }

            // Demo dữ liệu bài tập (chỉ tự luận)
            courseAssignments = new Dictionary<string, List<Assignment>>
            {
                { "Lập trình C#", new List<Assignment> {
                    new Assignment("Bài 1", new List<StudentSubmission> {
                        new StudentSubmission("Nguyễn Văn A", @"C:\FakePath\NVA_De1.docx"),
                        new StudentSubmission("Trần Thị B", @"C:\FakePath\TTB_De1.docx")
                    })
                }}
            };
        }

        private void Course_Click(object sender, EventArgs e)
        {
            panelAssignments.Controls.Clear();

            string course = ((Button)sender).Tag.ToString();
            if (courseAssignments.ContainsKey(course))
            {
                foreach (var assignment in courseAssignments[course])
                {
                    var lbl = new Label
                    {
                        Text = assignment.Title,
                        Height = 40,
                        Padding = new Padding(10),
                        BorderStyle = BorderStyle.FixedSingle,
                        Tag = assignment
                    };
                    lbl.Click += Assignment_Click;
                    panelAssignments.Controls.Add(lbl);
                }
            }

            panelStudents.Controls.Clear();
        }

        private void Assignment_Click(object sender, EventArgs e)
        {
            panelStudents.Controls.Clear();

            if (((Label)sender).Tag is Assignment assignment)
            {
                foreach (var sub in assignment.Submissions)
                {
                    var btn = new Button
                    {
                        Text = $"{sub.StudentName} - {System.IO.Path.GetFileName(sub.FilePath)}",
                        Height = 35,
                        Dock = DockStyle.Top,
                        Tag = sub
                    };
                    btn.Click += Submission_Click;
                    panelStudents.Controls.Add(btn);
                }
            }
        }

        private void Submission_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Tag is StudentSubmission sub)
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    FileName = System.IO.Path.GetFileName(sub.FilePath)
                };
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Demo: bạn cần copy từ file thực, ở đây mình giả lập
                    System.IO.File.Copy(sub.FilePath, saveDialog.FileName, true);
                    MessageBox.Show("Đã lưu bài nộp.");
                }
            }
        }
    }

    public class Assignment
    {
        public string Title { get; set; }
        public List<StudentSubmission> Submissions { get; set; }

        public Assignment(string title, List<StudentSubmission> submissions)
        {
            Title = title;
            Submissions = submissions;
        }
    }

    public class StudentSubmission
    {
        public string StudentName { get; set; }
        public string FilePath { get; set; }

        public StudentSubmission(string name, string path)
        {
            StudentName = name;
            FilePath = path;
        }
    }
}
