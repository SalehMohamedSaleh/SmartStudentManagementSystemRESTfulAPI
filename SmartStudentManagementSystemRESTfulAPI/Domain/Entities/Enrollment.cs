using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;

namespace SmartStudentManagementSystemRESTfulAPI.Domain.Entities
{
    // Represents a student's registration in a specific course.
    // Attendance and Grade both depend on this record (existence-dependent),
    // rather than on Student and Course independently.
    public class Enrollment : BaseEntity
    {
        public DateTime EnrollmentDate { get; set; }
        public string AcademicYear { get; set; } = string.Empty; // 2026/2027
        public Semester Semester { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        // Navigation
        public ICollection<Attendance> Attendances { get; } = new List<Attendance>();
        public ICollection<Grade> Grades { get; } = new List<Grade>();
    }
}
