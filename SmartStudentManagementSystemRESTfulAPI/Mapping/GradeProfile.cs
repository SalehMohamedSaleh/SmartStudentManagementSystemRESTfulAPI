using AutoMapper;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;
using SmartStudentManagementSystemRESTfulAPI.DTOs.Grade;


namespace SmartStudentManagementSystemRESTfulAPI.Mapping
{
    public class GradeProfile : Profile
    {
        public GradeProfile()
        {
            // Create DTO → Entity
            CreateMap<CreateGradeDto, Grade>();

            // Update DTO → Entity
            CreateMap<UpdateGradeDto, Grade>();

            // Entity → Details DTO (للعرض)
            CreateMap<Grade, GradeDetailsDto>()
                .ForMember(dest => dest.Percentage,
                    opt => opt.MapFrom(src => src.Percentage));
        }
    }
}