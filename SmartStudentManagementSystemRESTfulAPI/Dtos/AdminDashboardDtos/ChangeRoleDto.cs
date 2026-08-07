namespace SmartStudentManagementSystemRESTfulAPI.Dtos.AdminDashboardDtos
{
    public class ChangeRoleDto
    {
        public int UserId { get; set; }

        public string Role { get; set; } = string.Empty;
    }
}
