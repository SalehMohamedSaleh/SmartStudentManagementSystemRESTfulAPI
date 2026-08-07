using Microsoft.AspNetCore.Mvc;
using SmartStudentManagementSystemRESTfulAPI.Dtos.EnrollmentDtos;
using SmartStudentManagementSystemRESTfulAPI.Services.SmartStudentManagementSystemRESTfulAPI.Application.Services;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController : ControllerBase
{
    private readonly EnrollmentService _enrollmentService;

    public EnrollmentsController(EnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }


    // GET: api/enrollments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EnrollmentDetailsDto>>> GetAll()
    {
        var enrollments = await _enrollmentService.GetAllAsync();

        return Ok(enrollments);
    }


    // GET: api/enrollments/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EnrollmentDetailsDto>> GetById(int id)
    {
        var enrollment = await _enrollmentService.GetByIdAsync(id);

        return Ok(enrollment);
    }


    // POST: api/enrollments
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEnrollmentDto dto)
    {
        await _enrollmentService.CreateAsync(dto);

        return StatusCode(StatusCodes.Status201Created,new { message = "Enrollment created successfully." });
    }


    // PUT: api/enrollments/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id,[FromBody] UpdateEnrollmentDto dto)
    {
        await _enrollmentService.UpdateAsync(id, dto);

        return Ok(new { message = "Enrollment updated successfully." });
    }


    // DELETE: api/enrollments/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _enrollmentService.DeleteAsync(id);

        return Ok(new { message = "Enrollment deleted successfully." });
    }
}