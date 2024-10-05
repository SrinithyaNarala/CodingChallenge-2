using Loan_Management_System.Exceptions;
using Loan_Management_System.Model;
using Loan_Management_System.Util;
using LoanManagement.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System.DAO
{
    internal class LoanRepository:ILoanRepository
    {
        SqlConnection sqlConnection = null;
        SqlCommand sqlCommand = null;

        public LoanRepository()
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnString());
            sqlCommand = new SqlCommand();
        }

        // ApplyLoan Method
        public void ApplyLoan(Loan loan)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    conn.Open();
                    Console.WriteLine($"CustomerId: {loan.CustomerId}, PrincipalAmount: {loan.PrincipalAmount}, InterestRate: {loan.InterestRate}, LoanTerm: {loan.LoanTerm}, LoanType: {loan.LoanType}, LoanStatus: {loan.LoanStatus}");
                    StringBuilder queryBuilder = new StringBuilder();
                    queryBuilder.Append("INSERT INTO Loans (CustomerID, PrincipalAmount, InterestRate, LoanTerm, LoanType, LoanStatus) ");
                    queryBuilder.Append("VALUES (@CustomerId, @PrincipalAmount, @InterestRate, @LoanTerm, @LoanType, @LoanStatus)");
                    SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);

                    // Explicitly define parameter types
                    cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = loan.CustomerId;
                    cmd.Parameters.Add("@PrincipalAmount", SqlDbType.Decimal).Value = loan.PrincipalAmount;
                    cmd.Parameters.Add("@InterestRate", SqlDbType.Decimal).Value = loan.InterestRate;
                    cmd.Parameters.Add("@LoanTerm", SqlDbType.Int).Value = loan.LoanTerm;
                    cmd.Parameters.Add("@LoanType", SqlDbType.NVarChar).Value = loan.LoanType;
                    cmd.Parameters.Add("@LoanStatus", SqlDbType.NVarChar).Value = loan.LoanStatus;

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Loan successfully applied.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while applying loan: " + ex.Message);
            }
        }


        public decimal CalculateEMI(int loanId)
        {
            // Implementation here
            throw new NotImplementedException();
        }

        public decimal CalculateEMI(decimal principal, decimal interestRate, int term)
        {
            // Implementation here
            throw new NotImplementedException();
        }

        public decimal CalculateInterest(int loanId)
        {
            // Implementation here
            throw new NotImplementedException();
        }

        public decimal CalculateInterest(decimal principal, decimal interestRate, int term)
        {
            // Implementation here
            throw new NotImplementedException();
        }

        // GetAllLoans Method
       public List<Loan> GetAllLoans()
{
            List<Loan> loans = new List<Loan>();

            sqlCommand.CommandText = "SELECT LoanID, CustomerID, PrincipalAmount, InterestRate, LoanTerm, LoanType, LoanStatus FROM Loan";
            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Loan loan = new Loan()
                    {
                        LoanId = (int)reader["LoanID"],
                        CustomerId = (int)reader["CustomerID"], // Assuming you have a CustomerID property
                        PrincipalAmount = (int)reader["PrincipalAmount"],
                        InterestRate = (decimal)reader["InterestRate"],
                        LoanTerm = (int)reader["LoanTerm"],
                        LoanType = (string)reader["LoanType"],
                        LoanStatus = (string)reader["LoanStatus"]
                    };
                    loans.Add(loan);
                }
            }
            sqlConnection.Close();
            return loans;
        }


        public Customer GetCustomerById(int customerId)
        {
            throw new NotImplementedException();
        }

        // GetLoanById Method
        public Loan GetLoanById(int loanId)
        {
            Loan loan = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    conn.Open();
                    string query = "SELECT * FROM Loans WHERE LoanId = @LoanId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@LoanId", loanId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string loanType = reader["LoanType"].ToString();
                        if (loanType == "HomeLoan")
                            loan = new HomeLoan();
                        else if (loanType == "CarLoan")
                            loan = new CarLoan();
                        else
                            throw new InvalidLoanException($"Unknown loan type: {loanType}");

                        loan.LoanId = Convert.ToInt32(reader["LoanId"]);
                        loan.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                        loan.PrincipalAmount = Convert.ToDecimal(reader["PrincipalAmount"]);
                        loan.InterestRate = Convert.ToDecimal(reader["InterestRate"]);
                        loan.LoanTerm = Convert.ToInt32(reader["LoanTerm"]);
                        loan.LoanStatus = reader["LoanStatus"].ToString();
                    }
                    else
                    {
                        throw new InvalidLoanException($"Loan with ID {loanId} not found.");
                    }
                }
            }
            catch (InvalidLoanException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while retrieving loan: " + ex.Message);
            }

            return loan;
        }

        // LoanRepayment Method
        public void LoanRepayment(int loanId, decimal amount)
        {
            try
            {
                Loan loan = GetLoanById(loanId);

                if (loan == null)
                {
                    throw new InvalidLoanException($"Loan with ID {loanId} not found.");
                }

                decimal monthlyEMI = CalculateEMI(loan);
                if (amount < monthlyEMI)
                {
                    Console.WriteLine("Insufficient amount for EMI payment.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(DbConnUtil.GetConnString()))
                {
                    conn.Open();
                    string query = "UPDATE Loans SET PrincipalAmount = PrincipalAmount - @Amount WHERE LoanId = @LoanId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@LoanId", loanId);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Loan repayment successful.");
                }
            }
            catch (InvalidLoanException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while processing loan repayment: " + ex.Message);
            }
        }

        public void LoanStatus(int loanId)
        {
            throw new NotImplementedException();
        }

        // Calculate EMI Method (helper)
        private decimal CalculateEMI(Loan loan)
        {
            decimal monthlyInterestRate = loan.InterestRate / 12 / 100;
            int loanTermInMonths = loan.LoanTerm;
            decimal emi = (loan.PrincipalAmount * monthlyInterestRate * (decimal)Math.Pow((double)(1 + monthlyInterestRate), loanTermInMonths)) /
                          (decimal)(Math.Pow((double)(1 + monthlyInterestRate), loanTermInMonths) - 1);
            return emi;
        }
    }
}
