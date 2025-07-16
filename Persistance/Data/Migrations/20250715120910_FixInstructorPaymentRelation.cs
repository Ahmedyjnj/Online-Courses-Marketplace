using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixInstructorPaymentRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseImage_Courses_CourseId",
                table: "CourseImage");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseVideo_Courses_CourseId",
                table: "CourseVideo");

            migrationBuilder.DropTable(
                name: "InstructorCourses");

            migrationBuilder.DropIndex(
                name: "IX_Students_Name",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Instructors_Name",
                table: "Instructors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseVideo",
                table: "CourseVideo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseImage",
                table: "CourseImage");

            migrationBuilder.RenameTable(
                name: "CourseVideo",
                newName: "CourseVideos");

            migrationBuilder.RenameTable(
                name: "CourseImage",
                newName: "CourseImages");

            migrationBuilder.RenameIndex(
                name: "IX_CourseVideo_CourseId",
                table: "CourseVideos",
                newName: "IX_CourseVideos_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseImage_CourseId",
                table: "CourseImages",
                newName: "IX_CourseImages_CourseId");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "StudentPayments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "InstructorId",
                table: "Courses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseVideos",
                table: "CourseVideos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseImages",
                table: "CourseImages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "InstructorPayments",
                columns: table => new
                {
                    InstructorId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgressPayment = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorPayments", x => new { x.InstructorId, x.PaymentId });
                    table.ForeignKey(
                        name: "FK_InstructorPayments_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstructorPayments_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                table: "Students",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_Email",
                table: "Instructors",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorPayments_PaymentId",
                table: "InstructorPayments",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseImages_Courses_CourseId",
                table: "CourseImages",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Instructors_InstructorId",
                table: "Courses",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseVideos_Courses_CourseId",
                table: "CourseVideos",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseImages_Courses_CourseId",
                table: "CourseImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Instructors_InstructorId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseVideos_Courses_CourseId",
                table: "CourseVideos");

            migrationBuilder.DropTable(
                name: "InstructorPayments");

            migrationBuilder.DropIndex(
                name: "IX_Students_Email",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Instructors_Email",
                table: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseVideos",
                table: "CourseVideos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseImages",
                table: "CourseImages");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "StudentPayments");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "CourseVideos",
                newName: "CourseVideo");

            migrationBuilder.RenameTable(
                name: "CourseImages",
                newName: "CourseImage");

            migrationBuilder.RenameIndex(
                name: "IX_CourseVideos_CourseId",
                table: "CourseVideo",
                newName: "IX_CourseVideo_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseImages_CourseId",
                table: "CourseImage",
                newName: "IX_CourseImage_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseVideo",
                table: "CourseVideo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseImage",
                table: "CourseImage",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "InstructorCourses",
                columns: table => new
                {
                    InstructorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorCourses", x => new { x.InstructorId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_InstructorCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorCourses_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_Name",
                table: "Students",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_Name",
                table: "Instructors",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorCourses_CourseId",
                table: "InstructorCourses",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseImage_Courses_CourseId",
                table: "CourseImage",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseVideo_Courses_CourseId",
                table: "CourseVideo",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
