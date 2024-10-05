using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System.Exceptions
{
    internal class InvalidLoanException : Exception
    {
        public InvalidLoanException(string message) : base(message) { }
    }
}
