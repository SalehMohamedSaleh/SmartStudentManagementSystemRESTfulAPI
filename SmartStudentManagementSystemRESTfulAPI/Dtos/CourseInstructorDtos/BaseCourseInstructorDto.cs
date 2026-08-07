using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;

namespace SmartStudentManagementSystemRESTfulAPI.DTOs.CourseInstructor
{
    public abstract class BaseCourseInstructorDto
    {
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public TeacherRole Role { get; set; }
    }
}