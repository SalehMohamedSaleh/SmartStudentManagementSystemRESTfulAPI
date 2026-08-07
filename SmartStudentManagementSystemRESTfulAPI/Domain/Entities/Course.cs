namespace SmartStudentManagementSystemRESTfulAPI.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CreditHours { get; set; }

        // Navigation
        public ICollection<CourseInstructor> CourseInstructors { get; }
        = new List<CourseInstructor>();
        public ICollection<Enrollment> Enrollments { get; } = new List<Enrollment>();
    }
}
