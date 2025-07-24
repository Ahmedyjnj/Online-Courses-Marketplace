using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatestudentpayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentPayments",
                table: "StudentPayments");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "StudentPayments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentPayments",
                table: "StudentPayments",
                columns: new[] { "StudentId", "CourseId", "PaymentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPayments_StudentEnrollments_StudentId_CourseId",
                table: "StudentPayments",
                columns: new[] { "StudentId", "CourseId" },
                principalTable: "StudentEnrollments",
                principalColumns: new[] { "StudentId", "CourseId" },
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentPayments_StudentEnrollments_StudentId_CourseId",
                table: "StudentPayments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentPayments",
                table: "StudentPayments");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "StudentPayments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentPayments",
                table: "StudentPayments",
                columns: new[] { "StudentId", "PaymentId" });
        }
    }
}
