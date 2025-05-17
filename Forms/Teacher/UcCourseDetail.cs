using System;
using System.Windows.Forms;
using CNPM.BLL;
using System.Linq;

namespace CNPM.Forms.Teacher
{
    public partial class UcCourseDetail : UserControl
    {
        private int sessionCount = 1;
        private readonly SessionBLL sessionBLL = new SessionBLL();

        private string courseName;
        public string CourseName
        {
            get => courseName;
            set
            {
                courseName = value;
                lblCourseName.Text = courseName;
                LoadSessions();
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

            var allSessions = sessionBLL.GetAllSessions();

            var courseSessions = allSessions
                .Where(s => !string.IsNullOrEmpty(s.Title) && s.Title.StartsWith("Buổi"))
                .ToList();

            foreach (var session in courseSessions)
            {
                var uc = new UcSessionItem(session.Title);
                uc.LoadFromSessionData(session); //UcSessiongItem
                uc.Width = flowPanelSessions.Width - 30;
                flowPanelSessions.Controls.Add(uc);

                string numberPart = session.Title.Split(' ')[1];
                if (int.TryParse(numberPart, out int n) && n >= sessionCount)
                    sessionCount = n + 1;
            }
        }
    }
}
