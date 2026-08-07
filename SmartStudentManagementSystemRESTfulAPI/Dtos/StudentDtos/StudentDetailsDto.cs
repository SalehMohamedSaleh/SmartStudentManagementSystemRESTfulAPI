using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;

namespace SmartStudentManagementSystemRESTfulAPI.Dtos.StudentDtos
{
    public class StudentDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateOnly BirthDate { get; set; }

        public int Age { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? GuardianPhone { get; set; }

        public string Email { get; set; } = string.Empty;

        public StudentStatus Status { get; set; }

        public Gender Gender { get; set; }

        public string? ImageUrl { get; set; }

        // student.ClassRoom.Name == Grade 2 - A
        public string ClassRoomName { get; set; } = string.Empty; 
    }
}
