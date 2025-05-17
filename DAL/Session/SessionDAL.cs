using System;
using System.Collections.Generic;
using System.IO;
using CNPM.Models.Courses.Sessions;

namespace CNPM.DAL
{
    public class SessionDAL
    {
        private readonly string sessionFolderPath;

        public SessionDAL()
        {
            sessionFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sessions");

            if (!Directory.Exists(sessionFolderPath))
            {
                Directory.CreateDirectory(sessionFolderPath);
            }
        }

        public List<SessionData> GetAllSessions()
        {
            var sessions = new List<SessionData>();

            var files = Directory.GetFiles(sessionFolderPath, "*.txt");
            foreach (var file in files)
            {
                var session = LoadSessionFromFile(file);
                if (session != null)
                {
                    sessions.Add(session);
                }
            }

            return sessions;
        }

        public SessionData LoadSessionFromFile(string filePath)
        {
            if (!File.Exists(filePath)) return null;

            var lines = File.ReadAllLines(filePath);
            var session = new SessionData();

            int i = 0;
            if (lines.Length > 0 && lines[0].StartsWith("SessionTitle: "))
                session.Title = lines[0].Substring("SessionTitle: ".Length).Trim();
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
    }
}
