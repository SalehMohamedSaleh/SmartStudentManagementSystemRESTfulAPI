using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartStudentManagementSystemRESTfulAPI.Application.Services;
using SmartStudentManagementSystemRESTfulAPI.Dtos.AdminDashboardDtos;

namespace SmartStudentManagementSystemRESTfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : ControllerBase
    {
        private readonly AdminDashboardService _adminDashboardService;

        public AdminDashboardController(AdminDashboardService adminDashboardService)
        {
            _adminDashboardService = adminDashboardService;
        }


        // GET: api/admindashboard/users
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserDetailsDto>>> GetAllUsers()
        {
            var users = await _adminDashboardService.GetAllUsersAsync();

            return Ok(users);
        }


        // PUT: api/admindashboard/change-role
        [HttpPut("change-role")]
        public async Task<IActionResult> ChangeRole([FromBody] ChangeRoleDto dto)
        {
            await _adminDashboardService.ChangeRoleAsync(dto);

            return Ok(new{message = "User role updated successfully."});
        }


        // GET: api/admindashboard/statistics
        [HttpGet("statistics")]
        public async Task<ActionResult<DashboardStatisticsDto>> GetStatistics()
        {
            var statistics = await _adminDashboardService.GetStatisticsAsync();

            return Ok(statistics);
        }
    }
}