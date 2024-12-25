using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightMicroservice.Migrations.Document
{
    public partial class AddDocumentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentId);
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "DocumentId", "CreatedDate", "DocumentType", "FilePath", "FlightId" },
                values: new object[] { 1, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PDF", "path/to/document1.pdf", 1 });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "DocumentId", "CreatedDate", "DocumentType", "FilePath", "FlightId" },
                values: new object[] { 2, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Word", "path/to/document2.docx", 2 });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "DocumentId", "CreatedDate", "DocumentType", "FilePath", "FlightId" },
                values: new object[] { 3, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Image", "path/to/document3.jpg", 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}
