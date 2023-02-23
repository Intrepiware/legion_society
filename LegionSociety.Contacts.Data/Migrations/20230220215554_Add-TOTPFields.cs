using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LegionSociety.Contacts.Data.Migrations
{
    public partial class AddTOTPFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TotpConfirmDate",
                table: "Contact",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotpKey",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotpConfirmDate",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "TotpKey",
                table: "Contact");
        }
    }
}
