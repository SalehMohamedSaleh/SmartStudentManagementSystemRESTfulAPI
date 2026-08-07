using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // Audit Fields
        builder.Property(au => au.CreatedAt).IsRequired();
        builder.Property(au => au.UpdatedAt).IsRequired();
        builder.Property(au => au.IsDeleted).HasDefaultValue(false);

        // Navigation Properties (One-to-One with Student and Teacher)
        builder.HasOne(au => au.Student)
            .WithOne(s => s.ApplicationUser)
            .HasForeignKey<Student>(s => s.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(au => au.Teacher)
            .WithOne(t => t.ApplicationUser)
            .HasForeignKey<Teacher>(t => t.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}