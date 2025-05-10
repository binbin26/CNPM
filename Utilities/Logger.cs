using System;
using System.IO;

namespace CNPM.Utilities
{
    public static class Logger
    {
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
                // Kiểm tra và xử lý errorMessage nếu null/rỗng
                string safeErrorMessage = string.IsNullOrEmpty(errorMessage)
                                        ? "Lỗi không xác định."
                                        : errorMessage;

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
        public static void LogInfo(string message)
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string logDirectory = Path.Combine(basePath, "logs");
                string logFilePath = Path.Combine(logDirectory, $"log_{DateTime.Now:yyyyMMdd}.log");

                Directory.CreateDirectory(logDirectory);

                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] INFO: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Không thể ghi log: {ex.Message}");
            }
        }

    }
}