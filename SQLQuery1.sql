CREATE TABLE departments (
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentCode VARCHAR(10) NOT NULL,
    DepartmentName VARCHAR(50) NOT NULL
);

CREATE TABLE employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Salary DECIMAL(18, 2) NOT NULL,
    DepartmentID INT,
    FOREIGN KEY (DepartmentID) REFERENCES departments(DepartmentID)
);
<<<<<<< HEAD


SELECT  *
FROM departments

SELECT  *
FROM employees
=======


SELECT  *
FROM departments

SELECT  *
FROM employees
>>>>>>> 353775acb4dfa9709334d4eda77d1df0ae405eee
