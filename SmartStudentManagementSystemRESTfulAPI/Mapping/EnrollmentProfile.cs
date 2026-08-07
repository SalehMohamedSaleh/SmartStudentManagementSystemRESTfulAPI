using AutoMapper;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;
using SmartStudentManagementSystemRESTfulAPI.Dtos.EnrollmentDtos;

namespace SmartStudentManagementSystemRESTfulAPI.Application.Mapping
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            // Create DTO → Entity
            CreateMap<CreateEnrollmentDto, Enrollment>();

            // Update DTO → Entity
            CreateMap<UpdateEnrollmentDto, Enrollment>();

            // Entity → Details DTO
            CreateMap<Enrollment, EnrollmentDetailsDto>()
                .ForMember(dest => dest.StudentName,
                    opt => opt.MapFrom(src => src.Student.Name))
                .ForMember(dest => dest.CourseName,
                    opt => opt.MapFrom(src => src.Course.Name));
        }
    }
}