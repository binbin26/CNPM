using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNPM.Models.Courses;
using System.Windows.Forms;

namespace CNPM.Forms.Teacher
{
    public partial class UcCourseDetail : UserControl
    {
        private int sessionCount = 1;

        private string courseName;
        public string CourseName
        {
            get => courseName;
            set
            {
                courseName = value;
                lblCourseName.Text = courseName;
                LoadSessions(); // Load lại buổi học khi gán tên khóa học
            }
        }

        public UcCourseDetail()
        {
            InitializeComponent();
        }

        private void btnAddSession_Click(object sender, EventArgs e)
        {
            string title = $"Buổi {sessionCount++}";
            var ucSession = new UcSessionItem(title);
            ucSession.Width = flowPanelSessions.Width - 30;
            flowPanelSessions.Controls.Add(ucSession);
        }

        private void LoadSessions()
        {
            flowPanelSessions.Controls.Clear();
            sessionCount = 1;

            string folder = Path.Combine(Application.StartupPath, "Sessions");
            if (!Directory.Exists(folder)) return;

            var files = Directory.GetFiles(folder, "*.txt");

            foreach (var file in files)
            {
                var session = LoadSessionFromFileText(file);
                if (session != null && session.Title.StartsWith("Buổi"))
                {
                    var uc = new UcSessionItem(session.Title);
                    uc.LoadFromSessionData(session); // Gọi hàm bên trong UcSessionItem
                    uc.Width = flowPanelSessions.Width - 30;
                    flowPanelSessions.Controls.Add(uc);

                    // Tìm sessionCount lớn nhất
                    string numberPart = session.Title.Split(' ')[1];
                    if (int.TryParse(numberPart, out int n) && n >= sessionCount)
                        sessionCount = n + 1;
                }
            }
        }

        private SessionData LoadSessionFromFileText(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            var lines = File.ReadAllLines(filePath);
            SessionData session = new SessionData();

            int i = 0;
            if (lines.Length > 0 && lines[0].StartsWith("SessionTitle: "))
                session.Title = lines[0].Substring("SessionTitle: ".Length).Trim();

            i = 1;
            while (i < lines.Length && lines[i].Trim() != "AttachedFiles:")
                i++;

            i++;
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
                            session.Assignments.Add(new AssignmentData
                            {
                                Title = parts[0],
                                AssignmentType = parts[1],
                                FilePath = parts.Length > 2 ? parts[2] : null
                            });
                        }
                    }
                    i++;
                }
            }

            return session;
        }
    }
}
