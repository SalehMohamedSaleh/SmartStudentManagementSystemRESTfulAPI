using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;

namespace SmartStudentManagementSystemRESTfulAPI.Infrastructure.Configuration
{
    public class AttendanceConfiguration : BaseEntityConfiguration<Attendance>
    {
        public override void Configure(EntityTypeBuilder<Attendance> builder)
        {
            base.Configure(builder);

            // Table
            builder.ToTable("Attendances");

            // Properties
            builder.Property(a => a.Date)
                   .IsRequired();
                   

            builder.Property(a => a.Status)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(20);

            // Relationships
            builder.HasOne(a => a.Enrollment)
                   .WithMany(e => e.Attendances)
                   .HasForeignKey(a => a.EnrollmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            // لا يمكن لنفس الطالب (Enrollment) أن يكون له أكثر من سجل حضور في نفس اليوم.
            builder.HasIndex(a => new { a.EnrollmentId, a.Date })
                   .IsUnique();
        }
    }
}
