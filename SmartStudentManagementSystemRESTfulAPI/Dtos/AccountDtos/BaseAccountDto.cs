namespace SmartStudentManagementSystemRESTfulAPI.Dtos.AccountDtos
{
    public abstract class BaseAccountDto
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
