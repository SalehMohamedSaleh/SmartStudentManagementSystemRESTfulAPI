using System.ComponentModel.DataAnnotations.Schema;
using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;

namespace SmartStudentManagementSystemRESTfulAPI.Domain.Entities
{
    public class Student : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public string? Phone { get; set; }
        public string? GuardianPhone { get; set; }
        public string Email { get; set; } = string.Empty;
        public StudentStatus Status { get; set; }
        public Gender Gender { get; set; }

        // Derived attribute — not stored in the database
        // DayOfYear ==> calculate The Number of the day In the Year

        // calculated property
        public int Age
        {
            get
            {
                var today = DateOnly.FromDateTime(DateTime.Today);

                var age = today.Year - BirthDate.Year;

                if (BirthDate > today.AddYears(-age))
                    age--;

                return age;
            }
        }

        // Identity Link - REQUIRED
        public int ApplicationUserId { get; set; }  // Foreign Key to ApplicationUser
        public ApplicationUser ApplicationUser { get; set; } = null!;

        // Navigation
        public int ClassRoomId { get; set; }
        public ClassRoom ClassRoom { get; set; } = null!;

        public ICollection<Enrollment> Enrollments { get; } = new List<Enrollment>();

    }
}
