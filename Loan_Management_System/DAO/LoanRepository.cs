using Loan_Management_System.Exceptions;
using Loan_Management_System.Model;
using Loan_Management_System.Util;
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
        private readonly string connectionString = DBConnUtil.GetDBConn();

        // ApplyLoan Method
        public void ApplyLoan(Loan loan)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine($"CustomerId: {loan.CustomerId}, PrincipalAmount: {loan.PrincipalAmount}, InterestRate: {loan.InterestRate}, LoanTerm: {loan.LoanTerm}, LoanType: {loan.LoanType}, LoanStatus: {loan.LoanStatus}");
                    StringBuilder queryBuilder = new StringBuilder();
                    queryBuilder.Append("INSERT INTO Loans (CustomerId, PrincipalAmount, InterestRate, LoanTerm, LoanType, LoanStatus) ");
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

    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM Loans";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Loan loan;
                string loanType = reader["LoanType"].ToString();
                int loanId = Convert.ToInt32(reader["LoanId"]);
                Customer customer = new Customer { CustomerId = Convert.ToInt32(reader["CustomerId"]) }; // Assuming you fetch the customer details properly

                if (loanType.Equals("HomeLoan", StringComparison.OrdinalIgnoreCase))
                {
                    loan = new HomeLoan(
                        loanId,
                        customer,
                        Convert.ToDecimal(reader["PrincipalAmount"]),
                        Convert.ToDecimal(reader["InterestRate"]),
                        Convert.ToInt32(reader["LoanTerm"]),
                        loanType,
                        reader["LoanStatus"].ToString(),
                        reader["PropertyAddress"].ToString(), // Assuming you have this in the database
                        Convert.ToInt32(reader["PropertyValue"]) // Assuming you have this in the database
                    );
                }
                else if (loanType.Equals("CarLoan", StringComparison.OrdinalIgnoreCase))
                {
                    loan = new CarLoan(
                        loanId,
                        //customer,
                        Convert.ToDecimal(reader["PrincipalAmount"]),
                        Convert.ToDecimal(reader["InterestRate"]),
                        Convert.ToInt32(reader["LoanTerm"]),
                        loanType,
                        reader["LoanStatus"].ToString(),
                        reader["CarModel"].ToString(), // Assuming you have this in the database
                        Convert.ToInt32(reader["CarValue"]) // Assuming you have this in the database
                    );
                }
                else
                {
                    throw new InvalidLoanException($"Unknown loan type: {loanType}");
                }

                loans.Add(loan);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error while retrieving loans: " + ex.Message);
    }

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
                using (SqlConnection conn = new SqlConnection(connectionString))
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

                using (SqlConnection conn = new SqlConnection(connectionString))
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
