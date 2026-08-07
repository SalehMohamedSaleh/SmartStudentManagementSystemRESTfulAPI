using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;

namespace SmartStudentManagementSystemRESTfulAPI.DTOs.Teacher
{
    public class TeacherDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Specialization { get; set; }
        public DateOnly HireDate { get; set; }
        public Gender Gender { get; set; }
        public string? ImageUrl { get; set; }
    }
}