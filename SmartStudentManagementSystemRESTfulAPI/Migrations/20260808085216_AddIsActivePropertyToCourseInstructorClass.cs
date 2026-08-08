using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartStudentManagementSystemRESTfulAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActivePropertyToCourseInstructorClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CourseInstructors",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CourseInstructors");
        }
    }
}
