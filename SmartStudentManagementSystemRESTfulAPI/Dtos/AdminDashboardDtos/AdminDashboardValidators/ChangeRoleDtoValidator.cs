namespace SmartStudentManagementSystemRESTfulAPI.Dtos.AdminDashboardDtos.AdminDashboardValidators
{
    using FluentValidation;

    namespace SmartStudentManagementSystemRESTfulAPI.Application.Validators.AdminDashboardValidators
    {
        public class ChangeRoleDtoValidator : AbstractValidator<ChangeRoleDto>
        {
            public ChangeRoleDtoValidator()
            {
                RuleFor(r => r.UserId)
                    .GreaterThan(0)
                    .WithMessage("User Id is required.");

                RuleFor(r => r.Role)
                    .NotEmpty()
                    .WithMessage("Role is required.")
                    .Must(role => role == "Student" || role == "Teacher" || role == "Admin")
                    .WithMessage("Role must be Student, Teacher, or Admin.");
            }
        }
    }
}
