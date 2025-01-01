using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightMicroservice.Migrations.ApplicationDb
{
    public partial class AddRolesToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "RefreshTokenExpiryTime",
            //    table: "AspNetUsers",
            //    type: "datetime2",
            //    nullable: true,
            //    oldClrType: typeof(DateTime),
                //oldType: "datetime2");

            migrationBuilder.InsertData(
               table: "AspNetRoles",
               columns: new[] { "Id", "Name", "NormalizedName" },
               values: new object[,]
               {
                    { Guid.NewGuid().ToString(), "Admin", "ADMIN" },
                    { Guid.NewGuid().ToString(), "Pilot", "PILOT" },
                    { Guid.NewGuid().ToString(), "Staff GO", "STAFF GO" },
                    { Guid.NewGuid().ToString(), "Attendant", "ATTENDANT" }
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "RefreshTokenExpiryTime",
            //    table: "AspNetUsers",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            //    oldClrType: typeof(DateTime),
            //    oldType: "datetime2",
            //    oldNullable: true);

            migrationBuilder.DeleteData(
           table: "AspNetRoles",
           keyColumn: "Name",
           keyValues: new object[] { "Admin", "Pilot", "Staff GO", "Attendant" });
        }

    }
}
