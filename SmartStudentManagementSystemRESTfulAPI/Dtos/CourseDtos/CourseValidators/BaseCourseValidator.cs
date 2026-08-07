using FluentValidation;
using SmartStudentManagementSystemRESTfulAPI.Dtos.CourseDtos;

public abstract class BaseCourseValidator<T> : AbstractValidator<T> where T : BaseCourseDto
{
    protected BaseCourseValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Course name is required.")
            .MaximumLength(100).WithMessage("Course name cannot exceed 100 characters.");

        RuleFor(c => c.Code)
            .NotEmpty().WithMessage("Course code is required.")
            .MaximumLength(20).WithMessage("Course code cannot exceed 20 characters.");

        RuleFor(c => c.Description)
            .MaximumLength(500).WithMessage("Course description cannot exceed 500 characters.");

        RuleFor(c => c.CreditHours)
            .GreaterThan(0).WithMessage("Credit hours must be greater than zero.");
    }
}