using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartStudentManagementSystemRESTfulAPI.Dtos.EnrollmentDtos;
using SmartStudentManagementSystemRESTfulAPI.Services.SmartStudentManagementSystemRESTfulAPI.Application.Services;
using System.Security.Claims;


/*
  Enrollment Management
    Admin Can : Add / Edit / Delete / View enrollments.
                Assign students to courses and classes.(Create Enrollment)

    Teacher Can : Add / Edit / Delete / View enrollments.
                Assign students to courses and classes.(Create Enrollment)

    Student Can : View their own enrollments only.
 */

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EnrollmentsController : ControllerBase
{
    private readonly EnrollmentService _enrollmentService;

    public EnrollmentsController(EnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }


    // GET: api/enrollments
    [HttpGet]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<ActionResult<IEnumerable<EnrollmentDetailsDto>>> GetAll()
    {
        var enrollments = await _enrollmentService.GetAllAsync();

        return Ok(enrollments);
    }


    // GET: api/enrollments/5
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Teacher,Student")]
    public async Task<ActionResult<EnrollmentDetailsDto>> GetById(int id)
    {

        string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        if (string.IsNullOrEmpty(currentUserId))
        {
            return Unauthorized(new { message = "Invalid token or user ID not found." });
        }

        bool isStudent = User.IsInRole("Student");

        var enrollment = await _enrollmentService.GetByIdAsync(id, currentUserId, isStudent);

        return Ok(enrollment);
    }


    // POST: api/enrollments
    [HttpPost]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> Create([FromBody] CreateEnrollmentDto dto)
    {
        await _enrollmentService.CreateAsync(dto);

        return StatusCode(StatusCodes.Status201Created,new { message = "Enrollment created successfully." });
    }


    // PUT: api/enrollments/5
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> Update(int id,[FromBody] UpdateEnrollmentDto dto)
    {
        await _enrollmentService.UpdateAsync(id, dto);

        return Ok(new { message = "Enrollment updated successfully." });
    }


    // DELETE: api/enrollments/5
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> Delete(int id)
    {
        await _enrollmentService.DeleteAsync(id);

        return Ok(new { message = "Enrollment deleted successfully." });
    }
}