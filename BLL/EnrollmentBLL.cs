using CNPM.DAL;
using CNPM.Models;
using System.Collections.Generic;

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
