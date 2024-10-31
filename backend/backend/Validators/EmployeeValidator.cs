using FluentValidation;
using backend.Models;

namespace backend.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            //RuleFor(e => e.EmployeeId).NotEmpty().WithMessage("Employee ID is required.");
            RuleFor(e => e.FirstName).NotEmpty().WithMessage("First Name is required.");
            RuleFor(e => e.LastName).NotEmpty().WithMessage("Last Name is required.");
            RuleFor(e => e.Email).NotEmpty().EmailAddress().WithMessage("A valid Email is required.");
            RuleFor(e => e.DateOfBirth).NotEmpty().WithMessage("Date of Birth is required.");
            RuleFor(e => e.Salary).NotEmpty().WithMessage("Salary is required.");
            RuleFor(e => e.DepartmentId).NotEmpty().WithMessage("Department ID is required.");
        }
    }
}
