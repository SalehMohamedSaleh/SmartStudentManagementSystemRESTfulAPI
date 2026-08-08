using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartStudentManagementSystemRESTfulAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipBetweenTeacherAndClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeacherClassRooms",
                columns: table => new
                {
                    ClassRoomsId = table.Column<int>(type: "int", nullable: false),
                    TeachersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherClassRooms", x => new { x.ClassRoomsId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_TeacherClassRooms_Classrooms_ClassRoomsId",
                        column: x => x.ClassRoomsId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherClassRooms_Teachers_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherClassRooms_TeachersId",
                table: "TeacherClassRooms",
                column: "TeachersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherClassRooms");
        }
    }
}
