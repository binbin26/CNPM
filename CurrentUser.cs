using CNPM.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM
{
    public interface IUserContext
    {
        User CurrentUser { get; set; }
    }
    public class UserContext : IUserContext
    {
        public User CurrentUser { get; set; }
    }
}
