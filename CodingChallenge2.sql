create database Loan_Management
use Loan_Management
CREATE TABLE Customer (
    customerId INT PRIMARY KEY IDENTITY(1,1),  
    name NVARCHAR(100) NOT NULL,
    emailAddress NVARCHAR(100) UNIQUE NOT NULL,  
    phoneNumber NVARCHAR(15) NOT NULL,
    address NVARCHAR(255) NOT NULL,
    creditScore INT CHECK (creditScore BETWEEN 300 AND 850) 
);
INSERT INTO Customer ( name, emailAddress,phoneNumber,address,creditScore)
VALUES 
( 'Sri','sri@gmail.com','0987654321','abc street', 500),
( 'Srinithya','sri123@gmail.com','9345654321','def street', 600);
CREATE TABLE Loans (
    loanId INT PRIMARY KEY IDENTITY(1,1), 
    customerId INT,  
    principalAmount DECIMAL(18,2) NOT NULL,
    interestRate DECIMAL(5,2) NOT NULL,  
    loanTerm INT CHECK (loanTerm > 0), 
    loanType NVARCHAR(50) CHECK (loanType IN ('CarLoan', 'HomeLoan')),  
    loanStatus NVARCHAR(50) CHECK (loanStatus IN ('Pending', 'Approved')),  
    --CONSTRAINT FK_CustomerLoan FOREIGN KEY (customerId) REFERENCES Customer(customerId) 
       -- ON DELETE CASCADE  

);
INSERT INTO Loans (principalAmount, interestRate, loanTerm, loanType, loanStatus) VALUES
(25000.00, 5.50, 60, 'CarLoan', 'Pending'),
(150000.00, 3.75, 240, 'HomeLoan', 'Approved');

create table HomeLoan (
HomeLoanID int identity primary key,
loanId int,
PropertyAddress text,
PropertyValue int,
Foreign key (loanId) references Loans(loanId)
);
INSERT INTO HomeLoan (loanId, PropertyAddress, PropertyValue) 
VALUES 
(1, '4/1 Pulivendula', 200000),
(2, '2/1 Kadapa', 300000);


create table CarLoan (
CarLoanId int identity primary key,
loanId int,
CarModel varchar(30),
CarValue int,
Foreign Key (loanId) references Loans(LoanID)
);
INSERT INTO CarLoan (loanId, CarModel, CarValue) 
VALUES 
(2, 'Honda', 100000),
(1, 'TVS', 200000);
