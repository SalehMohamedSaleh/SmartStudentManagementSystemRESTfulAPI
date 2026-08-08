using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;
using SmartStudentManagementSystemRESTfulAPI.Domain.Interfaces;

namespace SmartStudentManagementSystemRESTfulAPI.Domain.Entities
{
    public class CourseInstructor : IAuditableEntity
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public TeacherRole Role { get; set; }

        public DateTime CreatedAt { get ; set ; }
        public DateTime UpdatedAt { get ; set ; }
        public bool IsDeleted { get ; set ; }
    }
}
