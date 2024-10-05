using Loan_Management_System.DAO;
using Loan_Management_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System.MainModule
{
    internal class MainMenu
    {
        public static void Main(string[] args)
        {
            ILoanRepository loanRepo = new LoanRepository();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n===============Loan Management System:=========================");
                Console.WriteLine("\n===============Welcome to Loan Management System:=========================");
                Console.WriteLine("1. Apply Loan");
                Console.WriteLine("2. Get All Loans");
                Console.WriteLine("3. Get Loan by ID");
                Console.WriteLine("4. Loan Repayment");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ApplyLoan(loanRepo);
                        break;
                    case "2":
                        GetAllLoans(loanRepo);
                        break;
                    case "3":
                        GetLoanById(loanRepo);
                        break;
                    case "4":
                        LoanRepayment(loanRepo);
                        break;
                    case "5":
                        exit = true;
                        Console.WriteLine("Exiting Loan Management System.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        // 1. Apply Loan Method
     
        private static void ApplyLoan(ILoanRepository loanRepo)
        {
            Console.Write("Enter customer ID: ");
            int customerId = int.Parse(Console.ReadLine());

            Console.Write("Enter principal amount: ");
            decimal principalAmount = decimal.Parse(Console.ReadLine());

            Console.Write("Enter interest rate: ");
            decimal interestRate = decimal.Parse(Console.ReadLine());

            Console.Write("Enter loan term (months): ");
            int loanTerm = int.Parse(Console.ReadLine());

            Console.Write("Enter loan type (HomeLoan/CarLoan): ");
            string loanType = Console.ReadLine();

            Loan loan;

            if (loanType.Equals("HomeLoan", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter property address: ");
                string propertyAddress = Console.ReadLine();

                Console.Write("Enter property value: ");
                int propertyValue = int.Parse(Console.ReadLine());

                loan = new HomeLoan(0, customerId, principalAmount, interestRate, loanTerm, "HomeLoan", "Pending", propertyAddress, propertyValue);
            }
            else if (loanType.Equals("CarLoan", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter car model: ");
                string carModel = Console.ReadLine();

                Console.Write("Enter car value: ");
                int carValue = int.Parse(Console.ReadLine());

                loan = new CarLoan(0, customerId, principalAmount, interestRate, loanTerm, "CarLoan", "Pending", carModel, carValue);
            }
            else
            {
                Console.WriteLine("Invalid loan type.");
                return;
            }

            loanRepo.ApplyLoan(loan);
            Console.WriteLine("Loan application processed.");
        }



        // 2. Get All Loans Method
        private static void GetAllLoans(ILoanRepository loanRepo)
        {
            List<Loan> loans = loanRepo.GetAllLoans();
            foreach (Loan loan in loans)
            {
                Console.WriteLine(loan.ToString());
            }
        }

        // 3. Get Loan by ID Method
        private static void GetLoanById(ILoanRepository loanRepo)
        {
            Console.Write("Enter loan ID: ");
            int loanId = int.Parse(Console.ReadLine());

            try
            {
                Loan loan = loanRepo.GetLoanById(loanId);
                Console.WriteLine(loan.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // 4. Loan Repayment Method
        private static void LoanRepayment(ILoanRepository loanRepo)
        {
            Console.Write("Enter loan ID: ");
            int loanId = int.Parse(Console.ReadLine());

            Console.Write("Enter repayment amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            try
            {
                loanRepo.LoanRepayment(loanId, amount);
                Console.WriteLine("Loan repayment processed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
