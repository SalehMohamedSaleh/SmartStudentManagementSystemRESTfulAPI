using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;

namespace SmartStudentManagementSystemRESTfulAPI.Dtos.EnrollmentDtos
{
    public class EnrollmentDetailsDto
    {
        public int Id { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string AcademicYear { get; set; } = string.Empty;
        public Semester Semester { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
    }
}
