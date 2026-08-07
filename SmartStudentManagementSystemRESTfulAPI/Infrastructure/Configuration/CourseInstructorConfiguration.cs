using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;

namespace SmartStudentManagementSystemRESTfulAPI.Infrastructure.Configuration
{
    public class CourseInstructorConfiguration : IEntityTypeConfiguration<CourseInstructor>
    {
        public void Configure(EntityTypeBuilder<CourseInstructor> builder)
        {
            // Table
            builder.ToTable("CourseInstructors");

            // Composite Key
            builder.HasKey(ci => new
            {
                ci.TeacherId,
                ci.CourseId
            });

            // Properties
            builder.Property(ci => ci.Role)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(20); 

            // Relationships

            builder.HasOne(ci => ci.Teacher)
                   .WithMany(t => t.CourseInstructors)
                   .HasForeignKey(ci => ci.TeacherId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(ci => ci.Course)
                   .WithMany(c => c.CourseInstructors)
                   .HasForeignKey(ci => ci.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}