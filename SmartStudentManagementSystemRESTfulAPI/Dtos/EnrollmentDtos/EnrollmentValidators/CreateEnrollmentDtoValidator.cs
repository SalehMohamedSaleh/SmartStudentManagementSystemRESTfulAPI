using FluentValidation;
using SmartStudentManagementSystemRESTfulAPI.Dtos.EnrollmentDtos;

namespace SmartStudentManagementSystemRESTfulAPI.Application.DTOs.Enrollment.Validators
{
    public class CreateEnrollmentDtoValidator : AbstractValidator<CreateEnrollmentDto>
    {
        public CreateEnrollmentDtoValidator()
        {
            // EnrollmentDate
            RuleFor(e => e.EnrollmentDate)
                .NotEqual(default(DateTime))
                .WithMessage("Enrollment date is required.")
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Enrollment date cannot be in the future.");

            // AcademicYear
            RuleFor(e => e.AcademicYear)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Academic year is required.")
                .MaximumLength(9)
                .WithMessage("Academic year cannot exceed 9 characters.")
                .Matches(@"^\d{4}/\d{4}$")
                .WithMessage("Academic year must be in the format YYYY/YYYY.");

            // Semester
            RuleFor(e => e.Semester)
                .IsInEnum()
                .WithMessage("Semester is invalid.");

            // StudentId
            RuleFor(e => e.StudentId)
                .GreaterThan(0)
                .WithMessage("Student is required.");

            // CourseId
            RuleFor(e => e.CourseId)
                .GreaterThan(0)
                .WithMessage("Course is required.");
        }
    }
}