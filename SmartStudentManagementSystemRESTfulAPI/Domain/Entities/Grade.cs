using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartStudentManagementSystemRESTfulAPI.Domain.Entities
{
    // Not a Weak Entity by identity (has its own PK), but existence-dependent
    // on Enrollment via a mandatory (NOT NULL) foreign key.
    public class Grade : BaseEntity
    {
        public DateTime Date { get; set; }
        public GradeType GradeType { get; set; }
        public decimal Score { get; set; }
        public decimal MaxScore { get; set; }

        // calculated property
        public decimal Percentage => MaxScore == 0 ? 0 : (Score / MaxScore) * 100;


        // navigation
        public int EnrollmentId { get; set; }
        public Enrollment Enrollment { get; set; } = null!;
    }
}
