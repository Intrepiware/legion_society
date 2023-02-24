using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LegionSociety.Contacts.Data.Migrations
{
    public partial class AddContactFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "Contact",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "Contact",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Contact",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Contact",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Contact",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DietaryRestrictions",
                table: "Contact",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Contact",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MembershipStartDate",
                table: "Contact",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumberMain",
                table: "Contact",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);


            migrationBuilder.CreateTable(
                name: "ContactFamilyMember",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactId = table.Column<long>(type: "bigint", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PreferredName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactFamilyMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactFamilyMember_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactFamilyMember_ContactId",
                table: "ContactFamilyMember",
                column: "ContactId");

            migrationBuilder.Sql("update Contact set MembershipStartDate = '1/1/2000'");


            migrationBuilder.AlterColumn<DateTime>(
                name: "MembershipStartDate",
                table: "Contact",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactFamilyMember");

            migrationBuilder.DropColumn(
                name: "Address1",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "DietaryRestrictions",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "MembershipStartDate",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "PhoneNumberMain",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Contact");
        }
    }
}
