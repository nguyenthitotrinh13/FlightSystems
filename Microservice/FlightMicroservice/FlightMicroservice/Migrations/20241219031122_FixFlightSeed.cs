using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightMicroservice.Migrations
{
    public partial class FixFlightSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightID",
                keyValue: "FL001",
                column: "DepartureTime",
                value: new DateTime(2024, 12, 18, 10, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightID",
                keyValue: "FL002",
                column: "DepartureTime",
                value: new DateTime(2024, 12, 18, 11, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightID",
                keyValue: "FL003",
                column: "DepartureTime",
                value: new DateTime(2024, 12, 18, 12, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightID",
                keyValue: "FL001",
                column: "DepartureTime",
                value: new DateTime(2024, 12, 14, 1, 54, 2, 783, DateTimeKind.Local).AddTicks(5291));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightID",
                keyValue: "FL002",
                column: "DepartureTime",
                value: new DateTime(2024, 12, 14, 2, 54, 2, 783, DateTimeKind.Local).AddTicks(5401));

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightID",
                keyValue: "FL003",
                column: "DepartureTime",
                value: new DateTime(2024, 12, 14, 3, 54, 2, 783, DateTimeKind.Local).AddTicks(5403));
        }
    }
}
