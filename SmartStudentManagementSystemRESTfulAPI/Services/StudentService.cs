using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;
using SmartStudentManagementSystemRESTfulAPI.Domain.Enums;
using SmartStudentManagementSystemRESTfulAPI.Dtos.StudentDtos;
using SmartStudentManagementSystemRESTfulAPI.Infrastructure;
using System.ComponentModel.DataAnnotations;

public class StudentService
{
    private readonly SchoolDbContext _context;
    private readonly IMapper _mapper;
    private readonly ImageService _imageService;
    private readonly IValidator<CreateStudentDto> _createValidator;
    private readonly IValidator<UpdateStudentDto> _updateValidator;

    public StudentService(
        SchoolDbContext context,
        IMapper mapper,
        ImageService imageService,
        IValidator<CreateStudentDto> createValidator,
        IValidator<UpdateStudentDto> updateValidator)
    {
        _context = context;
        _mapper = mapper;
        _imageService = imageService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }


    public async Task<List<StudentListDto>> GetAllAsync()
    {
        return await _context.Students
                             .AsNoTracking()
                             .ProjectTo<StudentListDto>(_mapper.ConfigurationProvider)
                             .ToListAsync();
    }

    public async Task<StudentDetailsDto> GetByIdAsync(int id)
    {
        var student = await _context.Students
            .AsNoTracking()
            .ProjectTo<StudentDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student is null)
            throw new KeyNotFoundException($"Student with Id '{id}' was not found.");

        return student;
    }


    public async Task CreateAsync(CreateStudentDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);
      // var result = validator.Validate(dto);
      // if (!result.IsValid)

            var student = _mapper.Map<Student>(dto);

        // تعيين الحالة الافتراضية
        student.Status = StudentStatus.Active;

        // رفع الصورة إذا كانت موجودة وتخزين مسارها
        if (dto.Image != null)
        {
            student.ImageUrl = await _imageService.SaveImageAsync(dto.Image, "images/students");
        }

        _context.Students.Add(student);
        await _context.SaveChangesAsync();
    }

   
    public async Task UpdateAsync(int id, UpdateStudentDto dto)
    {
        if (id != dto.Id)
            throw new ArgumentException("Route Id does not match Student Id.");

        await _updateValidator.ValidateAndThrowAsync(dto);

        var student = await _context.Students.FindAsync(id);

        if (student is null)
            throw new KeyNotFoundException($"Student with Id '{id}' was not found.");

        // يقوم بتحديث الخصائص المشتركة من BaseStudentDto و الـ Status
        _mapper.Map(dto, student);
        
        // إذا أرسل المستخدم صورة جديدة، نقوم بحذف القديمة ورفع الجديدة
       
        if (dto.Image != null)
        {
            _imageService.DeleteImage(student.ImageUrl);
            student.ImageUrl = await _imageService.SaveImageAsync(dto.Image, "images/students");
        }

        await _context.SaveChangesAsync();
    }

   
    public async Task DeleteAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student is null)
            throw new KeyNotFoundException($"Student with Id '{id}' was not found.");

        // Soft Delete
        student.IsDeleted = true;

        await _context.SaveChangesAsync();
    }

}