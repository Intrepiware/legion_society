using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LegionSociety.Contacts.Data.Migrations
{
    public partial class UpdateContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Contact",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Contact",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InactiveDate",
                table: "Contact",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "InactiveDate",
                table: "Contact");
        }
    }
}
