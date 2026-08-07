using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;

namespace SmartStudentManagementSystemRESTfulAPI.Infrastructure.Configuration
{
        public class StudentConfiguration : BaseEntityConfiguration<Student>
        {
            public override void Configure(EntityTypeBuilder<Student> builder)
            {
                base.Configure(builder);

                // Table Name 
                builder.ToTable("Students");

                // Properties

                builder.Property(s => s.Name)
                       .IsRequired()
                       .HasMaxLength(100);

                builder.Property(s => s.BirthDate)
                       .IsRequired();

                builder.Property(s => s.Address)
                       .HasMaxLength(250);

                builder.Property(s => s.ImageUrl)
                       .HasMaxLength(500);

                builder.Property(s => s.Phone)
                       .HasMaxLength(20);

                builder.Property(s => s.GuardianPhone)
                       .HasMaxLength(20);

                builder.Property(s => s.Email)
                       .IsRequired()
                       .HasMaxLength(150);

                builder.Property(s => s.Status)
                       .IsRequired()
                       .HasConversion<string>()
                       .HasMaxLength(20); 
                   
                builder.Property(s => s.Gender)
                       .IsRequired()
                       .HasConversion<string>()
                       .HasMaxLength(20);


            // Relationships

                builder.HasOne(s => s.ApplicationUser)
                       .WithOne(au => au.Student)
                       .HasForeignKey<Student>(s => s.ApplicationUserId)
                       .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(s => s.ClassRoom)
                       .WithMany(c => c.Students)
                       .HasForeignKey(s => s.ClassRoomId)
                       .OnDelete(DeleteBehavior.Restrict);

                builder.HasMany(s => s.Enrollments)
                       .WithOne(e => e.Student)
                       .HasForeignKey(e => e.StudentId)
                       .OnDelete(DeleteBehavior.Restrict);

                // Indexes

                builder.HasIndex(s => s.Email) 
                       .IsUnique()
                       .HasFilter("[IsDeleted] = 0");
        }
       
        }
}
