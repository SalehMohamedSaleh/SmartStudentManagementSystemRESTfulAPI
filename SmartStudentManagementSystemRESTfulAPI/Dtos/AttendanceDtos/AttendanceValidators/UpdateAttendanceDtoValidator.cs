using FluentValidation;

namespace SmartStudentManagementSystemRESTfulAPI.Dtos.AttendanceDtos.AttendanceValidators
{
    public class UpdateAttendanceDtoValidator : BaseAttendanceValidator<UpdateAttendanceDto>
    {
        public UpdateAttendanceDtoValidator()
        {
            RuleFor(a => a.Id).GreaterThan(0).WithMessage("Attendance Id is required.");
        }
    }
}
