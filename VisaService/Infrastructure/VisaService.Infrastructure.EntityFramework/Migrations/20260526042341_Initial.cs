using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisaService.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "applicants",
                columns: table => new
                {
                    applicant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    middle_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicants", x => x.applicant_id);
                });

            migrationBuilder.CreateTable(
                name: "officers",
                columns: table => new
                {
                    officer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    middle_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    position = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_officers", x => x.officer_id);
                });

            migrationBuilder.CreateTable(
                name: "visa_application",
                columns: table => new
                {
                    application_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    applicant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    officer_id = table.Column<Guid>(type: "uuid", nullable: true),
                    form_filled = table.Column<bool>(type: "boolean", nullable: false),
                    documents_checked = table.Column<bool>(type: "boolean", nullable: false),
                    result_checked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_visa_application", x => x.application_id);
                    table.ForeignKey(
                        name: "FK_visa_application_applicants_applicant_id",
                        column: x => x.applicant_id,
                        principalTable: "applicants",
                        principalColumn: "applicant_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_visa_application_officers_officer_id",
                        column: x => x.officer_id,
                        principalTable: "officers",
                        principalColumn: "officer_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_visa_application_applicant_id",
                table: "visa_application",
                column: "applicant_id");

            migrationBuilder.CreateIndex(
                name: "IX_visa_application_officer_id",
                table: "visa_application",
                column: "officer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "visa_application");

            migrationBuilder.DropTable(
                name: "applicants");

            migrationBuilder.DropTable(
                name: "officers");
        }
    }
}
