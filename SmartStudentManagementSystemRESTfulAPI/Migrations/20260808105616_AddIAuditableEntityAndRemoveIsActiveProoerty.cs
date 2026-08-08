using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartStudentManagementSystemRESTfulAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddIAuditableEntityAndRemoveIsActiveProoerty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CourseInstructors");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CourseInstructors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CourseInstructors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CourseInstructors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CourseInstructors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CourseInstructors");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CourseInstructors");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CourseInstructors",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }
    }
}
