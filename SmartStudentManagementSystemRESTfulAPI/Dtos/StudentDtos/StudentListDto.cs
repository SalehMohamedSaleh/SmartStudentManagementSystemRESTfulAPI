using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;

namespace SmartStudentManagementSystemRESTfulAPI.Dtos.StudentDtos
{
    public class StudentListDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Email { get; set; } = string.Empty;

        public StudentStatus Status { get; set; }

        public Gender Gender { get; set; }

        public string? ImageUrl { get; set; }

        public string ClassRoomName { get; set; } = string.Empty;
    }
}
