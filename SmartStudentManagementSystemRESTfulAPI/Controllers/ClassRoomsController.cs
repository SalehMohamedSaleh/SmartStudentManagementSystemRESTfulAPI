using Microsoft.AspNetCore.Mvc;
using SmartStudentManagementSystemRESTfulAPI.Dtos.ClassRoomDtos;

[Route("api/[controller]")]
[ApiController]
public class ClassRoomsController : ControllerBase
{
    private readonly ClassRoomService _classRoomService;

    public ClassRoomsController(ClassRoomService classRoomService)
    {
        _classRoomService = classRoomService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClassRoomDetailsDto>>> GetAll()
    {
        var classRooms = await _classRoomService.GetAllAsync();
        return Ok(classRooms);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClassRoomDetailsDto>> GetById(int id)
    {
        var classRoom = await _classRoomService.GetByIdAsync(id);
        return Ok(classRoom);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClassRoomDto dto)
    {
        await _classRoomService.CreateAsync(dto);
        return Ok(new { message = "ClassRoom created successfully." });// 201 Created
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateClassRoomDto dto)
    {
        await _classRoomService.UpdateAsync(id, dto);
        return Ok(new { message = "ClassRoom updated successfully." });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _classRoomService.DeleteAsync(id);
        return Ok(new { message = "ClassRoom deleted successfully." });
    }
}