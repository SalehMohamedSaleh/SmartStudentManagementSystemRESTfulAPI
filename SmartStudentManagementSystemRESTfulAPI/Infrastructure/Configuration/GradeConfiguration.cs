using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;

namespace SmartStudentManagementSystemRESTfulAPI.Infrastructure.Configuration
{
    public class GradeConfiguration : BaseEntityConfiguration<Grade>
    {
        public override void Configure(EntityTypeBuilder<Grade> builder)
        {
            base.Configure(builder);

            // Table
            builder.ToTable("Grades", table =>
            {
                table.HasCheckConstraint(
                    "CK_Grades_Score",
                    "[Score] >= 0 AND [Score] <= [MaxScore]");
            });

            // Properties
            builder.Property(g => g.Date)
                   .IsRequired();

            builder.Property(g => g.GradeType)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(20);

            builder.Property(g => g.Score)
                   .IsRequired()
                   .HasPrecision(5, 2);

            builder.Property(g => g.MaxScore)
                   .IsRequired()
                   .HasPrecision(5, 2);

            builder.Ignore(g => g.Percentage);

            // Relationships
            builder.HasOne(g => g.Enrollment)
                   .WithMany(e => e.Grades)
                   .HasForeignKey(g => g.EnrollmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(g => new
            {
                g.EnrollmentId,
                g.GradeType
            })
            .IsUnique();
        }
    }
}