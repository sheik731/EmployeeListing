using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeListing.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Classes_EmpClassClassId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmpClassClassId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmpClassClassId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeClass",
                table: "Employees",
                column: "EmployeeClass");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Classes_EmployeeClass",
                table: "Employees",
                column: "EmployeeClass",
                principalTable: "Classes",
                principalColumn: "ClassId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Classes_EmployeeClass",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeClass",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "EmpClassClassId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmpClassClassId",
                table: "Employees",
                column: "EmpClassClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Classes_EmpClassClassId",
                table: "Employees",
                column: "EmpClassClassId",
                principalTable: "Classes",
                principalColumn: "ClassId");
        }
    }
}
