using AutoMapper;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;
using SmartStudentManagementSystemRESTfulAPI.Dtos.CourseDtos;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        // التحويل من DTOs إلى الـ Entity
        CreateMap<CreateCourseDto, Course>();
        CreateMap<UpdateCourseDto, Course>();

        // التحويل من الـ Entity إلى DTOs (العرض)
        CreateMap<Course, CourseDetailsDto>();
    }
}