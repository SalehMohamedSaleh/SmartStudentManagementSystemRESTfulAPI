using FluentValidation;
using SmartStudentManagementSystemRESTfulAPI.DTOs.Grade;

namespace SmartStudentManagementSystemRESTfulAPI.DTOs.Grade.Validators
{
    public class UpdateGradeDtoValidator : BaseGradeValidator<UpdateGradeDto>
    {
        public UpdateGradeDtoValidator()
        {
            // إضافة القواعد الخاصة بالـ Update فقط
            RuleFor(g => g.Id)
                .GreaterThan(0).WithMessage("Grade Id is required.");
        }
    }
}