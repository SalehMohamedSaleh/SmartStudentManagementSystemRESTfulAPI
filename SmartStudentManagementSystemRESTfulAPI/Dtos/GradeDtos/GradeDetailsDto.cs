using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;

namespace SmartStudentManagementSystemRESTfulAPI.DTOs.Grade
{
    public class GradeDetailsDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public GradeType GradeType { get; set; }
        public decimal Score { get; set; }
        public decimal MaxScore { get; set; }
        public decimal Percentage { get; set; }
        public int EnrollmentId { get; set; }
    }
}