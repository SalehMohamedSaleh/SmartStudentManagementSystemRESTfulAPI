using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;
using SmartStudentManagementSystemRESTfulAPI.DTOs.Grade;
using SmartStudentManagementSystemRESTfulAPI.Services;
using System.Security.Claims;

namespace SmartStudentManagementSystemRESTfulAPI.Controllers
{

    /* Required Roles For Grade Management:
     
           *** Teachers can:
                    1- Add grades per student / course
                    2- View/Edit grades

           *** Students can:
                    1- View their grades only
    */

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GradesController : ControllerBase
    {
        private readonly GradeService _gradeService;

        public GradesController(GradeService gradeService)
        {
            _gradeService = gradeService;
        }

        // GET: api/grades
        [HttpGet]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<ActionResult<IEnumerable<GradeDetailsDto>>> GetAll()
        {
            var grades = await _gradeService.GetAllAsync();
            return Ok(grades);
        }

        // GET: api/grades/5
        
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Teacher,Student,Admin")]
        public async Task<ActionResult<GradeDetailsDto>> GetById(int id)
        {
            // Retrieve the current user's ID and roles from the claims
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            if (string.IsNullOrEmpty(currentUserId))
            {
                return Unauthorized(new { message = "Invalid token or user ID not found." });
            }

            // Check if the user is a student
            bool isStudent = User.IsInRole("Student");

            var grade = await _gradeService.GetByIdAsync(id, currentUserId, isStudent);
            return Ok(grade);
        }

        // POST: api/grades
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Create([FromBody] CreateGradeDto dto)
        {
            await _gradeService.CreateAsync(dto);
            return StatusCode(StatusCodes.Status201Created,
                new { message = "Grade created successfully." });
        }

        // PUT: api/grades/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGradeDto dto)
        {
            await _gradeService.UpdateAsync(id, dto);
            return Ok(new { message = "Grade updated successfully." });
        }

        // DELETE: api/grades/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _gradeService.DeleteAsync(id);
            return Ok(new { message = "Grade deleted successfully." });
        }
    }
}