using System.Data.SqlClient;

namespace CNPM.DAL
{
    public class PermissionDAL
    {
        public bool UserHasPermission(int userID, string permissionType)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = @"
                SELECT 1 
                FROM Users u
                JOIN Permissions p ON u.Role = p.Role
                WHERE u.UserID = @UserID 
                AND p." + permissionType + " = 1";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userID);

                conn.Open();
                return (cmd.ExecuteScalar() != null);
            }
        }
    }
}
