using FluentValidation;

namespace SmartStudentManagementSystemRESTfulAPI.Dtos.AttendanceDtos.AttendanceValidators
{
    public abstract class BaseAttendanceValidator<T> : AbstractValidator<T> where T : BaseAttendanceDto
    {
        protected BaseAttendanceValidator()
        {
            RuleFor(a => a.Date)
                .NotEqual(default(DateOnly)).WithMessage("Attendance date is required.")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Date cannot be in the future.");

            RuleFor(a => a.Status)
                .IsInEnum().WithMessage("Attendance status is invalid.");

            RuleFor(a => a.EnrollmentId)
                .GreaterThan(0).WithMessage("Enrollment is required.");
        }
    }
}
