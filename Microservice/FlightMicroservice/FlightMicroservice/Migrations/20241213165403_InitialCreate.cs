using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightMicroservice.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightID);
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightID", "DepartureTime", "Destination", "FlightNumber" },
                values: new object[] { "FL001", new DateTime(2024, 12, 14, 1, 54, 2, 783, DateTimeKind.Local).AddTicks(5291), "New York", "UA123" });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightID", "DepartureTime", "Destination", "FlightNumber" },
                values: new object[] { "FL002", new DateTime(2024, 12, 14, 2, 54, 2, 783, DateTimeKind.Local).AddTicks(5401), "Los Angeles", "DL456" });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightID", "DepartureTime", "Destination", "FlightNumber" },
                values: new object[] { "FL003", new DateTime(2024, 12, 14, 3, 54, 2, 783, DateTimeKind.Local).AddTicks(5403), "Chicago", "AA789" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
