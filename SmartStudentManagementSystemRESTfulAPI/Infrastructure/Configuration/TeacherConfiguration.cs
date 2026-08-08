using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;

namespace SmartStudentManagementSystemRESTfulAPI.Infrastructure.Configuration
{
    public class TeacherConfiguration : BaseEntityConfiguration<Teacher>
    {
        public override void Configure(EntityTypeBuilder<Teacher> builder)
        {
            base.Configure(builder);

            // Table
            builder.ToTable("Teachers");

            // Properties
            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(t => t.Address)
                   .HasMaxLength(250);

            builder.Property(t => t.ImageUrl)
                   .HasMaxLength(500);

            builder.Property(t => t.Specialization)
                   .HasMaxLength(100);

            builder.Property(t => t.HireDate)
                   .IsRequired();

            builder.Property(t => t.Email)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(t => t.Phone)
                   .HasMaxLength(20);

            builder.Property(t => t.Gender)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(20);


            // Relationships

            builder.HasOne(t => t.ApplicationUser)
                   .WithOne(au => au.Teacher)
                   .HasForeignKey<Teacher>(t => t.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.CourseInstructors)
                   .WithOne(ci => ci.Teacher)
                   .HasForeignKey(ci => ci.TeacherId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.ClassRooms)
                   .WithMany(cr => cr.Teachers)
                   .UsingEntity(j => j.ToTable("TeacherClassRooms"));


            // Indexes

            builder.HasIndex(t => t.Email)
                   .IsUnique()
                   .HasFilter("[IsDeleted] = 0");
        }
    }
}