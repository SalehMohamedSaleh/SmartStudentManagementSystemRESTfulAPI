using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;

namespace SmartStudentManagementSystemRESTfulAPI.Domain.Entities
{
    public class Attendance : BaseEntity
    {
        public DateOnly Date { get; set; }
        public AttendanceStatus Status { get; set; }

        // Navigation
        public int EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; } = null!;
    }
}
