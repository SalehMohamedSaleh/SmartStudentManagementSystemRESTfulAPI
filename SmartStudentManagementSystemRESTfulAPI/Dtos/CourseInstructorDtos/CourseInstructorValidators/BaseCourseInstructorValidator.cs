using FluentValidation;
using SmartStudentManagementSystemRESTfulAPI.DTOs.CourseInstructor;

namespace SmartStudentManagementSystemRESTfulAPI.DTOs.CourseInstructor.Validators
{
    public abstract class BaseCourseInstructorValidator<T> : AbstractValidator<T> where T : BaseCourseInstructorDto
    {
        protected BaseCourseInstructorValidator()
        {
            // TeacherId
            RuleFor(ci => ci.TeacherId)
                .GreaterThan(0).WithMessage("Teacher is required.");

            // CourseId
            RuleFor(ci => ci.CourseId)
                .GreaterThan(0).WithMessage("Course is required.");

            // Role
            RuleFor(ci => ci.Role)
                .IsInEnum().WithMessage("Teacher role is invalid.");
        }
    }
}