using System;
using System.Data;
using System.Data.SqlClient;

public static class DataHelper
{
    private static string connectionString = @"Data Source=YOUR_SERVER_NAME;Initial Catalog=YOUR_DATABASE_NAME;Integrated Security=True";

    public static SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }
} 