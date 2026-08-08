using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;
using SmartStudentManagementSystemRESTfulAPI.Dtos.CourseDtos;
using SmartStudentManagementSystemRESTfulAPI.Infrastructure;

namespace SmartStudentManagementSystemRESTfulAPI.Application.Services
{
    public class CourseService
    {
        private readonly SchoolDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCourseDto> _createValidator;
        private readonly IValidator<UpdateCourseDto> _updateValidator;

        public CourseService(
            SchoolDbContext context,
            IMapper mapper,
            IValidator<CreateCourseDto> createValidator,
            IValidator<UpdateCourseDto> updateValidator)
        {
            _context = context;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<List<CourseDetailsDto>> GetAllAsync()
        {
            var courses = await _context.Courses
                .AsNoTracking()
                .ProjectTo<CourseDetailsDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return courses;
        }

        public async Task<CourseDetailsDto> GetByIdAsync(int id)
        {
            var course = await _context.Courses
                .AsNoTracking()
                .ProjectTo<CourseDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course is null)
                throw new KeyNotFoundException($"Course with Id '{id}' was not found.");

            return course;
        }

        public async Task CreateAsync(CreateCourseDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);

            var exists = await _context.Courses
                .AnyAsync(c => c.Code == dto.Code);

            if (exists)
                throw new InvalidOperationException(
                    $"Course with Code '{dto.Code}' already exists.");

            var course = _mapper.Map<Course>(dto);

            _context.Courses.Add(course);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateCourseDto dto)
        {
            if (id != dto.Id)
                throw new ArgumentException("Route Id does not match Course Id.");

            await _updateValidator.ValidateAndThrowAsync(dto);

            var course = await _context.Courses.FindAsync(id);

            if (course is null)
                throw new KeyNotFoundException($"Course with Id '{id}' was not found.");

            var exists = await _context.Courses
                .AnyAsync(c =>
                    c.Id != id && // Exclude(يستثني) the current course from the check
                    c.Code == dto.Code); // Check for duplicate Code

            if (exists)
                throw new InvalidOperationException(
                    $"Course with Code '{dto.Code}' already exists.");

            _mapper.Map(dto, course);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course is null)
                throw new KeyNotFoundException($"Course with Id '{id}' was not found.");

            // Soft Delete
            course.IsDeleted = true;

            await _context.SaveChangesAsync();
        }
    }
}