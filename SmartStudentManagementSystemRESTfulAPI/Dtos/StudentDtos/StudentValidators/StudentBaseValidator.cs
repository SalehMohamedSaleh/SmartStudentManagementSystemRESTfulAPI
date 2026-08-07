using FluentValidation;
using SmartStudentManagementSystemRESTfulAPI.Dtos.StudentDtos;

namespace SmartStudentManagementSystemRESTfulAPI.Dtos.StudentDtos.StudentValidators
{
    public abstract class StudentBaseValidator<T> : AbstractValidator<T> where T : BaseStudentDto
    {
        protected StudentBaseValidator()
        {
            // Name
            RuleFor(s => s.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Student name is required.")
                .MaximumLength(100).WithMessage("Student name cannot exceed 100 characters.");

            // BirthDate
            RuleFor(s => s.BirthDate)
                .NotEqual(default(DateOnly)).WithMessage("Birth date is required.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Birth date cannot be in the future.")
                .Must(bd =>
                {
                    var today = DateOnly.FromDateTime(DateTime.Today);
                    var age = today.Year - bd.Year;
                    if (bd > today.AddYears(-age)) age--;
                    return age >= 5;
                }).WithMessage("Student must be at least 5 years old.");

            // Address
            RuleFor(s => s.Address)
                .MaximumLength(250).When(s => !string.IsNullOrWhiteSpace(s.Address))
                .WithMessage("Address cannot exceed 250 characters.");

            // Phone
            RuleFor(s => s.Phone)
                .MaximumLength(20).When(s => !string.IsNullOrWhiteSpace(s.Phone))
                .WithMessage("Phone number cannot exceed 20 characters.");

            // GuardianPhone
            RuleFor(s => s.GuardianPhone)
                .MaximumLength(20).When(s => !string.IsNullOrWhiteSpace(s.GuardianPhone))
                .WithMessage("Guardian phone number cannot exceed 20 characters.");

            // Email
            RuleFor(s => s.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(150).WithMessage("Email cannot exceed 150 characters.")
                .EmailAddress().WithMessage("Email format is invalid.");

            // Gender
            RuleFor(s => s.Gender)
                .IsInEnum().WithMessage("Gender is invalid.");

            // ClassRoomId
            RuleFor(s => s.ClassRoomId)
                .GreaterThan(0).WithMessage("Classroom is required.");

            // Image
            // Will be validated in the service layer, as it involves file handling and storage.

        }
    }
}