using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactTracing15.Services.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.InsertData(
                table: "TestingCentres",
                columns: new[] { "TestingCentreID", "Name", "Postcode" },
                values: new object[,]
                {
                    { 1, "centre1", "OX1" },
                    { 2, "centre2", "BH17" }
                });

            migrationBuilder.InsertData(
                table: "TracingCentres",
                columns: new[] { "TracingCentreID", "Name", "Postcode" },
                values: new object[,]
                {
                    { 1, "centre1", "OX1" },
                    { 2, "centre2", "BH17" }
                });

            migrationBuilder.InsertData(
                table: "Testers",
                columns: new[] { "TesterID", "TestingCentreID", "Username" },
                values: new object[,]
                {
                    { 1, 1, "tester2@123.com" },
                    { 2, 2, "tester3@123.com" }
                });

            migrationBuilder.InsertData(
                table: "Tracers",
                columns: new[] { "TracerID", "TracingCentreID", "Username" },
                values: new object[,]
                {
                    { 1, 1, "tracer2@123.com" },
                    { 2, 2, "tracer3@123.com" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Testers",
                keyColumn: "TesterID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Testers",
                keyColumn: "TesterID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tracers",
                keyColumn: "TracerID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tracers",
                keyColumn: "TracerID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TestingCentres",
                keyColumn: "TestingCentreID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TestingCentres",
                keyColumn: "TestingCentreID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TracingCentres",
                keyColumn: "TracingCentreID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TracingCentres",
                keyColumn: "TracingCentreID",
                keyValue: 2);
        }
    }
}
