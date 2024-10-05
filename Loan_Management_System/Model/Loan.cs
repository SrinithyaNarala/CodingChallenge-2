using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System.Model
{
    internal abstract class Loan
    {
        public int LoanId { get; set; }
        public Customer Customer { get; set; }  // Reference to Customer class
        public int CustomerId { get; set; }      // Customer ID as int
        public decimal PrincipalAmount { get; set; }
        public decimal InterestRate { get; set; }
        public int LoanTerm { get; set; }
        public string LoanType { get; set; }
        public string LoanStatus { get; set; }

        // Default Constructor
        protected Loan()
        {
            // Initialize with default values if needed
        }

        // Parameterized Constructor
        public Loan(int loanId, Customer customer, decimal principal, decimal interestRate, int loanTerm, string loanType, string loanStatus)
        {
            LoanId = loanId;
            Customer = customer;
            CustomerId = customer.CustomerId;  // Assuming Customer class has CustomerId property
            PrincipalAmount = principal;
            InterestRate = interestRate;
            LoanTerm = loanTerm;
            LoanType = loanType;
            LoanStatus = loanStatus;
        }

        public void PrintLoanInfo()
        {
            string customerName = Customer != null ? Customer.Name : "Unknown";
            Console.WriteLine($"Loan ID: {LoanId}, Customer: {customerName}, Principal: {PrincipalAmount}, Interest Rate: {InterestRate}, Term: {LoanTerm}, Type: {LoanType}, Status: {LoanStatus}");
        }

        // Abstract methods to be implemented in subclasses
        public abstract void PrintSpecificDetails();
    }
}
