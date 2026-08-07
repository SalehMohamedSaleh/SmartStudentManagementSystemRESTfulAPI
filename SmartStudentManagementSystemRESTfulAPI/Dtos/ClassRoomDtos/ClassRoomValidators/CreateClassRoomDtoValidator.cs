using SmartStudentManagementSystemRESTfulAPI.Dtos.ClassRoomDtos;

namespace SmartStudentManagementSystemRESTfulAPI.Dtos.ClassRoomDtos.ClassRoomValidators
{
    public class CreateClassRoomDtoValidator : ClassRoomBaseValidator<CreateClassRoomDto>
    {
        public CreateClassRoomDtoValidator()
        {
            // يرث القواعد المشتركة تلقائياً
        }
    }
}