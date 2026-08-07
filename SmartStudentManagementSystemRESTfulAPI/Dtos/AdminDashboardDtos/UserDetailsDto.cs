namespace SmartStudentManagementSystemRESTfulAPI.Dtos.AdminDashboardDtos
{
    public class UserDetailsDto
    {
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public List<string> Roles { get; set; } = new();
    }
}
