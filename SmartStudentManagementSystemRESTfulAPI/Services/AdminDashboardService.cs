using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartStudentManagementSystemRESTfulAPI.Dtos.AdminDashboardDtos;
using SmartStudentManagementSystemRESTfulAPI.Infrastructure;

namespace SmartStudentManagementSystemRESTfulAPI.Application.Services
{
    public class AdminDashboardService
    {
        private readonly SchoolDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidator<ChangeRoleDto> _changeRoleValidator;

        public AdminDashboardService(
            SchoolDbContext context,
            UserManager<ApplicationUser> userManager,
            IValidator<ChangeRoleDto> changeRoleValidator)
        {
            _context = context;
            _userManager = userManager;
            _changeRoleValidator = changeRoleValidator;
        }

        public async Task<List<UserDetailsDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.AsNoTracking().ToListAsync();

            var result = new List<UserDetailsDto>();

            foreach (var user in users)
            {
                var roles = (await _userManager.GetRolesAsync(user)).ToList();

                result.Add(new UserDetailsDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email ?? string.Empty,
                    Roles = roles
                });
            }

            return result;
        }


        public async Task ChangeRoleAsync(ChangeRoleDto dto)
        {
            await _changeRoleValidator.ValidateAndThrowAsync(dto);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == dto.UserId);

            if (user is null)
                throw new KeyNotFoundException($"User with Id '{dto.UserId}' was not found.");

            var currentRoles = await _userManager.GetRolesAsync(user);

            if (currentRoles.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);

                if (!removeResult.Succeeded)
                {
                    var errors = string.Join(", ",
                        removeResult.Errors.Select(e => e.Description));

                    throw new InvalidOperationException(errors);
                }
            }

            var addResult = await _userManager.AddToRoleAsync(user, dto.Role);

            if (!addResult.Succeeded)
            {
                var errors = string.Join(", ",
                    addResult.Errors.Select(e => e.Description));

                throw new InvalidOperationException(errors);
            }
        }


        public async Task<DashboardStatisticsDto> GetStatisticsAsync()
        {
            return new DashboardStatisticsDto
            {
                UsersCount = await _userManager.Users.CountAsync(),

                StudentsCount = await _context.Students.CountAsync(),

                TeachersCount = await _context.Teachers.CountAsync(),

                CoursesCount = await _context.Courses.CountAsync(),

                ClassRoomsCount = await _context.ClassRooms.CountAsync(),

                EnrollmentsCount = await _context.Enrollments.CountAsync()
            };
        }
    }
}