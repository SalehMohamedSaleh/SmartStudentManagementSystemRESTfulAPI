using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;

namespace SmartStudentManagementSystemRESTfulAPI.DTOs.Grade
{
    public abstract class BaseGradeDto
    {
        public DateTime Date { get; set; }
        public GradeType GradeType { get; set; }
        public decimal Score { get; set; }
        public decimal MaxScore { get; set; }
        public int EnrollmentId { get; set; }
    }
}