using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;

namespace SmartStudentManagementSystemRESTfulAPI.Infrastructure.Configuration
{
    public class ClassRoomConfiguration : BaseEntityConfiguration<ClassRoom>
    {
        public override void Configure(EntityTypeBuilder<ClassRoom> builder)
        {
            base.Configure(builder);

            // Table
            builder.ToTable("Classrooms", table =>
            {
                table.HasCheckConstraint(
                    "CK_Classroom_Capacity",
                    "[Capacity] > 0");
            });

            // Properties
            builder.Property(c => c.GradeLevel)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(c => c.Section)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(c => c.Capacity)
                   .IsRequired();

            builder.Ignore(c => c.Name); // == Not Mapped

            // Relationships
            builder.HasMany(c => c.Students)
                   .WithOne(s => s.ClassRoom)
                   .HasForeignKey(s => s.ClassRoomId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(c => new { c.GradeLevel, c.Section })
                   .IsUnique();
        }

    }
}
