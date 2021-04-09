using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactTracing15.Services.Migrations
{
    public partial class spGetById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedureCases = @"Create procedure spGetCaseById
                                    @CaseId int
                                    as
                                    Begin
                                        Select * from Cases
                                        where CaseId = @CaseId
                                    End";
            migrationBuilder.Sql(procedureCases);
            string procedureContacts = @"Create procedure spGetContactById
                                    @ContactId int
                                    as
                                    Begin
                                        Select * from Contacts
                                        where ContactId = @ContactId
                                    End";
            migrationBuilder.Sql(procedureContacts);
            string procedureTracers = @"Create procedure spGetTracerById
                                    @TracerId int
                                    as
                                    Begin
                                        Select * from Tracers
                                        where TracerId = @TracerId
                                    End";
            migrationBuilder.Sql(procedureTracers);
            string procedureTesters = @"Create procedure spGetTesterById
                                    @TesterId int
                                    as
                                    Begin
                                        Select * from Testers
                                        where TesterId = @TesterId
                                    End";
            migrationBuilder.Sql(procedureTesters);
            string procedureTestingCentres = @"Create procedure spGetTestingCentreById
                                    @TestingCentreId int
                                    as
                                    Begin
                                        Select * from TestingCentres
                                        where TestingCentreId = @TestingCentreId
                                    End";
            migrationBuilder.Sql(procedureTestingCentres);
            string procedureTracingCentres = @"Create procedure spGetTracingCentreById
                                    @TracingCentreId int
                                    as
                                    Begin
                                        Select * from TracingCentres
                                        where TracingCentreId = @TracingCentreId
                                    End";
            migrationBuilder.Sql(procedureTracingCentres);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedureCases = @"Drop procedure spGetCaseById";
            migrationBuilder.Sql(procedureCases);
            string procedureContacts = @"Drop procedure spGetContactById ";
            migrationBuilder.Sql(procedureContacts);
            string procedureTracers = @"Drop procedure spGetTracerById ";
            migrationBuilder.Sql(procedureTracers);
            string procedureTesters = @"Drop procedure spGetTesterById";
            migrationBuilder.Sql(procedureTesters);
            string procedureTestingCentres = @"Drop procedure spGetTestingCentreById";
            migrationBuilder.Sql(procedureTestingCentres);
            string procedureTracingCentres = @"Drop procedure spGetTracingCentreById";
            migrationBuilder.Sql(procedureTracingCentres);
        }
    }
}
