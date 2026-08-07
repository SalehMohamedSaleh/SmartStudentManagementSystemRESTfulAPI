using Microsoft.AspNetCore.Mvc;
using SmartStudentManagementSystemRESTfulAPI.DTOs.Teacher;
using SmartStudentManagementSystemRESTfulAPI.Services;

namespace SmartStudentManagementSystemRESTfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly TeacherService _teacherService;

        public TeachersController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // GET: api/teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDetailsDto>>> GetAll()
        {
            var teachers = await _teacherService.GetAllAsync();
            return Ok(teachers);
        }

        // GET: api/teachers/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TeacherDetailsDto>> GetById(int id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);
            return Ok(teacher);
        }

        // POST: api/teachers
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateTeacherDto dto)
        {
            await _teacherService.CreateAsync(dto);
            return StatusCode(StatusCodes.Status201Created,
                new { message = "Teacher created successfully." });
        }

        // PUT: api/teachers/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateTeacherDto dto)
        {
            await _teacherService.UpdateAsync(id, dto);
            return Ok(new { message = "Teacher updated successfully." });
        }

        // DELETE: api/teachers/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _teacherService.DeleteAsync(id);
            return Ok(new { message = "Teacher deleted successfully." });
        }
    }
}