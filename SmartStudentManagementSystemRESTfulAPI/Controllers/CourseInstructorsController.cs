using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartStudentManagementSystemRESTfulAPI.DTOs.CourseInstructor;
using SmartStudentManagementSystemRESTfulAPI.Services;

namespace SmartStudentManagementSystemRESTfulAPI.Controllers
{
    /*
  CourseInstructor Management

    Admin Can : Add / Edit / Delete / View course instructors.
                Assign teachers to courses.

    Teacher Can : View course instructors.
    
    Student Can : View course instructors.
 */

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseInstructorsController : ControllerBase
    {
        private readonly CourseInstructorService _courseInstructorService;

        public CourseInstructorsController(CourseInstructorService courseInstructorService)
        {
            _courseInstructorService = courseInstructorService;
        }

        // GET: api/courseinstructors
        [HttpGet]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<ActionResult<IEnumerable<CourseInstructorDetailsDto>>> GetAll()
        {
            var courseInstructors = await _courseInstructorService.GetAllAsync();
            return Ok(courseInstructors);
        }

        // GET: api/courseinstructors/5/3
        [HttpGet("{teacherId:int}/{courseId:int}")]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<ActionResult<CourseInstructorDetailsDto>> GetByIds(int teacherId, int courseId)
        {
            var courseInstructor = await _courseInstructorService.GetByIdsAsync(teacherId, courseId);
            return Ok(courseInstructor);
        }

        // POST: api/courseinstructors
        [HttpPost]
        [Authorize(Roles = "Admin")]  
        public async Task<IActionResult> Create([FromBody] CreateCourseInstructorDto dto)
        {
            await _courseInstructorService.CreateAsync(dto);
            return StatusCode(StatusCodes.Status201Created,
                new { message = "CourseInstructor created successfully." });
        }

        // PUT: api/courseinstructors/5/3
        [HttpPut("{teacherId:int}/{courseId:int}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Update(int teacherId, int courseId, [FromBody] UpdateCourseInstructorDto dto)
        {
            await _courseInstructorService.UpdateAsync(teacherId, courseId, dto);
            return Ok(new { message = "CourseInstructor updated successfully." });
        }

        // DELETE: api/courseinstructors/5/3
        [HttpDelete("{teacherId:int}/{courseId:int}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Delete(int teacherId, int courseId)
        {
            await _courseInstructorService.DeleteAsync(teacherId, courseId);
            return Ok(new { message = "CourseInstructor deleted successfully." });
        }
    }
}