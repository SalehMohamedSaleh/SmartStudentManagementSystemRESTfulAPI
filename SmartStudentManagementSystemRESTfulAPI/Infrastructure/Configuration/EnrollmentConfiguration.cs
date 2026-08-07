using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;

namespace SmartStudentManagementSystemRESTfulAPI.Infrastructure.Configuration
{
    public class EnrollmentConfiguration : BaseEntityConfiguration<Enrollment>
    {
        public override void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            base.Configure(builder);

            // Table
            builder.ToTable("Enrollments");

            // Properties
            builder.Property(e => e.EnrollmentDate)
                   .IsRequired();

            builder.Property(e => e.AcademicYear)
                   .IsRequired()
                   .HasMaxLength(9); // Example: 2026/2027

            builder.Property(e => e.Semester)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(20); ;

            // Relationships

            builder.HasOne(e => e.Student)
                   .WithMany(s => s.Enrollments)
                   .HasForeignKey(e => e.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(e => e.Course)
                   .WithMany(c => c.Enrollments)
                   .HasForeignKey(e => e.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(e => e.Attendances)
                   .WithOne(a => a.Enrollment)
                   .HasForeignKey(a => a.EnrollmentId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(e => e.Grades)
                   .WithOne(g => g.Enrollment)
                   .HasForeignKey(g => g.EnrollmentId)
                   .OnDelete(DeleteBehavior.Restrict);


            // Indexes
            // Unique Constraint (StudentId, CourseId, AcademicYear, Semester)
            builder.HasIndex(e => new
            {
                e.StudentId,
                e.CourseId,
                e.AcademicYear,
                e.Semester
            })
            .IsUnique();
        }
    }
}