using AutoMapper;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;
using SmartStudentManagementSystemRESTfulAPI.DTOs.CourseInstructor;

namespace SmartStudentManagementSystemRESTfulAPI.Mapping
{
    public class CourseInstructorProfile : Profile
    {
        public CourseInstructorProfile()
        {
            // Create DTO → Entity
            CreateMap<CreateCourseInstructorDto, CourseInstructor>();

            // Update DTO → Entity
            CreateMap<UpdateCourseInstructorDto, CourseInstructor>();

            // Entity → Details DTO
            CreateMap<CourseInstructor, CourseInstructorDetailsDto>()
                .ForMember(dest => dest.TeacherName,
                    opt => opt.MapFrom(src => src.Teacher.Name))
                .ForMember(dest => dest.CourseName,
                    opt => opt.MapFrom(src => src.Course.Name));
        }
    }
}