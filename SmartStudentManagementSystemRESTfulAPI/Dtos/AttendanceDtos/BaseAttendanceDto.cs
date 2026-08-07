using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;

namespace SmartStudentManagementSystemRESTfulAPI.Dtos.AttendanceDtos
{
    public abstract class BaseAttendanceDto
    {
        public DateOnly Date { get; set; }
        public AttendanceStatus Status { get; set; }
        public int EnrollmentId { get; set; }
    }
}
