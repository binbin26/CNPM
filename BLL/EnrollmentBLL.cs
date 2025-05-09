using CNPM.DAL;
using CNPM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.BLL
{
    public class EnrollmentBLL
    {
        private readonly EnrollmentDAL _enrollmentDAL = new EnrollmentDAL();

        public List<EnrolledStudent> GetEnrolledStudents(int courseID)
        {
            return _enrollmentDAL.GetEnrolledStudents(courseID);
        }
    }
}
