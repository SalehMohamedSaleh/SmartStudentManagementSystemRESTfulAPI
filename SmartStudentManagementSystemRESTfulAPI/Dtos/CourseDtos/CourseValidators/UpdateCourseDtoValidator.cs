using FluentValidation;

namespace SmartStudentManagementSystemRESTfulAPI.Dtos.CourseDtos.CourseValidators
{
    public class UpdateCourseDtoValidator : BaseCourseValidator<UpdateCourseDto>
    {
        public UpdateCourseDtoValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0)
                .WithMessage("Course Id is required.");
        }
    }
}
