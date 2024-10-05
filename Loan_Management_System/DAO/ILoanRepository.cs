using Loan_Management_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System.DAO
{
    internal interface ILoanRepository
    {
        void ApplyLoan(Loan loan);  // Apply for a loan
        decimal CalculateInterest(int loanId);  // Calculate interest by loanId
        decimal CalculateInterest(decimal principal, decimal interestRate, int term);  // Overloaded method for interest
        void LoanStatus(int loanId);  // Approve or reject loan based on credit score
        decimal CalculateEMI(int loanId);  // Calculate EMI by loanId
        decimal CalculateEMI(decimal principal, decimal interestRate, int term);  // Overloaded method for EMI
        void LoanRepayment(int loanId, decimal amount);  // Handle repayment
        List<Loan> GetAllLoans();  // Get all loans
        Loan GetLoanById(int loanId);  // Get loan by loanId
        Customer GetCustomerById(int customerId);
    }
}

