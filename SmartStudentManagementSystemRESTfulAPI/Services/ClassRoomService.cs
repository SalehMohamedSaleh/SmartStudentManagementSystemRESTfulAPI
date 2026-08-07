using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;
using SmartStudentManagementSystemRESTfulAPI.Dtos.ClassRoomDtos;
using SmartStudentManagementSystemRESTfulAPI.Infrastructure;

public class ClassRoomService
{
    private readonly SchoolDbContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateClassRoomDto> _createValidator;
    private readonly IValidator<UpdateClassRoomDto> _updateValidator;

    public ClassRoomService(
        SchoolDbContext context,
        IMapper mapper,
        IValidator<CreateClassRoomDto> createValidator,
        IValidator<UpdateClassRoomDto> updateValidator)
    {
        _context = context;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<List<ClassRoomDetailsDto>> GetAllAsync()
    {
        var classRooms = await _context.ClassRooms
                             .AsNoTracking()
                             .ProjectTo<ClassRoomDetailsDto>(_mapper.ConfigurationProvider)
                             .ToListAsync();
        return classRooms;
    }

    public async Task<ClassRoomDetailsDto> GetByIdAsync(int id)
    {
        var classRoom = await _context.ClassRooms
            .AsNoTracking()
            .ProjectTo<ClassRoomDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (classRoom is null)
            throw new KeyNotFoundException($"ClassRoom with Id '{id}' was not found.");

        return classRoom;
    }

    public async Task CreateAsync(CreateClassRoomDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var classRoom = _mapper.Map<ClassRoom>(dto);

        _context.ClassRooms.Add(classRoom);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, UpdateClassRoomDto dto)
    {
        if (id != dto.Id)
            throw new ArgumentException("Route Id does not match ClassRoom Id.");

        await _updateValidator.ValidateAndThrowAsync(dto);

        var classRoom = await _context.ClassRooms.FindAsync(id);

        if (classRoom is null)
            throw new KeyNotFoundException($"ClassRoom with Id '{id}' was not found.");

        _mapper.Map(dto, classRoom);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var classRoom = await _context.ClassRooms.FindAsync(id);

        if (classRoom is null)
            throw new KeyNotFoundException($"ClassRoom with Id '{id}' was not found.");

        // Soft Delete
        classRoom.IsDeleted = true;

        await _context.SaveChangesAsync();
    }
}