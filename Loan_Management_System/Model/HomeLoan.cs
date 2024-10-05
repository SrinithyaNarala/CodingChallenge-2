using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System.Model
{
    internal class HomeLoan : Loan
    {
        private int v1;
        private string v2;
        private string v3;

        public string PropertyAddress { get; set; }
        public int PropertyValue { get; set; }

        // Constructor
        public HomeLoan(int loanId, Customer customer, decimal principalAmount, decimal interestRate, int loanTerm, string loanType, string loanStatus, string propertyAddress, int propertyValue)
            : base(loanId, customer, principalAmount, interestRate, loanTerm, loanType, loanStatus)
        {
            PropertyAddress = propertyAddress;
            PropertyValue = propertyValue;
        }

        public HomeLoan()
        {
        }

        public HomeLoan(int v1, int customerId, decimal principalAmount, decimal interestRate, int loanTerm, string v2, string v3, string? propertyAddress, int propertyValue)
        {
            this.v1 = v1;
            CustomerId = customerId;
            PrincipalAmount = principalAmount;
            InterestRate = interestRate;
            LoanTerm = loanTerm;
            this.v2 = v2;
            this.v3 = v3;
            PropertyAddress = propertyAddress;
            PropertyValue = propertyValue;
        }

        public override void PrintSpecificDetails()
        {
            Console.WriteLine($"Property Address: {PropertyAddress}, Property Value: {PropertyValue}");
        }
    }
}
