using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;

namespace SmartStudentManagementSystemRESTfulAPI.Dtos.EnrollmentDtos
{
    public class CreateEnrollmentDto
    {
        public DateTime EnrollmentDate { get; set; }
        public string AcademicYear { get; set; } = string.Empty;
        public Semester Semester { get; set; }

        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}


