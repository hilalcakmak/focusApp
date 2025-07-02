using System;
using Dapper;
using System.Data.SqlClient;
namespace focusApp.Models
{
    public class DP
    {
        public static string connectionString = "Server=localhost; Database=FocusDB; User Id=SA; Password=123; TrustServerCertificate=True;";

        public static void ExecuteReturn(string procadi, DynamicParameters param = null)
        {
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                db.Execute(procadi, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public static IEnumerable<T> Listeleme<T>(string procadi, DynamicParameters param = null)
        {
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                return db.Query<T>(procadi, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}

