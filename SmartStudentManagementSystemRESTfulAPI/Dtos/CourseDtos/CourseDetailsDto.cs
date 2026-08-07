namespace SmartStudentManagementSystemRESTfulAPI.Dtos.CourseDtos
{
    public class CourseDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int CreditHours { get; set; }
    }
}
