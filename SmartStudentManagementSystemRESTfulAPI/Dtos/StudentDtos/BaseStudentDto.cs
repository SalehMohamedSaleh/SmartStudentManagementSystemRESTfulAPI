using Microsoft.AspNetCore.Http;
using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;

namespace SmartStudentManagementSystemRESTfulAPI.Dtos.StudentDtos
{
    public abstract class BaseStudentDto
    {
        public string Name { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? GuardianPhone { get; set; }
        public string Email { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public int ClassRoomId { get; set; }
        public IFormFile? Image { get; set; }
    }
}