using AutoMapper;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;
using SmartStudentManagementSystemRESTfulAPI.DTOs.Teacher;


namespace SmartStudentManagementSystemRESTfulAPI.Mapping
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            // Create DTO → Entity
            // Note: ImageUrl is ignored here as it will be handled by ImageService
            CreateMap<CreateTeacherDto, Teacher>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

            // Update DTO → Entity
            CreateMap<UpdateTeacherDto, Teacher>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

            // Entity → Details DTO (للعرض)
            CreateMap<Teacher, TeacherDetailsDto>();
        }
    }
}