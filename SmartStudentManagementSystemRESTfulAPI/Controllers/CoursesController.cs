using Microsoft.AspNetCore.Mvc;
using SmartStudentManagementSystemRESTfulAPI.Application.Services;
using SmartStudentManagementSystemRESTfulAPI.Dtos.CourseDtos;

namespace SmartStudentManagementSystemRESTfulAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CoursesController(CourseService courseService)
        {
            _courseService = courseService;
        }


        // GET: api/courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDetailsDto>>> GetAll()
        {
            var courses = await _courseService.GetAllAsync();

            return Ok(courses);
        }


        // GET: api/courses/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CourseDetailsDto>> GetById(int id)
        {
            var course = await _courseService.GetByIdAsync(id);

            return Ok(course);
        }


        // POST: api/courses
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseDto dto)
        {
            await _courseService.CreateAsync(dto);

            return StatusCode(StatusCodes.Status201Created,
                new { message = "Course created successfully." });
        }


        // PUT: api/courses/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateCourseDto dto)
        {
            await _courseService.UpdateAsync(id, dto);

            return Ok(new { message = "Course updated successfully." });
        }


        // DELETE: api/courses/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseService.DeleteAsync(id);

            return Ok(new { message = "Course deleted successfully." });
        }
    }
}