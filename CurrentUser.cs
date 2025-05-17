using CNPM.Models.Users;

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
