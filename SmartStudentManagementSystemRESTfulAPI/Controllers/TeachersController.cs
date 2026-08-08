using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;
using SmartStudentManagementSystemRESTfulAPI.DTOs.Teacher;
using SmartStudentManagementSystemRESTfulAPI.Services;
using System.Security.Claims;

namespace SmartStudentManagementSystemRESTfulAPI.Controllers
{
    /*
  Teacher Management
    Admin Can : Add / Edit / Delete / View teachers.

    Teacher Can : View their own data only.
 */

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeachersController : ControllerBase
    {
        private readonly TeacherService _teacherService;

        public TeachersController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // GET: api/teachers
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<TeacherDetailsDto>>> GetAll()
        {
            var teachers = await _teacherService.GetAllAsync();
            return Ok(teachers);
        }

        // GET: api/teachers/5
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<ActionResult<TeacherDetailsDto>> GetById(int id)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            if (string.IsNullOrEmpty(currentUserId))
            {
                return Unauthorized(new { message = "Invalid token or user ID not found." });
            }

            bool isTeacher = User.IsInRole("Teacher");
            var teacher = await _teacherService.GetByIdAsync(id, currentUserId, isTeacher);
            return Ok(teacher);
        }

        // POST: api/teachers
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] CreateTeacherDto dto)
        {
            await _teacherService.CreateAsync(dto);
            return StatusCode(StatusCodes.Status201Created,
                new { message = "Teacher created successfully." });
        }

        // PUT: api/teachers/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateTeacherDto dto)
        {
            await _teacherService.UpdateAsync(id, dto);
            return Ok(new { message = "Teacher updated successfully." });
        }

        // DELETE: api/teachers/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _teacherService.DeleteAsync(id);
            return Ok(new { message = "Teacher deleted successfully." });
        }

        [HttpPost("{teacherId}/classrooms/{classRoomId}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> AssignTeacherToClassRoom(int teacherId, int classRoomId)
        {
            await _teacherService.AssignTeacherToClassRoomAsync(teacherId, classRoomId);

            return Ok(new { message = "Teacher assigned to the classroom successfully." });
        }
    }
}