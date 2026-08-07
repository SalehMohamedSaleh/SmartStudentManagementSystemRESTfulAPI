using FluentValidation;
using SmartStudentManagementSystemRESTfulAPI.Dtos.ClassRoomDtos;

namespace SmartStudentManagementSystemRESTfulAPI.Dtos.ClassRoomDtos.ClassRoomValidators
{
    public class UpdateClassRoomDtoValidator : ClassRoomBaseValidator<UpdateClassRoomDto>
    {
        public UpdateClassRoomDtoValidator()
        {
            // إضافة القواعد الخاصة بالـ Update فقط
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("ClassRoom Id is required.");
        }
    }
}