namespace SmartStudentManagementSystemRESTfulAPI.Services
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using FluentValidation;
    using global::SmartStudentManagementSystemRESTfulAPI.Domain.Entities;
    using global::SmartStudentManagementSystemRESTfulAPI.Dtos.EnrollmentDtos;
    using global::SmartStudentManagementSystemRESTfulAPI.Infrastructure;
    using Microsoft.EntityFrameworkCore;

    namespace SmartStudentManagementSystemRESTfulAPI.Application.Services
    {
        public class EnrollmentService
        {
            private readonly SchoolDbContext _context;
            private readonly IMapper _mapper;
            private readonly IValidator<CreateEnrollmentDto> _createValidator;
            private readonly IValidator<UpdateEnrollmentDto> _updateValidator;

            public EnrollmentService(
                SchoolDbContext context,
                IMapper mapper,
                IValidator<CreateEnrollmentDto> createValidator,
                IValidator<UpdateEnrollmentDto> updateValidator)
            {
                _context = context;
                _mapper = mapper;
                _createValidator = createValidator;
                _updateValidator = updateValidator;
            }

            public async Task<List<EnrollmentDetailsDto>> GetAllAsync()
            {
                var enrollments = await _context.Enrollments
                    .AsNoTracking()
                    .ProjectTo<EnrollmentDetailsDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                return enrollments;
            }

            public async Task<EnrollmentDetailsDto> GetByIdAsync(int id, string currentUserId, bool isStudent)
            {
                int parsedUserId = int.Parse(currentUserId);
                var query = _context.Enrollments.AsNoTracking();

                if (isStudent)
                {
                    // Students can only access their own enrollments
                    query = query.Where(e => e.Student.ApplicationUserId == parsedUserId);
                }

                var enrollment = await query
                    .ProjectTo<EnrollmentDetailsDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (enrollment is null)
                    throw new KeyNotFoundException($"Enrollment with Id '{id}' was not found or you do not have permission to view it.");

                return enrollment;
            }
            public async Task CreateAsync(CreateEnrollmentDto dto)
            {
                await _createValidator.ValidateAndThrowAsync(dto);


                if (!await _context.Students.AnyAsync(s => s.Id == dto.StudentId))
                    throw new KeyNotFoundException($"Student with Id '{dto.StudentId}' was not found.");

                if (!await _context.Courses.AnyAsync(c => c.Id == dto.CourseId))
                    throw new KeyNotFoundException($"Course with Id '{dto.CourseId}' was not found.");


                // Prevent duplicate enrollment for the same student,
                // course, academic year, and semester.
                var exists = await _context.Enrollments.AnyAsync(e =>
                                          e.StudentId == dto.StudentId &&
                                          e.CourseId == dto.CourseId &&
                                          e.AcademicYear == dto.AcademicYear &&
                                          e.Semester == dto.Semester);

                if (exists)
                    throw new InvalidOperationException($"Student with Id '{dto.StudentId}' is already enrolled in Course '{dto.CourseId}' for Academic Year '{dto.AcademicYear}' and Semester '{dto.Semester}'.");

                var enrollment = _mapper.Map<Enrollment>(dto);

                _context.Enrollments.Add(enrollment);

                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(int id, UpdateEnrollmentDto dto)
            {
                if (id != dto.Id)
                    throw new ArgumentException("Route Id does not match Enrollment Id.");

                await _updateValidator.ValidateAndThrowAsync(dto);

                var enrollment = await _context.Enrollments.FindAsync(id);

                if (enrollment is null)
                    throw new KeyNotFoundException($"Enrollment with Id '{id}' was not found.");

                var exists = await _context.Enrollments.AnyAsync(e =>
                                          e.Id != id &&
                                          e.StudentId == enrollment.StudentId &&
                                          e.CourseId == enrollment.CourseId &&
                                          e.AcademicYear == dto.AcademicYear &&
                                          e.Semester == dto.Semester);

                if (exists)
                    throw new InvalidOperationException(
                        $"Student with Id '{enrollment.StudentId}' is already enrolled in Course '{enrollment.CourseId}' for Academic Year '{dto.AcademicYear}' and Semester '{dto.Semester}'.");

                _mapper.Map(dto, enrollment);

                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var enrollment = await _context.Enrollments.FindAsync(id);

                if (enrollment is null)
                    throw new KeyNotFoundException($"Enrollment with Id '{id}' was not found.");

                // Soft Delete
                enrollment.IsDeleted = true;

                await _context.SaveChangesAsync();
            }
        }
    }
}
