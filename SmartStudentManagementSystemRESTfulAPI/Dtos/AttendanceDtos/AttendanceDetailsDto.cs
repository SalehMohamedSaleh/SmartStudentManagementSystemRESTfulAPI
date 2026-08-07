using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;

namespace SmartStudentManagementSystemRESTfulAPI.Dtos.AttendanceDtos
{
    public class AttendanceDetailsDto
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public AttendanceStatus Status { get; set; }
        public int EnrollmentId { get; set; }
    }
}
