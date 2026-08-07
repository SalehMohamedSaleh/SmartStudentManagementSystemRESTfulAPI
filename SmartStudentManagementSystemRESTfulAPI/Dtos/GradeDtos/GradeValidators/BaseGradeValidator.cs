using FluentValidation;
using SmartStudentManagementSystemRESTfulAPI.DTOs.Grade;

namespace SmartStudentManagementSystemRESTfulAPI.DTOs.Grade.Validators
{
    public abstract class BaseGradeValidator<T> : AbstractValidator<T> where T : BaseGradeDto
    {
        protected BaseGradeValidator()
        {
            // Date
            RuleFor(g => g.Date)
                .NotEqual(default(DateTime)).WithMessage("Grade date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Grade date cannot be in the future.");

            // GradeType
            RuleFor(g => g.GradeType)
                .IsInEnum().WithMessage("Grade type is invalid.");

            // Score
            RuleFor(g => g.Score)
                .GreaterThanOrEqualTo(0).WithMessage("Score cannot be negative.");

            // MaxScore
            RuleFor(g => g.MaxScore)
                .GreaterThan(0).WithMessage("Maximum score must be greater than zero.");

            // Score <= MaxScore
            RuleFor(g => g)
                .Must(g => g.Score <= g.MaxScore)
                .WithMessage("Score cannot be greater than the maximum score.");

            // EnrollmentId
            RuleFor(g => g.EnrollmentId)
                .GreaterThan(0).WithMessage("Enrollment is required.");
        }
    }
}