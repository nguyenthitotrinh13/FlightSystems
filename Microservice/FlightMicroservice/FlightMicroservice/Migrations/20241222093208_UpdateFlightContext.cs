using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightMicroservice.Migrations
{
    public partial class UpdateFlightContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the foreign key constraint and any other constraints that reference the FlightID column
            migrationBuilder.Sql("ALTER TABLE Flights DROP CONSTRAINT IF EXISTS PK_Flights");

            // Drop the column 'FlightID'
            migrationBuilder.DropColumn(
                name: "FlightID",
                table: "Flights");

            // Add the 'FlightID' column with IDENTITY property
            migrationBuilder.AddColumn<int>(
                name: "FlightID",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // Optional: Insert any required data
            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightNumber", "Destination", "DepartureTime" },
                values: new object[] { "UA123", "New York", new DateTime(2024, 12, 18, 10, 0, 0) });

            // Add other necessary data here
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Rollback: Drop the new FlightID column
            migrationBuilder.DropColumn(
                name: "FlightID",
                table: "Flights");

            // Recreate the original FlightID column as a string (or adjust as needed)
            migrationBuilder.AddColumn<string>(
                name: "FlightID",
                table: "Flights",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");
        }
    }
}
