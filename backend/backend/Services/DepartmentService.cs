using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace backend.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IValidator<Department> _validator;

        public DepartmentService(IDepartmentRepository departmentRepository, IValidator<Department> validator)
        {
            _departmentRepository = departmentRepository;
            _validator = validator;
        }

        public IEnumerable<Department> GetAll()
        {
            return _departmentRepository.GetAll();
        }

        public Department GetById(int id)
        {
            return _departmentRepository.GetById(id);
        }

        public void Add(Department department)
        {
            ValidateDepartment(department);



            _departmentRepository.Add(department);
        }

        public void Update(Department department)
        {
            ValidateDepartment(department);
           _departmentRepository.Update(department);
        }

        public void Delete(int id)
        {
            _departmentRepository.Delete(id);
        }

        private void ValidateDepartment(Department department)
        {
            ValidationResult result = _validator.Validate(department);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }

    }
}
