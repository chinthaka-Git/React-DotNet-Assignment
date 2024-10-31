using backend.DTOs;
using backend.Models;

namespace backend.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeDto ToDto(this Employee employee, Department department)
        {
            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                DateOfBirth = employee.DateOfBirth,
                Salary = employee.Salary,
                Department = department
            };
        }
        public static Employee ToEntity(this EmployeeDto employee)
        {
            return new Employee
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                DateOfBirth = employee.DateOfBirth,
                Salary = employee.Salary,
                DepartmentId = employee.Department.DepartmentID
            };
        }
    }
}
