using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartStudentManagementSystemRESTfulAPI.Domain.Entities;

namespace SmartStudentManagementSystemRESTfulAPI.Infrastructure
{
    public class SchoolDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options)
        {
        }

        // Exposes the Student table as a DbSet.
        // We use an expression-bodied property (=> Set<Student>()) instead of { get; set; }
        // because the DbContext already manages the DbSet instance internally.
        // This makes the property read-only and prevents accidental reassignment.
        public DbSet<Attendance> Attendances => Set<Attendance>();
        public DbSet<ClassRoom> ClassRooms => Set<ClassRoom>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<CourseInstructor> CourseInstructors => Set<CourseInstructor>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Grade> Grades => Set<Grade>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();


        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Use Table-per-Concrete-Type (TPC) mapping strategy for BaseEntity and its derived classes.
            modelBuilder.Entity<BaseEntity>()
                        .HasKey(e => e.Id);

            modelBuilder.Entity<BaseEntity>()
                        .UseTpcMappingStrategy();

            // Apply a global query filter to exclude soft-deleted entities from queries.
            modelBuilder.Entity<BaseEntity>().HasQueryFilter(e => !e.IsDeleted);

            // Hide course instructors if their course is soft-deleted
            modelBuilder.Entity<CourseInstructor>()
                        .HasQueryFilter(ci => !ci.Course.IsDeleted);

            // Apply A Global Query Filter to exclude soft-deleted entities from queries for ApplicationUser.
            modelBuilder.Entity<ApplicationUser>().HasQueryFilter(au => !au.IsDeleted);


            // Automatically applies all IEntityTypeConfiguration<T>
            // implementations from the current assembly.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchoolDbContext).Assembly);
        }



        private void UpdateAuditFields()
        {
            var now = DateTime.UtcNow;

            // Update BaseEntity-derived entities
            var baseEntries = ChangeTracker.Entries<BaseEntity>();// Get all tracked entities that inherit from BaseEntity.
            foreach (var entry in baseEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                    entry.Entity.UpdatedAt = now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = now;
                }
            }

            // Update ApplicationUser
            var userEntries = ChangeTracker.Entries<ApplicationUser>();
            foreach (var entry in userEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                    entry.Entity.UpdatedAt = now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = now;
                }
            }
        }




        public override int SaveChanges()
        {
            UpdateAuditFields();

            return base.SaveChanges();
        }



        public override async Task<int> SaveChangesAsync( CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();

            return await base.SaveChangesAsync(cancellationToken);
        }





    }
}
