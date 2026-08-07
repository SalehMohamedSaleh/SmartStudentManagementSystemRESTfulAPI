using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;
using SmartStudentManagementSystemRESTfulAPI.DTOs.Grade;
using SmartStudentManagementSystemRESTfulAPI.Infrastructure;

namespace SmartStudentManagementSystemRESTfulAPI.Services
{
    public class GradeService
    {
        private readonly SchoolDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateGradeDto> _createValidator;
        private readonly IValidator<UpdateGradeDto> _updateValidator;

        public GradeService(
            SchoolDbContext context,
            IMapper mapper,
            IValidator<CreateGradeDto> createValidator,
            IValidator<UpdateGradeDto> updateValidator)
        {
            _context = context;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<List<GradeDetailsDto>> GetAllAsync()
        {
            var grads = await _context.Grades
                .AsNoTracking()
                .ProjectTo<GradeDetailsDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return grads;
        }

        public async Task<GradeDetailsDto> GetByIdAsync(int id)
        {
            var grade = await _context.Grades
                .AsNoTracking()
                .ProjectTo<GradeDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (grade is null)
                throw new KeyNotFoundException($"Grade with Id '{id}' was not found.");

            return grade;
        }

        public async Task CreateAsync(CreateGradeDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);

            if (!await _context.Enrollments.AnyAsync(e => e.Id == dto.EnrollmentId))
                throw new KeyNotFoundException($"Enrollment with Id '{dto.EnrollmentId}' was not found.");

            var exists = await _context.Grades.AnyAsync(g =>
                g.EnrollmentId == dto.EnrollmentId &&
                g.GradeType == dto.GradeType);

            if (exists)
                throw new InvalidOperationException(
                    $"Grade '{dto.GradeType}' already exists for Enrollment '{dto.EnrollmentId}'.");

            var grade = _mapper.Map<Grade>(dto);

            _context.Grades.Add(grade);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateGradeDto dto)
        {
            if (id != dto.Id)
                throw new ArgumentException("Route Id does not match Grade Id.");

            await _updateValidator.ValidateAndThrowAsync(dto);

            var grade = await _context.Grades.FindAsync(id);

            if (grade is null)
                throw new KeyNotFoundException($"Grade with Id '{id}' was not found.");

            var exists = await _context.Grades.AnyAsync(g =>
                g.Id != id &&
                g.EnrollmentId == grade.EnrollmentId &&
                g.GradeType == dto.GradeType);

            if (exists)
                throw new InvalidOperationException(
                    $"Grade '{dto.GradeType}' already exists for Enrollment '{grade.EnrollmentId}'.");

            _mapper.Map(dto, grade);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var grade = await _context.Grades.FindAsync(id);

            if (grade is null)
                throw new KeyNotFoundException($"Grade with Id '{id}' was not found.");

            grade.IsDeleted = true;

            await _context.SaveChangesAsync();
        }
    }
}