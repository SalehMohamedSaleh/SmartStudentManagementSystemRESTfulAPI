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

        public async Task<TeacherDetailsDto> GetByIdAsync(int id)
        {
            var teacher = await _context.Teachers
                .AsNoTracking()
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
                _imageService.DeleteImage(teacher.ImageUrl);
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
    }
}