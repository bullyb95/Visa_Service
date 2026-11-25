using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisaService.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddContactInfoAndVisaDecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_visa_application_officers_officer_id",
                table: "visa_application");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "visa_application",
                newName: "number");

            migrationBuilder.AddColumn<bool>(
                name: "is_visa_issued",
                table: "visa_application",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "applicants",
                type: "character varying(254)",
                maxLength: 254,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phone",
                table: "applicants",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_visa_application_number",
                table: "visa_application",
                column: "number",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_visa_application_officers_officer_id",
                table: "visa_application",
                column: "officer_id",
                principalTable: "officers",
                principalColumn: "officer_id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_visa_application_officers_officer_id",
                table: "visa_application");

            migrationBuilder.DropIndex(
                name: "IX_visa_application_number",
                table: "visa_application");

            migrationBuilder.DropColumn(
                name: "is_visa_issued",
                table: "visa_application");

            migrationBuilder.DropColumn(
                name: "email",
                table: "applicants");

            migrationBuilder.DropColumn(
                name: "phone",
                table: "applicants");

            migrationBuilder.RenameColumn(
                name: "number",
                table: "visa_application",
                newName: "Number");

            migrationBuilder.AddForeignKey(
                name: "FK_visa_application_officers_officer_id",
                table: "visa_application",
                column: "officer_id",
                principalTable: "officers",
                principalColumn: "officer_id");
        }
    }
}
