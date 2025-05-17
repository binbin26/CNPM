using CNPM.Models.Courses;
using CNPM.Models.Users;
using System.Data.Entity;

namespace CNPM.DAL
{
    public class EduMasterDbContext : DbContext
    {
        // Map các bảng
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }

        // Cấu hình connection string
        public EduMasterDbContext() : base("name=EduMasterDB") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Cấu hình ánh xạ nâng cao (nếu cần)
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
