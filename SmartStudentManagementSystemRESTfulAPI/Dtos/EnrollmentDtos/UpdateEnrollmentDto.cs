using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;

namespace SmartStudentManagementSystemRESTfulAPI.Dtos.EnrollmentDtos
{
    public class UpdateEnrollmentDto
    {
        public int Id { get; set; }

        public DateTime EnrollmentDate { get; set; }
        public string AcademicYear { get; set; } = string.Empty;
        public Semester Semester { get; set; }
    }
}
