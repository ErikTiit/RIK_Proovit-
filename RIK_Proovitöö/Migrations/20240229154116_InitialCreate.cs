using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RIK_Proovitöö.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegistryCode = table.Column<long>(type: "bigint", nullable: false),
                    CompanyAttendeeAmount = table.Column<int>(type: "int", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    ExtraInfo = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ExtraInfo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Individuals",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonalCode = table.Column<long>(type: "bigint", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    ExtraInfo = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individuals", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EventCompanies",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCompanies", x => new { x.EventID, x.CompanyID });
                    table.ForeignKey(
                        name: "FK_EventCompanies_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventCompanies_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventIndividuals",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false),
                    IndividualID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventIndividuals", x => new { x.EventID, x.IndividualID });
                    table.ForeignKey(
                        name: "FK_EventIndividuals_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventIndividuals_Individuals_IndividualID",
                        column: x => x.IndividualID,
                        principalTable: "Individuals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "ID", "CompanyAttendeeAmount", "CompanyName", "ExtraInfo", "PaymentType", "RegistryCode" },
                values: new object[,]
                {
                    { 1, 43, "Company 1", "Test extra info 1", 1, 77123954950L },
                    { 2, 31, "Company 2", "Test extra info 2", 0, 87565072526L },
                    { 3, 86, "Company 3", "Test extra info 3", 1, 41433813380L },
                    { 4, 15, "Company 4", "Test extra info 4", 1, 42176155251L },
                    { 5, 80, "Company 5", "Test extra info 5", 0, 56112707159L },
                    { 6, 52, "Company 6", "Test extra info 6", 1, 15954689450L },
                    { 7, 41, "Company 7", "Test extra info 7", 1, 39810279648L },
                    { 8, 77, "Company 8", "Test extra info 8", 1, 19969956326L },
                    { 9, 60, "Company 9", "Test extra info 9", 1, 2935804317L },
                    { 10, 25, "Company 10", "Test extra info 10", 1, 25433291606L }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "ID", "Date", "ExtraInfo", "Location", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 2, 17, 41, 16, 66, DateTimeKind.Local).AddTicks(4280), null, "Location1", "Event1" },
                    { 2, new DateTime(2023, 8, 14, 17, 41, 16, 66, DateTimeKind.Local).AddTicks(4320), null, "Location2", "Event2" },
                    { 3, new DateTime(2024, 5, 17, 17, 41, 16, 66, DateTimeKind.Local).AddTicks(4322), null, "Location3", "Event3" },
                    { 4, new DateTime(2024, 1, 20, 17, 41, 16, 66, DateTimeKind.Local).AddTicks(4325), null, "Location4", "Event4" },
                    { 5, new DateTime(2024, 3, 8, 17, 41, 16, 66, DateTimeKind.Local).AddTicks(4327), null, "Location5", "Event5" },
                    { 6, new DateTime(2024, 9, 6, 17, 41, 16, 66, DateTimeKind.Local).AddTicks(4330), null, "Location6", "Event6" },
                    { 7, new DateTime(2023, 7, 10, 17, 41, 16, 66, DateTimeKind.Local).AddTicks(4332), null, "Location7", "Event7" },
                    { 8, new DateTime(2024, 12, 31, 17, 41, 16, 66, DateTimeKind.Local).AddTicks(4354), null, "Location8", "Event8" },
                    { 9, new DateTime(2024, 7, 17, 17, 41, 16, 66, DateTimeKind.Local).AddTicks(4356), null, "Location9", "Event9" },
                    { 10, new DateTime(2024, 10, 2, 17, 41, 16, 66, DateTimeKind.Local).AddTicks(4360), null, "Location10", "Event10" }
                });

            migrationBuilder.InsertData(
                table: "Individuals",
                columns: new[] { "ID", "ExtraInfo", "FirstName", "LastName", "PaymentType", "PersonalCode" },
                values: new object[,]
                {
                    { 1, "Test extra info 1", "First1", "Last1", 0, 46002710232L },
                    { 2, "Test extra info 2", "First2", "Last2", 1, 12416994081L },
                    { 3, "Test extra info 3", "First3", "Last3", 0, 61938462307L },
                    { 4, "Test extra info 4", "First4", "Last4", 0, 79201748549L },
                    { 5, "Test extra info 5", "First5", "Last5", 0, 1177739895L },
                    { 6, "Test extra info 6", "First6", "Last6", 1, 68745945131L },
                    { 7, "Test extra info 7", "First7", "Last7", 1, 42717135335L },
                    { 8, "Test extra info 8", "First8", "Last8", 1, 84201775727L },
                    { 9, "Test extra info 9", "First9", "Last9", 1, 86871840273L },
                    { 10, "Test extra info 10", "First10", "Last10", 0, 47804328631L }
                });

            migrationBuilder.InsertData(
                table: "EventCompanies",
                columns: new[] { "CompanyID", "EventID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 2 },
                    { 4, 2 },
                    { 5, 2 },
                    { 6, 2 },
                    { 7, 2 },
                    { 8, 2 },
                    { 9, 2 },
                    { 10, 2 },
                    { 1, 3 },
                    { 2, 3 },
                    { 3, 3 },
                    { 4, 3 },
                    { 5, 3 },
                    { 6, 3 },
                    { 7, 3 },
                    { 8, 3 },
                    { 9, 3 },
                    { 10, 3 },
                    { 1, 4 },
                    { 2, 4 },
                    { 3, 4 },
                    { 4, 4 },
                    { 5, 4 },
                    { 6, 4 },
                    { 7, 4 },
                    { 8, 4 },
                    { 9, 4 },
                    { 10, 4 },
                    { 1, 5 },
                    { 2, 5 },
                    { 3, 5 },
                    { 4, 5 },
                    { 5, 5 },
                    { 6, 5 },
                    { 7, 5 },
                    { 8, 5 },
                    { 9, 5 },
                    { 10, 5 },
                    { 1, 6 },
                    { 2, 6 },
                    { 3, 6 },
                    { 4, 6 },
                    { 5, 6 },
                    { 6, 6 },
                    { 7, 6 },
                    { 8, 6 },
                    { 9, 6 },
                    { 10, 6 },
                    { 1, 7 },
                    { 2, 7 },
                    { 3, 7 },
                    { 4, 7 },
                    { 5, 7 },
                    { 6, 7 },
                    { 7, 7 },
                    { 8, 7 },
                    { 9, 7 },
                    { 10, 7 },
                    { 1, 8 },
                    { 2, 8 },
                    { 3, 8 },
                    { 4, 8 },
                    { 5, 8 },
                    { 6, 8 },
                    { 7, 8 },
                    { 8, 8 },
                    { 9, 8 },
                    { 10, 8 },
                    { 1, 9 },
                    { 2, 9 },
                    { 3, 9 },
                    { 4, 9 },
                    { 5, 9 },
                    { 6, 9 },
                    { 7, 9 },
                    { 8, 9 },
                    { 9, 9 },
                    { 10, 9 },
                    { 1, 10 },
                    { 2, 10 },
                    { 3, 10 },
                    { 4, 10 },
                    { 5, 10 },
                    { 6, 10 },
                    { 7, 10 },
                    { 8, 10 },
                    { 9, 10 },
                    { 10, 10 }
                });

            migrationBuilder.InsertData(
                table: "EventIndividuals",
                columns: new[] { "EventID", "IndividualID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 6 },
                    { 1, 7 },
                    { 1, 8 },
                    { 1, 9 },
                    { 1, 10 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 2, 6 },
                    { 2, 7 },
                    { 2, 8 },
                    { 2, 9 },
                    { 2, 10 },
                    { 3, 1 },
                    { 3, 2 },
                    { 3, 3 },
                    { 3, 4 },
                    { 3, 5 },
                    { 3, 6 },
                    { 3, 7 },
                    { 3, 8 },
                    { 3, 9 },
                    { 3, 10 },
                    { 4, 1 },
                    { 4, 2 },
                    { 4, 3 },
                    { 4, 4 },
                    { 4, 5 },
                    { 4, 6 },
                    { 4, 7 },
                    { 4, 8 },
                    { 4, 9 },
                    { 4, 10 },
                    { 5, 1 },
                    { 5, 2 },
                    { 5, 3 },
                    { 5, 4 },
                    { 5, 5 },
                    { 5, 6 },
                    { 5, 7 },
                    { 5, 8 },
                    { 5, 9 },
                    { 5, 10 },
                    { 6, 1 },
                    { 6, 2 },
                    { 6, 3 },
                    { 6, 4 },
                    { 6, 5 },
                    { 6, 6 },
                    { 6, 7 },
                    { 6, 8 },
                    { 6, 9 },
                    { 6, 10 },
                    { 7, 1 },
                    { 7, 2 },
                    { 7, 3 },
                    { 7, 4 },
                    { 7, 5 },
                    { 7, 6 },
                    { 7, 7 },
                    { 7, 8 },
                    { 7, 9 },
                    { 7, 10 },
                    { 8, 1 },
                    { 8, 2 },
                    { 8, 3 },
                    { 8, 4 },
                    { 8, 5 },
                    { 8, 6 },
                    { 8, 7 },
                    { 8, 8 },
                    { 8, 9 },
                    { 8, 10 },
                    { 9, 1 },
                    { 9, 2 },
                    { 9, 3 },
                    { 9, 4 },
                    { 9, 5 },
                    { 9, 6 },
                    { 9, 7 },
                    { 9, 8 },
                    { 9, 9 },
                    { 9, 10 },
                    { 10, 1 },
                    { 10, 2 },
                    { 10, 3 },
                    { 10, 4 },
                    { 10, 5 },
                    { 10, 6 },
                    { 10, 7 },
                    { 10, 8 },
                    { 10, 9 },
                    { 10, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventCompanies_CompanyID",
                table: "EventCompanies",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_EventIndividuals_IndividualID",
                table: "EventIndividuals",
                column: "IndividualID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventCompanies");

            migrationBuilder.DropTable(
                name: "EventIndividuals");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Individuals");
        }
    }
}
