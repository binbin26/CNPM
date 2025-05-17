using CNPM.DAL;
using CNPM.Forms.Auth;
using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using CNPM.BLL;

namespace CNPM
{
    static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; } // ✅ thêm
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // ✅ Khởi tạo DI container
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
            if (!DatabaseHelper.TestConnection())
            {
                MessageBox.Show("Không thể kết nối đến database!");
                return;
            }
            Application.Run(new MainForm());

        }
        // ✅ Thêm method để đăng ký các dịch vụ
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUserContext, UserContext>();
            services.AddTransient<CourseDAL>();
            services.AddTransient<CourseBLL>();
            // Đăng ký các service khác nếu cần
        }
    }
}

