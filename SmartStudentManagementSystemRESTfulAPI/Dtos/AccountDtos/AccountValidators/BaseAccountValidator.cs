using FluentValidation;

namespace SmartStudentManagementSystemRESTfulAPI.Dtos.AccountDtos.AccountValidators
{
    public abstract class BaseAccountValidator<T> : AbstractValidator<T>
     where T : BaseAccountDto
    {
        protected BaseAccountValidator()
        {
            RuleFor(a => a.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");

            RuleFor(a => a.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters.");
        }
    }
}
