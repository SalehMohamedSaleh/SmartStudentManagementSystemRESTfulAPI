namespace SmartStudentManagementSystemRESTfulAPI.Dtos.ClassRoomDtos
{
    public class ClassRoomDetailsDto : BaseClassRoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
