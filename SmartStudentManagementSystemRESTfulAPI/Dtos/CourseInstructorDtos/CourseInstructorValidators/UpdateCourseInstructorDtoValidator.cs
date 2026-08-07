using FluentValidation;
using SmartStudentManagementSystemRESTfulAPI.DTOs.CourseInstructor;

namespace SmartStudentManagementSystemRESTfulAPI.DTOs.CourseInstructor.Validators
{
    public class UpdateCourseInstructorDtoValidator : BaseCourseInstructorValidator<UpdateCourseInstructorDto>
    {
        public UpdateCourseInstructorDtoValidator()
        {
            // يرث القواعد المشتركة تلقائياً
        }
    }
}