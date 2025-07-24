using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatestudentEnrollments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentPayments_Payments_PaymentId",
                table: "StudentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPayments_StudentEnrollments_StudentId_CourseId",
                table: "StudentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPayments_Students_StudentId",
                table: "StudentPayments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentPayments",
                table: "StudentPayments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentEnrollments",
                table: "StudentEnrollments");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "StudentPayments",
                newName: "StudentEnrollmentId");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "StudentPayments",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "StudentEnrollments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentPayments",
                table: "StudentPayments",
                columns: new[] { "StudentEnrollmentId", "PaymentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentEnrollments",
                table: "StudentEnrollments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPayments_StudentId",
                table: "StudentPayments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEnrollments_StudentId_CourseId",
                table: "StudentEnrollments",
                columns: new[] { "StudentId", "CourseId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPayments_Payments_PaymentId",
                table: "StudentPayments",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPayments_StudentEnrollments_StudentEnrollmentId",
                table: "StudentPayments",
                column: "StudentEnrollmentId",
                principalTable: "StudentEnrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPayments_Students_StudentId",
                table: "StudentPayments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentPayments_Payments_PaymentId",
                table: "StudentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPayments_StudentEnrollments_StudentEnrollmentId",
                table: "StudentPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPayments_Students_StudentId",
                table: "StudentPayments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentPayments",
                table: "StudentPayments");

            migrationBuilder.DropIndex(
                name: "IX_StudentPayments_StudentId",
                table: "StudentPayments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentEnrollments",
                table: "StudentEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_StudentEnrollments_StudentId_CourseId",
                table: "StudentEnrollments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentEnrollments");

            migrationBuilder.RenameColumn(
                name: "StudentEnrollmentId",
                table: "StudentPayments",
                newName: "CourseId");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "StudentPayments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentPayments",
                table: "StudentPayments",
                columns: new[] { "StudentId", "CourseId", "PaymentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentEnrollments",
                table: "StudentEnrollments",
                columns: new[] { "StudentId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPayments_Payments_PaymentId",
                table: "StudentPayments",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPayments_StudentEnrollments_StudentId_CourseId",
                table: "StudentPayments",
                columns: new[] { "StudentId", "CourseId" },
                principalTable: "StudentEnrollments",
                principalColumns: new[] { "StudentId", "CourseId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPayments_Students_StudentId",
                table: "StudentPayments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
