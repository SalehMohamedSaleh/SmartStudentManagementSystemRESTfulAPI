using SmartStudentManagementSystemRESTfulAPI.Domain.Interfaces;

namespace SmartStudentManagementSystemRESTfulAPI.Domain.Entities
{
    public abstract class BaseEntity : IAuditableEntity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
