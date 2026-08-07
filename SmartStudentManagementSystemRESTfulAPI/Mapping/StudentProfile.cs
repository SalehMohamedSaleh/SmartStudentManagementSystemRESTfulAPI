using AutoMapper;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;
using SmartStudentManagementSystemRESTfulAPI.Dtos.StudentDtos;

namespace SmartStudentManagementSystemRESTfulAPI.Mapping
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            // Read DTOs
            // Mapping from Student entity to StudentListDto
            // Get And GetAll Use ProjectionTo for better performance
            CreateMap<Student, StudentListDto>()
                             .ForMember(
                              dest => dest.ClassRoomName,
                              opt => opt.MapFrom(src => src.ClassRoom.Name)); 


            CreateMap<Student, StudentDetailsDto>()
                             .ForMember(dest => dest.ClassRoomName,
                             opt => opt.MapFrom(src=>src.ClassRoom.Name));

            // Create DTO
            // Mapping from CreateStudentDto to Student entity
            // Create And Update Use Map to map DTOs to Entities
            // Note: ImageUrl is ignored here as it will be handled by ImageService
            CreateMap<CreateStudentDto, Student>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

            // Update DTO
            CreateMap<UpdateStudentDto, Student>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
        }
    }
}
