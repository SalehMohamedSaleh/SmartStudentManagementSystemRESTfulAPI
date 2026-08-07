using Microsoft.AspNetCore.Identity;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;

public class ApplicationUser : IdentityUser<int>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }

    // Navigation Properties
    public Student? Student { get; set; }
    public Teacher? Teacher { get; set; }
}