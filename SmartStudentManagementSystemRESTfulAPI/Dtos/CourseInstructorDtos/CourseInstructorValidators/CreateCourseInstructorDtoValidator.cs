using FluentValidation;
using SmartStudentManagementSystemRESTfulAPI.DTOs.CourseInstructor;

namespace SmartStudentManagementSystemRESTfulAPI.DTOs.CourseInstructor.Validators
{
    public class CreateCourseInstructorDtoValidator : BaseCourseInstructorValidator<CreateCourseInstructorDto>
    {
        public CreateCourseInstructorDtoValidator()
        {
            // يرث القواعد المشتركة تلقائياً
        }
    }
}