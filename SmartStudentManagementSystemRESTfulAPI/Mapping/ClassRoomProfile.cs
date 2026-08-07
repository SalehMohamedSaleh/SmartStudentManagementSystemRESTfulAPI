using AutoMapper;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;
using SmartStudentManagementSystemRESTfulAPI.Dtos.ClassRoomDtos;

namespace SmartStudentManagementSystemRESTfulAPI.Application.Profiles
{
    public class ClassRoomProfile : Profile
    {
        public ClassRoomProfile()
        {
            // التحويل من DTOs إلى الـ Entity
            CreateMap<CreateClassRoomDto, ClassRoom>();
            CreateMap<UpdateClassRoomDto, ClassRoom>();

            // التحويل من الـ Entity إلى DTOs (العرض)
            CreateMap<ClassRoom, ClassRoomDetailsDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.GradeLevel} - {src.Section}"));
        }
    }
}