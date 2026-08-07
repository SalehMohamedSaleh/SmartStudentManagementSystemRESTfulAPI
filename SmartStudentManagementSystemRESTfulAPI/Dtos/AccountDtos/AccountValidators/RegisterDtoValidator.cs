using FluentValidation;

namespace SmartStudentManagementSystemRESTfulAPI.Dtos.AccountDtos.AccountValidators
{
    public class RegisterDtoValidator : BaseAccountValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.")
                .MaximumLength(50)
                .WithMessage("First name cannot exceed 50 characters.");

            RuleFor(r => r.LastName)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .MaximumLength(50)
                .WithMessage("Last name cannot exceed 50 characters.");
        }
    }
}
