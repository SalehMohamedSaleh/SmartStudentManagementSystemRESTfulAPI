using System.ComponentModel.DataAnnotations.Schema;

namespace SmartStudentManagementSystemRESTfulAPI.Domain.Entities
{
    public class ClassRoom : BaseEntity
    {
        public string GradeLevel { get; set; } = string.Empty;  // Grade 1
        public string Section { get; set; } = string.Empty; // A
        public int Capacity { get; set; }

        // Not mapped to the database, calculated property
        public string Name => $"{GradeLevel} - {Section}";

        // Navigation
        public ICollection<Student> Students { get; } = new List<Student>();
        public ICollection<Teacher> Teachers { get; } = new List<Teacher>();
    }
}
