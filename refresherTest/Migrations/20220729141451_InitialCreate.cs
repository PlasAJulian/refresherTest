using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace refresherTest.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    countryName = table.Column<string>(nullable: true),
                    regionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    departmentName = table.Column<string>(nullable: true),
                    locationID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "dependent",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    relationship = table.Column<string>(nullable: true),
                    employeeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dependent", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    phoneNum = table.Column<string>(nullable: true),
                    hireDate = table.Column<DateTime>(nullable: false),
                    jobID = table.Column<int>(nullable: false),
                    income = table.Column<decimal>(nullable: false),
                    managereID = table.Column<string>(nullable: true),
                    departmentID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "job",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    jobTitle = table.Column<string>(nullable: true),
                    minSalary = table.Column<decimal>(nullable: true),
                    maxSalary = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "location",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    address = table.Column<string>(nullable: true),
                    postalCode = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    state = table.Column<string>(nullable: true),
                    countryID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_location", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "region",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    regionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_region", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropTable(
                name: "department");

            migrationBuilder.DropTable(
                name: "dependent");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "job");

            migrationBuilder.DropTable(
                name: "location");

            migrationBuilder.DropTable(
                name: "region");
        }
    }
}
