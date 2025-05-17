using CNPM.DAL;
using CNPM.Models.Courses.Sessions;
using System.Collections.Generic;

namespace CNPM.BLL
{
    public class SessionBLL
    {
        private readonly SessionDAL sessionDAL;

        public SessionBLL()
        {
            sessionDAL = new SessionDAL();
        }

        public List<SessionData> GetAllSessions()
        {
            return sessionDAL.GetAllSessions();
        }
    }
}
