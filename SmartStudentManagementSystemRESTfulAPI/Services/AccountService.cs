using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SmartStudentManagementSystemRESTfulAPI.Dtos.AccountDtos;

namespace SmartStudentManagementSystemRESTfulAPI.Application.Services
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly TokenService _tokenService;


        public AccountService(
            UserManager<ApplicationUser> userManager,
            IValidator<RegisterDto> registerValidator,
            IValidator<LoginDto> loginValidator,
            TokenService tokenService)
        {
            _userManager = userManager;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
            _tokenService = tokenService;
        }



        public async Task RegisterAsync(RegisterDto dto)
        {
            await _registerValidator.ValidateAndThrowAsync(dto);

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);

            if (existingUser is not null)
                throw new InvalidOperationException($"Email '{dto.Email}' is already registered.");

            var user = new ApplicationUser
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,

                UserName = dto.Email,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ",result.Errors.Select(e => e.Description));
                throw new InvalidOperationException(errors);
            }

            // Default Role
            await _userManager.AddToRoleAsync(user,"Student");
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
            await _loginValidator.ValidateAndThrowAsync(dto);

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user is null)
                throw new KeyNotFoundException("Invalid email or password.");

            var passwordValid = await _userManager.CheckPasswordAsync(user,dto.Password);

            if (!passwordValid)
                throw new UnauthorizedAccessException("Invalid email or password.");

            var roles = (await _userManager.GetRolesAsync(user)).ToList();

            var token = _tokenService.CreateToken(user,roles);

            return new LoginResponseDto
            {
                Token = token,
                Email = user.Email!,
                Roles = roles
            };
        }

        public Task LogoutAsync()
        {
            // JWT is stateless.
            // Client removes the token.

            return Task.CompletedTask;
        }
    }
}