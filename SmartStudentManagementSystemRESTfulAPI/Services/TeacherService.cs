using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;
using SmartStudentManagementSystemRESTfulAPI.DTOs.Teacher;
using SmartStudentManagementSystemRESTfulAPI.Infrastructure;

namespace SmartStudentManagementSystemRESTfulAPI.Services
{
    public class TeacherService
    {
        private readonly SchoolDbContext _context;
        private readonly IMapper _mapper;
        private readonly ImageService _imageService;
        private readonly IValidator<CreateTeacherDto> _createValidator;
        private readonly IValidator<UpdateTeacherDto> _updateValidator;

        public TeacherService(
            SchoolDbContext context,
            IMapper mapper,
            ImageService imageService,
            IValidator<CreateTeacherDto> createValidator,
            IValidator<UpdateTeacherDto> updateValidator)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<List<TeacherDetailsDto>> GetAllAsync()
        {
            var teachers = await _context.Teachers
                                         .AsNoTracking()
                                         .ProjectTo<TeacherDetailsDto>(_mapper.ConfigurationProvider)
                                         .ToListAsync();
            return teachers;
        }

        public async Task<TeacherDetailsDto> GetByIdAsync(int id, string currentUserId, bool isTeacher)
        {
            int parsedUserId = int.Parse(currentUserId);
            var query = _context.Teachers.AsNoTracking();

            // If the user is a teacher, filter the teachers to only include the one that belongs to the teacher
            if (isTeacher)
            {
                query = query.Where(t => t.ApplicationUserId == parsedUserId);
            }

            var teacher = await query
                .ProjectTo<TeacherDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (teacher is null)
                throw new KeyNotFoundException($"Teacher with Id '{id}' was not found.");

            return teacher;
        }

        public async Task CreateAsync(CreateTeacherDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);

            var teacher = _mapper.Map<Teacher>(dto);

            // Handle image upload if provided
            if (dto.Image != null)
            {
                teacher.ImageUrl = await _imageService.SaveImageAsync(dto.Image, "images/teachers");
            }

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateTeacherDto dto)
        {
            if (id != dto.Id)
                throw new ArgumentException("Route Id does not match Teacher Id.");

            await _updateValidator.ValidateAndThrowAsync(dto);

            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher is null)
                throw new KeyNotFoundException($"Teacher with Id '{id}' was not found.");

            _mapper.Map(dto, teacher);

            // Handle image upload if provided
            if (dto.Image != null)
            {
                _imageService.DeleteImage(teacher.ImageUrl!);
                teacher.ImageUrl = await _imageService.SaveImageAsync(dto.Image, "images/teachers");
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher is null)
                throw new KeyNotFoundException($"Teacher with Id '{id}' was not found.");

            // Soft Delete
            teacher.IsDeleted = true;

            await _context.SaveChangesAsync();
        }

        public async Task AssignTeacherToClassRoomAsync(int teacherId, int classRoomId)
        {
            var teacher = await _context.Teachers.FindAsync(teacherId);
            var classRoom = await _context.ClassRooms.FindAsync(classRoomId);

            if (teacher == null || classRoom == null)
                throw new KeyNotFoundException("Teacher or ClassRoom not found");

            bool isAlreadyAssigned = await _context.ClassRooms
                            .Where(c => c.Id == classRoomId)
                            .AnyAsync(c => c.Teachers.Any(t => t.Id == teacherId));
            if (isAlreadyAssigned)
            {
                throw new InvalidOperationException("This teacher is already assigned to this classroom.");
            }

            classRoom.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
        }

    }
}