using FluentValidation;
using SmartStudentManagementSystemRESTfulAPI.Dtos.ClassRoomDtos;

namespace SmartStudentManagementSystemRESTfulAPI.Dtos.ClassRoomDtos.ClassRoomValidators
{
    public abstract class ClassRoomBaseValidator<T> : AbstractValidator<T> where T : BaseClassRoomDto
    {
        protected ClassRoomBaseValidator()
        {
            RuleFor(x => x.GradeLevel)
                .NotEmpty().WithMessage("Grade Level is required.")
                .MaximumLength(50).WithMessage("Grade Level must not exceed 50 characters.");

            RuleFor(x => x.Section)
                .NotEmpty().WithMessage("Section is required.")
                .MaximumLength(10).WithMessage("Section must not exceed 10 characters.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Capacity must be greater than zero.");
        }
    }
}