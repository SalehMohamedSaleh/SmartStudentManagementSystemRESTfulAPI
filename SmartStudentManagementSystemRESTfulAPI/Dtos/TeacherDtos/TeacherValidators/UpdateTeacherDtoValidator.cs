using FluentValidation;
using SmartStudentManagementSystemRESTfulAPI.DTOs.Teacher;

namespace SmartStudentManagementSystemRESTfulAPI.DTOs.Teacher.Validators
{
    public class UpdateTeacherDtoValidator : BaseTeacherValidator<UpdateTeacherDto>
    {
        public UpdateTeacherDtoValidator()
        {
            // إضافة القواعس الخاصة بالـ Update فقط
            RuleFor(t => t.Id)
                .GreaterThan(0).WithMessage("Teacher Id is required.");
        }
    }
}