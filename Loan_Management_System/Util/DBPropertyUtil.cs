using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System.Util
{
    internal class DBPropertyUtil
    {
        public static string GetConnectionString(string fileName)
        {
            // Logic to read connection string from a property file
            return File.ReadAllText(fileName);
        }
    }
}
