using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;

namespace SmartStudentManagementSystemRESTfulAPI.DTOs.Teacher
{
    public abstract class BaseTeacherDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Specialization { get; set; }
        public DateOnly HireDate { get; set; }
        public Gender Gender { get; set; }
        public IFormFile? Image { get; set; }
    }
}