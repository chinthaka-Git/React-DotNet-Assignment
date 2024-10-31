using backend.DTOs;
using backend.Mappers;
using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace backend.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IValidator<Employee> _validator;

        public EmployeeService(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IValidator<Employee> validator)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _validator = validator;
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            var empList = _employeeRepository.GetAll();

            var departments = _departmentRepository.GetAll()
                                .ToDictionary(d => d.DepartmentID, d => d);

            return empList.Select(employee => employee.ToDto(departments[employee.DepartmentId])).ToList();
        }

        public EmployeeDto GetById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if(employee == null)
            {
                throw new Exception($"Employee ID: {id} is invalid.");
            }
            var department = _departmentRepository.GetById(employee.DepartmentId);
            return employee.ToDto(department);
        }

        public void Add(Employee employee)
        {
            ValidateEmployee(employee);
            _employeeRepository.Add(employee);
        }

        public void Update(Employee employee)
        {
            ValidateEmployee(employee);
            _employeeRepository.Update(employee);
        }

        public void Delete(int id)
        {
            _employeeRepository.Delete(id);
        }

        private void ValidateEmployee(Employee employee)
        {
            ValidationResult result = _validator.Validate(employee);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }

    }
}
