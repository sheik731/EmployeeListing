using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeListing.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    className = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassId);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    GenderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.GenderId);
                });

            migrationBuilder.CreateTable(
                name: "Qualification",
                columns: table => new
                {
                    QualificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QualificationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualification", x => x.QualificationId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisterNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeClass = table.Column<int>(type: "int", nullable: false),
                    Qualification = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    MailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    isAdminAccepted = table.Column<bool>(type: "bit", nullable: false),
                    EmpClassClassId = table.Column<int>(type: "int", nullable: true),
                    QualificationsQualificationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Classes_EmpClassClassId",
                        column: x => x.EmpClassClassId,
                        principalTable: "Classes",
                        principalColumn: "ClassId");
                    table.ForeignKey(
                        name: "FK_Employees_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "GenderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Qualification_QualificationsQualificationId",
                        column: x => x.QualificationsQualificationId,
                        principalTable: "Qualification",
                        principalColumn: "QualificationId");
                    table.ForeignKey(
                        name: "FK_Employees_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "ClassId", "className", "isActive" },
                values: new object[,]
                {
                    { 1, 6, true },
                    { 2, 7, true },
                    { 3, 8, true }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "GenderId", "GenderName", "isActive" },
                values: new object[,]
                {
                    { 1, "Male", true },
                    { 2, "Female", true },
                    { 3, "Others", true }
                });

            migrationBuilder.InsertData(
                table: "Qualification",
                columns: new[] { "QualificationId", "QualificationName", "isActive" },
                values: new object[,]
                {
                    { 1, "M.A Tamil", true },
                    { 2, "B.A Tamil", true },
                    { 3, "M.A English", true },
                    { 4, "B.A English", true },
                    { 5, "M.A B.Ed", true },
                    { 6, "B.A B.Ed", true }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName", "isActive" },
                values: new object[,]
                {
                    { 1, "Admin", true },
                    { 2, "HM", true },
                    { 3, "User", true }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "SubjectName", "isActive" },
                values: new object[,]
                {
                    { 1, "Tamil", true },
                    { 2, "English", true },
                    { 3, "Maths", true }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateOfBirth", "EmpClassClassId", "EmployeeClass", "EmployeeName", "GenderId", "MailId", "Password", "Qualification", "QualificationsQualificationId", "RegisterNumber", "RoleId", "SubjectId", "isActive", "isAdminAccepted" },
                values: new object[] { 1, new DateTime(1990, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Prithvi", 1, "prithvi.palani@aspiresys.com", "Pass@12345", 1, null, "PSNA0001", 1, 1, true, false });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateOfBirth", "EmpClassClassId", "EmployeeClass", "EmployeeName", "GenderId", "MailId", "Password", "Qualification", "QualificationsQualificationId", "RegisterNumber", "RoleId", "SubjectId", "isActive", "isAdminAccepted" },
                values: new object[] { 2, new DateTime(1993, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, "Vinodhini", 2, "vinoth.jayakumar@aspiresys.com", "Pass@12345", 1, null, "PSNA0002", 2, 2, true, false });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateOfBirth", "EmpClassClassId", "EmployeeClass", "EmployeeName", "GenderId", "MailId", "Password", "Qualification", "QualificationsQualificationId", "RegisterNumber", "RoleId", "SubjectId", "isActive", "isAdminAccepted" },
                values: new object[] { 3, new DateTime(1992, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Sheik", 1, "sheik.farid@aspiresys.com", "Pass@12345", 2, null, "PSNA0003", 3, 3, true, false });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmpClassClassId",
                table: "Employees",
                column: "EmpClassClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GenderId",
                table: "Employees",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_QualificationsQualificationId",
                table: "Employees",
                column: "QualificationsQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SubjectId",
                table: "Employees",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Qualification");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
