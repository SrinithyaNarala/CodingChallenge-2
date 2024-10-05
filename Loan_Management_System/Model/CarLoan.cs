using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System.Model
{
    internal class CarLoan : Loan
    {
        private decimal v1;
        private decimal v2;
        private int v3;
        private string? v4;
        private string? v5;
        private int v6;

        public string CarModel { get; set; }
        public int CarValue { get; set; }

        // Default Constructor
        public CarLoan() {
            LoanType = "CarLoan";
        }

        // Parameterized Constructor
        public CarLoan(int loanId, int customerId, decimal principal, decimal interestRate, int loanTerm, string loanType, string loanStatus, string carModel, int carValue)
            //: base(loanId, customer, principal, interestRate, loanTerm, loanType, loanStatus)
        {
            CarModel = carModel;
            CarValue = carValue;
        }

        public CarLoan(int loanId, decimal v1, decimal v2, int v3, string loanType, string? v4, string? v5, int v6)
        {
            LoanId = loanId;
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            LoanType = loanType;
            this.v4 = v4;
            this.v5 = v5;
            this.v6 = v6;
        }

        public override void PrintSpecificDetails()
        {
            Console.WriteLine($"Car Model: {CarModel}, Car Value: {CarValue}");
        }
    }
}
