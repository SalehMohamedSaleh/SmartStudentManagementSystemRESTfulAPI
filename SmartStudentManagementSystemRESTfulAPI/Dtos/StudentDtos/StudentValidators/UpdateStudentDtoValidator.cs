using FluentValidation;
using SmartStudentManagementSystemRESTfulAPI.Dtos.StudentDtos;

namespace SmartStudentManagementSystemRESTfulAPI.Dtos.StudentDtos.StudentValidators
{
    public class UpdateStudentDtoValidator : StudentBaseValidator<UpdateStudentDto>
    {
        public UpdateStudentDtoValidator()
        {
            // إضافة القواعد الخاصة بالـ Update فقط
            RuleFor(s => s.Id)
                .GreaterThan(0).WithMessage("Student Id is required.");

            RuleFor(s => s.Status)
                .IsInEnum().WithMessage("Student status is invalid.");
        }
    }
}