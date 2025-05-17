using System;
using System.Windows.Forms;
using CNPM.DAL;
using System.Data.SqlClient;
using CNPM.Models.Courses;
using System.IO;
using CNPM.Models.Courses.Sessions;

namespace CNPM.Forms.Teacher
{
    public partial class UcCourseDetail : UserControl
    {
        private int sessionCount = 1;
        public Course CurrentCourse { get; set; }
        public int TeacherID { get; set; } = 10;

        public UcCourseDetail()
        {
            InitializeComponent();
        }

        private void btnAddSession_Click(object sender, EventArgs e)
        {
            var title = "Buổi " + sessionCount++;
            var ucSession = new UcSessionItem(title, CurrentCourse.CourseID, TeacherID);
            ucSession.Width = flowPanelSessions.Width - 30;
            flowPanelSessions.Controls.Add(ucSession);
        }

        public void LoadSessionsFromDatabase()
        {
            flowPanelSessions.Controls.Clear();
            sessionCount = 1;

            string query = "SELECT Title, FilePath FROM Sessions WHERE CourseID = @CourseID ORDER BY CreatedAt";
            using (var conn = DatabaseHelper.GetConnection())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CourseID", CurrentCourse.CourseID);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string title = reader.GetString(0);
                        string relativePath = reader.GetString(1);
                        string filePath = Path.Combine(Application.StartupPath, relativePath);

                        if (File.Exists(filePath))
                        {
                            SessionData session = LoadSessionFromFileText(filePath);
                            var uc = new UcSessionItem(session.Title, CurrentCourse.CourseID, TeacherID);
                            uc.Width = flowPanelSessions.Width - 30;
                            uc.LoadFromSessionData(session);
                            flowPanelSessions.Controls.Add(uc);

                            if (session.Title.StartsWith("Buổi "))
                            {
                                var parts = session.Title.Split(' ');
                                if (parts.Length >= 2 && int.TryParse(parts[1], out int n) && n >= sessionCount)
                                    sessionCount = n + 1;
                            }
                        }
                    }
                }
            }
        }

        private SessionData LoadSessionFromFileText(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            var lines = File.ReadAllLines(filePath);
            SessionData session = new SessionData();
            int i = 0;

            if (lines.Length > 0 && lines[0].StartsWith("SessionTitle: "))
                session.Title = lines[0].Substring("SessionTitle: ".Length).Trim();

            i = 1;
            while (i < lines.Length && lines[i].Trim() != "AttachedFiles:") i++;
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

        public void SetCourse(Course course, int teacherId)
        {
            this.CurrentCourse = course;
            this.TeacherID = teacherId;
            lblCourseName.Text = course.CourseName;
            LoadSessionsFromDatabase();
        }
    }
}
