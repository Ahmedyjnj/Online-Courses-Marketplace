using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoAndDescriptionToInstructorAndStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Students",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Students",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Students",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Students",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Instructors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Instructors",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Instructors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Instructors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Instructors",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Instructors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResumeUrl",
                table: "Instructors",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    AltText = table.Column<string>(type: "text", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseImage_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseVideo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseVideo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseVideo_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseImage_CourseId",
                table: "CourseImage",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseVideo_CourseId",
                table: "CourseVideo",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseImage");

            migrationBuilder.DropTable(
                name: "CourseVideo");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "ResumeUrl",
                table: "Instructors");
        }
    }
}
