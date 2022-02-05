using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LegionSociety.Contacts.Data.Migrations
{
    public partial class AddInvitationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvitingContactId = table.Column<long>(type: "bigint", nullable: false),
                    RedeemDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContactId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitation_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invitation_Contact_InvitingContactId",
                        column: x => x.InvitingContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_ContactId",
                table: "Invitation",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_InvitingContactId",
                table: "Invitation",
                column: "InvitingContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_PublicId",
                table: "Invitation",
                column: "PublicId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invitation");
        }
    }
}
