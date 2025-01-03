using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightMicroservice.Migrations.Document
{
    public partial class AddModifiedCountToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModifiedCount",
                table: "Documents",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 1,
                column: "ModifiedCount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 2,
                column: "ModifiedCount",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "DocumentId",
                keyValue: 3,
                column: "ModifiedCount",
                value: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedCount",
                table: "Documents");
        }
    }
}
