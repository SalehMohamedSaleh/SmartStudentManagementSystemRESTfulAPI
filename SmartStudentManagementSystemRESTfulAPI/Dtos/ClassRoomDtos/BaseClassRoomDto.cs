namespace SmartStudentManagementSystemRESTfulAPI.Dtos.ClassRoomDtos
{
    public class BaseClassRoomDto
    {
        public string GradeLevel { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }
}
