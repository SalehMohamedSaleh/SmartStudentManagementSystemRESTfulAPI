using Microsoft.AspNetCore.Mvc;
using SmartStudentManagementSystemRESTfulAPI.DTOs.Grade;
using SmartStudentManagementSystemRESTfulAPI.Services;

namespace SmartStudentManagementSystemRESTfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly GradeService _gradeService;

        public GradesController(GradeService gradeService)
        {
            _gradeService = gradeService;
        }

        // GET: api/grades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeDetailsDto>>> GetAll()
        {
            var grades = await _gradeService.GetAllAsync();
            return Ok(grades);
        }

        // GET: api/grades/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GradeDetailsDto>> GetById(int id)
        {
            var grade = await _gradeService.GetByIdAsync(id);
            return Ok(grade);
        }

        // POST: api/grades
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGradeDto dto)
        {
            await _gradeService.CreateAsync(dto);
            return StatusCode(StatusCodes.Status201Created,
                new { message = "Grade created successfully." });
        }

        // PUT: api/grades/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGradeDto dto)
        {
            await _gradeService.UpdateAsync(id, dto);
            return Ok(new { message = "Grade updated successfully." });
        }

        // DELETE: api/grades/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _gradeService.DeleteAsync(id);
            return Ok(new { message = "Grade deleted successfully." });
        }
    }
}