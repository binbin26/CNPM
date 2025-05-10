using System;
using System.IO;

namespace CNPM.Utilities
{
    public static class Logger
    {
        private static string LogFilePath =>
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "logs",
        $"log_{DateTime.Now:yyyyMMdd}.log"
    );
        /// <summary>
        /// Ghi log lỗi vào file
        /// </summary>
        public static void LogError(string errorMessage)
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string logDirectory = Path.Combine(basePath, "logs");
                string logFilePath = Path.Combine(logDirectory, $"log_{DateTime.Now:yyyyMMdd}.log");

                // Tạo thư mục nếu chưa tồn tại
                Directory.CreateDirectory(logDirectory);

                // Ghi log vào file
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                // Fallback: Hiển thị lỗi trên console nếu không ghi được log
                Console.WriteLine($"Không thể ghi log: {ex.Message}");
            }
        }
    }
}