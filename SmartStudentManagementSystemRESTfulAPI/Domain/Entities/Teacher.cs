using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;

namespace SmartStudentManagementSystemRESTfulAPI.Domain.Entities
{
    public class Teacher : BaseEntity
    {

        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public string? Specialization { get; set; }
        public DateOnly HireDate { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public Gender Gender { get; set; }


        // Identity Link - REQUIRED
        public int ApplicationUserId { get; set; }  // Foreign Key to ApplicationUser
        public ApplicationUser ApplicationUser { get; set; } = null!; 

        // Navigation
        public ICollection<CourseInstructor> CourseInstructors { get; }
        = new List<CourseInstructor>();
    }
}
