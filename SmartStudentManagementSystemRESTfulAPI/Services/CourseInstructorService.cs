using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;
using SmartStudentManagementSystemRESTfulAPI.DTOs.CourseInstructor;
using SmartStudentManagementSystemRESTfulAPI.Infrastructure;

namespace SmartStudentManagementSystemRESTfulAPI.Services
{
    public class CourseInstructorService
    {
        private readonly SchoolDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCourseInstructorDto> _createValidator;
        private readonly IValidator<UpdateCourseInstructorDto> _updateValidator;

        public CourseInstructorService(
            SchoolDbContext context,
            IMapper mapper,
            IValidator<CreateCourseInstructorDto> createValidator,
            IValidator<UpdateCourseInstructorDto> updateValidator)
        {
            _context = context;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<List<CourseInstructorDetailsDto>> GetAllAsync()
        {
            var courseInstructors = await _context.CourseInstructors
                .AsNoTracking()
                .ProjectTo<CourseInstructorDetailsDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return courseInstructors;
        }

        public async Task<CourseInstructorDetailsDto> GetByIdsAsync(int teacherId, int courseId)
        {
            var courseInstructor = await _context.CourseInstructors
                .AsNoTracking()
                .ProjectTo<CourseInstructorDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ci => ci.TeacherId == teacherId && ci.CourseId == courseId);

            if (courseInstructor is null)
                throw new KeyNotFoundException($"CourseInstructor with TeacherId '{teacherId}' and CourseId '{courseId}' was not found.");

            return courseInstructor;
        }

        // Roles = "Admin"
        public async Task CreateAsync(CreateCourseInstructorDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);

            var courseInstructor = _mapper.Map<CourseInstructor>(dto);

            _context.CourseInstructors.Add(courseInstructor);
            await _context.SaveChangesAsync();
        }

        // Roles = "Admin"
        public async Task UpdateAsync(int teacherId, int courseId, UpdateCourseInstructorDto dto)
        {
            if (teacherId != dto.TeacherId || courseId != dto.CourseId)
                throw new ArgumentException("Route Ids do not match CourseInstructor Ids.");

            await _updateValidator.ValidateAndThrowAsync(dto);

            var courseInstructor = await _context.CourseInstructors
                .FirstOrDefaultAsync(ci => ci.TeacherId == teacherId && ci.CourseId == courseId);

            if (courseInstructor is null)
                throw new KeyNotFoundException($"CourseInstructor not found.");

            _mapper.Map(dto, courseInstructor);

            await _context.SaveChangesAsync();
        }

        // Roles = "Admin"
        public async Task DeleteAsync(int teacherId, int courseId)
        {
            var courseInstructor = await _context.CourseInstructors
                .FirstOrDefaultAsync(ci => ci.TeacherId == teacherId && ci.CourseId == courseId);

            if (courseInstructor is null)
                throw new KeyNotFoundException($"CourseInstructor not found.");

            // Hard Delete (بدون Soft Delete)
            _context.CourseInstructors.Remove(courseInstructor);

            await _context.SaveChangesAsync();
        }
    }
}