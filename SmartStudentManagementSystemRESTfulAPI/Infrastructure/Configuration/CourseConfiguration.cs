using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;

namespace SmartStudentManagementSystemRESTfulAPI.Infrastructure.Configuration
{
    public class CourseConfiguration : BaseEntityConfiguration<Course>
    {
        public override void Configure(EntityTypeBuilder<Course> builder)
        {
            base.Configure(builder);

            // Table
            builder.ToTable("Courses", table =>
            {
                table.HasCheckConstraint(
                    "CK_Courses_CreditHours",
                    "[CreditHours] > 0");
            });

            // Properties
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Code)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(c => c.Description)
                   .HasMaxLength(1000);

            builder.Property(c => c.CreditHours)
                   .IsRequired();

            // Relationships
            builder.HasMany(c => c.CourseInstructors)
                   .WithOne(ci => ci.Course)
                   .HasForeignKey(ci => ci.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Enrollments)
                   .WithOne(e => e.Course)
                   .HasForeignKey(e => e.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(c => c.Code)
                   .IsUnique();
        }

    }
}
