using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartStudentManagementSystemRESTfulAPI.Dtos.AttendanceDtos;
using SmartStudentManagementSystemRESTfulAPI.Services;

namespace SmartStudentManagementSystemRESTfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly AttendanceService _attendanceService;

        public AttendancesController(AttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceDetailsDto>>> GetAll()
        {
            var attendances = await _attendanceService.GetAllAsync();
            return Ok(attendances);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AttendanceDetailsDto>> GetById(int id)
        {
            var attendance = await _attendanceService.GetByIdAsync(id);
            return Ok(attendance);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateAttendanceDto dto)
        {
            await _attendanceService.CreateAsync(dto);
            return StatusCode(StatusCodes.Status201Created, new { message = "Attendance created successfully." });
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAttendanceDto dto)
        {
            await _attendanceService.UpdateAsync(id, dto);
            return Ok(new { message = "Attendance updated successfully." });
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _attendanceService.DeleteAsync(id);
            return Ok(new { message = "Attendance deleted successfully." });
        }
    }
}
