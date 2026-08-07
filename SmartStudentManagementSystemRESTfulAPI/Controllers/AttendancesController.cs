using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartStudentManagementSystemRESTfulAPI.Dtos.AttendanceDtos;
using SmartStudentManagementSystemRESTfulAPI.Services;
using System.Security.Claims;

namespace SmartStudentManagementSystemRESTfulAPI.Controllers
{
    /* Required Roles For Attendance Tracking:
     
           *** Teachers can: mark daily attendance per course.

           *** Students can: view their own attendance history.
    */

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AttendancesController : ControllerBase
    {
        private readonly AttendanceService _attendanceService;

        public AttendancesController(AttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<ActionResult<IEnumerable<AttendanceDetailsDto>>> GetAll()
        {
            var attendances = await _attendanceService.GetAllAsync();
            return Ok(attendances);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Teacher,Student,Admin")]
        public async Task<ActionResult<AttendanceDetailsDto>> GetById(int id)
        {
            // Retrieve the current user's ID and roles from the claims
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            if (string.IsNullOrEmpty(currentUserId))
            {
                return Unauthorized(new { message = "Invalid token or user ID not found." });
            }

            // Check if the user is a student
            bool isStudent = User.IsInRole("Student");


            var attendance = await _attendanceService.GetByIdAsync(id, currentUserId, isStudent);
            return Ok(attendance);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> Create([FromBody] CreateAttendanceDto dto)
        {
            await _attendanceService.CreateAsync(dto);
            return StatusCode(StatusCodes.Status201Created, new { message = "Attendance created successfully." });
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAttendanceDto dto)
        {
            await _attendanceService.UpdateAsync(id, dto);
            return Ok(new { message = "Attendance updated successfully." });
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _attendanceService.DeleteAsync(id);
            return Ok(new { message = "Attendance deleted successfully." });
        }
    }
}
