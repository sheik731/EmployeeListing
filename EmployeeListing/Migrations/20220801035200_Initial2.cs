using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeListing.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Qualification_QualificationsQualificationId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_QualificationsQualificationId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "QualificationsQualificationId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Qualification",
                table: "Employees",
                column: "Qualification");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Qualification_Qualification",
                table: "Employees",
                column: "Qualification",
                principalTable: "Qualification",
                principalColumn: "QualificationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Qualification_Qualification",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Qualification",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "QualificationsQualificationId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_QualificationsQualificationId",
                table: "Employees",
                column: "QualificationsQualificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Qualification_QualificationsQualificationId",
                table: "Employees",
                column: "QualificationsQualificationId",
                principalTable: "Qualification",
                principalColumn: "QualificationId");
        }
    }
}
