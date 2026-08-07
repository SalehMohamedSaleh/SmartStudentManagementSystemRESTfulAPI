using FluentValidation;
using SmartStudentManagementSystemRESTfulAPI.DTOs.Grade;

namespace SmartStudentManagementSystemRESTfulAPI.DTOs.Grade.Validators
{
    public class CreateGradeDtoValidator : BaseGradeValidator<CreateGradeDto>
    {
        public CreateGradeDtoValidator()
        {
            // يرث القواعد المشتركة تلقائياً
        }
    }
}