using SmartStudentManagementSystemRESTfulAPI.Dtos.AccountDtos;

public class RegisterDto : BaseAccountDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}