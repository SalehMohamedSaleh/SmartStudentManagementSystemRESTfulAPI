using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStudentManagementSystemRESTfulAPI.Domain.Interfaces;

public class AuditableEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IAuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        // الإعدادات المشتركة لجميع الكيانات التي تحتوي على Audit
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);
    }
}