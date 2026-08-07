using FluentValidation;
using SmartStudentManagementSystemRESTfulAPI.Dtos.StudentDtos;
using SmartStudentManagementSystemRESTfulAPI.Dtos.StudentDtos.StudentValidators;

public class CreateStudentDtoValidator : StudentBaseValidator<CreateStudentDto>
{
    public CreateStudentDtoValidator()
    {
        // يرث القواعد المشتركة
    }
}