using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactTracing15.Services.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestingCentres",
                columns: table => new
                {
                    TestingCentreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestingCentres", x => x.TestingCentreID);
                });

            migrationBuilder.CreateTable(
                name: "TracingCentres",
                columns: table => new
                {
                    TracingCentreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TracingCentres", x => x.TracingCentreID);
                });

            migrationBuilder.CreateTable(
                name: "Testers",
                columns: table => new
                {
                    TesterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestingCentreID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testers", x => x.TesterID);
                    table.ForeignKey(
                        name: "FK_Testers_TestingCentres_TestingCentreID",
                        column: x => x.TestingCentreID,
                        principalTable: "TestingCentres",
                        principalColumn: "TestingCentreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tracers",
                columns: table => new
                {
                    TracerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TracingCentreID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracers", x => x.TracerID);
                    table.ForeignKey(
                        name: "FK_Tracers_TracingCentres_TracingCentreID",
                        column: x => x.TracingCentreID,
                        principalTable: "TracingCentres",
                        principalColumn: "TracingCentreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Forename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TesterID = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Traced = table.Column<bool>(type: "bit", nullable: false),
                    DroppedNum = table.Column<int>(type: "int", nullable: false),
                    Dropped = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TracerID = table.Column<int>(type: "int", nullable: true),
                    SymptomDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemovedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.CaseID);
                    table.ForeignKey(
                        name: "FK_Cases_Testers_TesterID",
                        column: x => x.TesterID,
                        principalTable: "Testers",
                        principalColumn: "TesterID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cases_Tracers_TracerID",
                        column: x => x.TracerID,
                        principalTable: "Tracers",
                        principalColumn: "TracerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Forename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TracedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContactedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemovedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactID);
                    table.ForeignKey(
                        name: "FK_Contacts_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "CaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_TesterID",
                table: "Cases",
                column: "TesterID");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_TracerID",
                table: "Cases",
                column: "TracerID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CaseID",
                table: "Contacts",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Testers_TestingCentreID",
                table: "Testers",
                column: "TestingCentreID");

            migrationBuilder.CreateIndex(
                name: "IX_Tracers_TracingCentreID",
                table: "Tracers",
                column: "TracingCentreID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "Testers");

            migrationBuilder.DropTable(
                name: "Tracers");

            migrationBuilder.DropTable(
                name: "TestingCentres");

            migrationBuilder.DropTable(
                name: "TracingCentres");
        }
    }
}
