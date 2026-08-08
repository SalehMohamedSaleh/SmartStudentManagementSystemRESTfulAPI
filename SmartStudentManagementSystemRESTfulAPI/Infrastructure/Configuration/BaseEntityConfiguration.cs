using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;
using SmartStudentManagementSystemRESTfulAPI.Domain.Interfaces;

namespace SmartStudentManagementSystemRESTfulAPI.Infrastructure.Configuration
{
    public class BaseEntityConfiguration<T> : AuditableEntityConfiguration<T> where T : BaseEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            // Call the base configuration for auditable entities
            base.Configure(builder);
        }
    }
}
