using FluentValidation;
using SmartStudentManagementSystemRESTfulAPI.DTOs.Teacher;

namespace SmartStudentManagementSystemRESTfulAPI.DTOs.Teacher.Validators
{
    public abstract class BaseTeacherValidator<T> : AbstractValidator<T> where T : BaseTeacherDto
    {
        protected BaseTeacherValidator()
        {
            // Name
            RuleFor(t => t.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Teacher name is required.")
                .MaximumLength(100).WithMessage("Teacher name cannot exceed 100 characters.");

            // Address
            RuleFor(t => t.Address)
                .MaximumLength(250).When(t => !string.IsNullOrWhiteSpace(t.Address))
                .WithMessage("Address cannot exceed 250 characters.");

            // Phone
            RuleFor(t => t.Phone)
                .MaximumLength(20).When(t => !string.IsNullOrWhiteSpace(t.Phone))
                .WithMessage("Phone number cannot exceed 20 characters.");

            // Email
            RuleFor(t => t.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(150).WithMessage("Email cannot exceed 150 characters.")
                .EmailAddress().WithMessage("Email format is invalid.");

            // Specialization
            RuleFor(t => t.Specialization)
                .MaximumLength(100).When(t => !string.IsNullOrWhiteSpace(t.Specialization))
                .WithMessage("Specialization cannot exceed 100 characters.");

            // HireDate
            RuleFor(t => t.HireDate)
                .NotEqual(default(DateOnly)).WithMessage("Hire date is required.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Hire date cannot be in the future.");

            // Gender
            RuleFor(t => t.Gender)
                .IsInEnum().WithMessage("Gender is invalid.");

            // Image
            // Will be validated in the service layer, as it involves file handling and storage.
        }
    }
}