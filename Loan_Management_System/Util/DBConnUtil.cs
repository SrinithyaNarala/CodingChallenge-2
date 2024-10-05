using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System.Util
{
    internal class DBConnUtil
    {
        public static SqlConnection GetDBConn(string connectionString)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect to database: " + ex.Message);
                throw;
            }
        }

        internal static string? GetConnString()
        {
            throw new NotImplementedException();
        }

        internal static string GetDBConn()
        {
            return "Server=LAPTOP-I82QA3KA;Database=Loan_Management;Trusted_Connection=True"; 
        }
    }
}
