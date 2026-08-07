using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SmartStudentManagementSystemRESTfulAPI.Dtos.StudentDtos
{
    public class UpdateStudentDto : BaseStudentDto
    {
        public int Id { get; set; }
        public StudentStatus Status { get; set; } 
    }
}
