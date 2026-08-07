using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;

namespace SmartStudentManagementSystemRESTfulAPI.DTOs.CourseInstructor
{
    public class CourseInstructorDetailsDto
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = string.Empty;

        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;

        public TeacherRole Role { get; set; }
    }
}