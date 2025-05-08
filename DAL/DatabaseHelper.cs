using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace CNPM.DAL
{
    public static class DatabaseHelper
    {
        public static string ConnectionString =>
        ConfigurationManager.ConnectionStrings["EduMasterDB"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return (conn.State == ConnectionState.Open);
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
