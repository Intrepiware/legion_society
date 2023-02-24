using Microsoft.EntityFrameworkCore.Migrations;

namespace LegionSociety.Contacts.Data.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_RoleId",
                table: "Contact",
                column: "RoleId");

            migrationBuilder.Sql(@"
insert into Role (Id, Description) values (1, 'User');
insert into Role (Id, Description) values (2, 'Admin');");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Role_RoleId",
                table: "Contact",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Role_RoleId",
                table: "Contact");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Contact_RoleId",
                table: "Contact");
        }
    }
}
