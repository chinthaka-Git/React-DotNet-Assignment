using FluentValidation;
using backend.Models;

namespace backend.Validators
{
    public class DepartmentValidator : AbstractValidator<Department>
    {
        public DepartmentValidator()
        {
            RuleFor(d => d.DepartmentCode).NotEmpty().WithMessage("Department Code is required.");
            RuleFor(d => d.DepartmentName).NotEmpty().WithMessage("Department Name is required.");
        }
    }
}
