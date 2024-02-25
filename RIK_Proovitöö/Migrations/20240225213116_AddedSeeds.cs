using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RIK_Proovitöö.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventAttendees_Events_EventId",
                table: "EventAttendees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventAttendees",
                table: "EventAttendees");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "EventAttendees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IndividualId",
                table: "EventAttendees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EventAttendees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventAttendees",
                table: "EventAttendees",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "AttendeeAmount", "CompanyName", "ExtraInfo", "PaymentType", "RegisteryCode" },
                values: new object[,]
                {
                    { 1, 10, "Company 1", "Test extra info 1", 0, 123456 },
                    { 2, 20, "Company 2", "Test extra info 2", 1, 654321 }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "ExtraInfo", "Location", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test extra info 1", "Location 1", "Event 1" },
                    { 2, new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test extra info 2", "Location 2", "Event 2" }
                });

            migrationBuilder.InsertData(
                table: "Individuals",
                columns: new[] { "Id", "ExtraInfo", "FirstName", "LastName", "PaymentType", "PersonalCode" },
                values: new object[,]
                {
                    { 1, "Test extra info 1", "John", "Doe", 0, 123456 },
                    { 2, "Test extra info 2", "Jane", "Doe", 1, 654321 }
                });

            migrationBuilder.InsertData(
                table: "EventAttendees",
                columns: new[] { "Id", "CompanyId", "EventId", "IndividualId" },
                values: new object[,]
                {
                    { 1, null, 1, 1 },
                    { 2, 1, 1, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EventAttendees_Events_Id",
                table: "EventAttendees",
                column: "Id",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventAttendees_Events_Id",
                table: "EventAttendees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventAttendees",
                table: "EventAttendees");

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EventAttendees",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EventAttendees",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Individuals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Individuals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EventAttendees");

            migrationBuilder.AlterColumn<int>(
                name: "IndividualId",
                table: "EventAttendees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "EventAttendees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventAttendees",
                table: "EventAttendees",
                columns: new[] { "EventId", "IndividualId", "CompanyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EventAttendees_Events_EventId",
                table: "EventAttendees",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
