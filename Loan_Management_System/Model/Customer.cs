using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_Management_System.Model
{
    internal class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int CreditScore { get; set; }

        // Default Constructor
        public Customer() { }

        // Parameterized Constructor
        public Customer(int customerId, string name, string email, string phone, string address, int creditScore)
        {
            CustomerId = customerId;
            Name = name;
            EmailAddress = email;
            PhoneNumber = phone;
            Address = address;
            CreditScore = creditScore;
        }

        // Print all information
        public void PrintCustomerInfo()
        {
            Console.WriteLine($"ID: {CustomerId}, Name: {Name}, Email: {EmailAddress}, Phone: {PhoneNumber}, Address: {Address}, Credit Score: {CreditScore}");
        }
    }
}
