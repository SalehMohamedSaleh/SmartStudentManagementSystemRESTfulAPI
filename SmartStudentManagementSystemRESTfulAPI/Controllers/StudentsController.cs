using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartStudentManagementSystemRESTfulAPI.Dtos.StudentDtos;
using System.Security.Claims;

/*
  Student Management
    Admin Can : Add / Edit / Delete / View students.
                Assign students to classes and courses

    Teacher Can : Add / Edit / Delete / View students.
                Assign students to classes and courses

    Student Can : view their own data only.
 */

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StudentsController : ControllerBase
{
    private readonly StudentService _studentService;

    public StudentsController(StudentService studentService)
    {
        _studentService = studentService;
    }

    // GET: api/students
    [HttpGet]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<ActionResult<IEnumerable<StudentListDto>>> GetAll()
    {
        var students = await _studentService.GetAllAsync();
        return Ok(students);
    }

    // GET: api/students/5
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Teacher,Student,Admin")]
    public async Task<ActionResult<StudentDetailsDto>> GetById(int id)
    {

        string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        if (string.IsNullOrEmpty(currentUserId))
        {
            return Unauthorized(new { message = "Invalid token or user ID not found." });
        }

        bool isStudent = User.IsInRole("Student");

        // طبقة الخدمة ستتحقق إذا كان الطالب يطلب ملفه الشخصي فقط
        var student = await _studentService.GetByIdAsync(id, currentUserId, isStudent);
        return Ok(student);
    }

    // POST: api/students
    [HttpPost]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> Create([FromForm] CreateStudentDto dto)
    {
        await _studentService.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created, new { message = "Student created successfully." });
    }

    // PUT: api/students/5
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateStudentDto dto)
    {
        await _studentService.UpdateAsync(id, dto);
        return Ok(new { message = "Student updated successfully." });
    }

    // DELETE: api/students/5
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _studentService.DeleteAsync(id);
        return Ok(new { message = "Student deleted successfully." });
    }


}