using Microsoft.AspNetCore.Mvc;
using SmartStudentManagementSystemRESTfulAPI.Dtos.StudentDtos;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly StudentService _studentService;

    public StudentsController(StudentService studentService)
    {
        _studentService = studentService;
    }

    // GET: api/students
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentListDto>>> GetAll()
    {
        var students = await _studentService.GetAllAsync();
        return Ok(students);
    }

    // GET: api/students/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<StudentDetailsDto>> GetById(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        return Ok(student);
    }

    // POST: api/students
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateStudentDto dto)
    {
        await _studentService.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created, new { message = "Student created successfully." });
    }

    // PUT: api/students/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateStudentDto dto)
    {
        await _studentService.UpdateAsync(id, dto);
        return Ok(new { message = "Student updated successfully." });
    }

    // DELETE: api/students/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _studentService.DeleteAsync(id);
        return Ok(new { message = "Student deleted successfully." });
    }
}