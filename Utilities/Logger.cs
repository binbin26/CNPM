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
        public static void LogError(string message)
        {
            Log("ERROR", message);
        }

        /// <summary>
        /// Ghi log thông tin
        /// </summary>
        public static void LogInfo(string message)
        {
            Log("INFO", message);
        }

        private static void Log(string logLevel, string message)
        {
            try
            {
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {message}{Environment.NewLine}";

                // Đảm bảo thread-safe khi ghi file
                lock (typeof(Logger))
                {
                    File.AppendAllText(LogFilePath, logMessage);
                }
            }
            catch (Exception ex)
            {
                // Xử lý exception nếu không ghi được log (ví dụ: ghi vào console)
                Console.WriteLine($"Không thể ghi log: {ex.Message}");
            }
        }
    }
}