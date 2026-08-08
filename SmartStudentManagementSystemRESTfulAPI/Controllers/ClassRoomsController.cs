using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;
using SmartStudentManagementSystemRESTfulAPI.Dtos.ClassRoomDtos;
using System.Security.Claims;

/*
      ClassRoom Management
        Admin Can : Add / Edit / Delete / View classrooms.
        Teacher Can : View classrooms.
        Student Can : View classrooms.
*/


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ClassRoomsController : ControllerBase
{
    private readonly ClassRoomService _classRoomService;

    public ClassRoomsController(ClassRoomService classRoomService)
    {
        _classRoomService = classRoomService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<ActionResult<IEnumerable<ClassRoomDetailsDto>>> GetAll()
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        if (string.IsNullOrEmpty(currentUserId))
        {
            return Unauthorized(new { message = "Invalid token or user ID not found." });
        }

        bool isTeacher = User.IsInRole("Teacher");

        var classRooms = await _classRoomService.GetAllAsync(currentUserId, isTeacher);
        return Ok(classRooms);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Teacher,Student")]
    public async Task<ActionResult<ClassRoomDetailsDto>> GetById(int id)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        if (string.IsNullOrEmpty(currentUserId))
        {
            return Unauthorized(new { message = "Invalid token or user ID not found." });
        }

        bool isStudent = User.IsInRole("Student");

        var classRoom = await _classRoomService.GetByIdAsync(id, currentUserId, isStudent);
        return Ok(classRoom);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateClassRoomDto dto)
    {
        await _classRoomService.CreateAsync(dto);
        return Ok(new { message = "ClassRoom created successfully." });// 201 Created
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateClassRoomDto dto)
    {
        await _classRoomService.UpdateAsync(id, dto);
        return Ok(new { message = "ClassRoom updated successfully." });
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _classRoomService.DeleteAsync(id);
        return Ok(new { message = "ClassRoom deleted successfully." });
    }
}