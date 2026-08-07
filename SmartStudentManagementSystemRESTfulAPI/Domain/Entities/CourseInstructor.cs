using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;

namespace SmartStudentManagementSystemRESTfulAPI.Domain.Entities
{
    public class CourseInstructor
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public TeacherRole Role { get; set; }
    }
}
