using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;
using SmartStudentManagementSystemRESTfulAPI.Dtos.AttendanceDtos;
using SmartStudentManagementSystemRESTfulAPI.Infrastructure;

namespace SmartStudentManagementSystemRESTfulAPI.Services
{
    public class AttendanceService
    {
        private readonly SchoolDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateAttendanceDto> _createValidator;
        private readonly IValidator<UpdateAttendanceDto> _updateValidator;

        public AttendanceService(SchoolDbContext context, IMapper mapper,
            IValidator<CreateAttendanceDto> createValidator,
            IValidator<UpdateAttendanceDto> updateValidator)
        {
            _context = context;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<List<AttendanceDetailsDto>> GetAllAsync()
        {
            var attendances = await _context.Attendances.AsNoTracking()
                .ProjectTo<AttendanceDetailsDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return attendances;
        }

        public async Task<AttendanceDetailsDto> GetByIdAsync(int id, string currentUserId, bool isStudent)
        {
            int parsedUserId = int.Parse(currentUserId);
            var query = _context.Attendances.AsNoTracking();

            // If the user is a student, filter the attendances to only include those that belong to the student
            if (isStudent)
            {
                query = query.Where(a => a.Enrollment.Student.ApplicationUserId == parsedUserId);
            }

            // Retrieve the Attendance by ID
            var attendance = await query
                .ProjectTo<AttendanceDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (attendance is null) throw new KeyNotFoundException($"Attendance with Id '{id}' not found.");
            return attendance;
        }

        public async Task CreateAsync(CreateAttendanceDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);
            var attendance = _mapper.Map<Attendance>(dto);
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateAttendanceDto dto)
        {
            if (id != dto.Id) throw new ArgumentException("Route Id does not match.");
            await _updateValidator.ValidateAndThrowAsync(dto);
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance is null) throw new KeyNotFoundException($"Attendance with Id '{id}' not found.");
            _mapper.Map(dto, attendance);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance is null) throw new KeyNotFoundException($"Attendance with Id '{id}' not found.");
            // Soft delete
            attendance.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
