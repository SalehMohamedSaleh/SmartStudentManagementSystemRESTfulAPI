using FluentValidation;
using SmartStudentManagementSystemRESTfulAPI.DTOs.Teacher;

namespace SmartStudentManagementSystemRESTfulAPI.DTOs.Teacher.Validators
{
    public class CreateTeacherDtoValidator : BaseTeacherValidator<CreateTeacherDto>
    {
        public CreateTeacherDtoValidator()
        {
            // يرث القواعد المشتركة تلقائياً
        }
    }
}