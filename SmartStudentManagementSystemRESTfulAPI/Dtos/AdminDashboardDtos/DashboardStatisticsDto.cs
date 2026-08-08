namespace SmartStudentManagementSystemRESTfulAPI.Dtos.AdminDashboardDtos
{
    public class DashboardStatisticsDto
    {
        public int UsersCount { get; set; }

        public int StudentsCount { get; set; }

        public int TeachersCount { get; set; }

        public int CoursesCount { get; set; }

        public int ClassRoomsCount { get; set; }

        public int EnrollmentsCount { get; set; }


        // Total number of attendances recorded today
        public int TotalAttendancesToday { get; set; }

        // General Average Grades In The year
        public decimal GeneralAverageGrade { get; set; }

        // The Latest Users Whos Registered In The System
        public List<RecentUserDto> RecentUsers { get; set; } = new(); 
    }


    
}
