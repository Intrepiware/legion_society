using Microsoft.EntityFrameworkCore.Migrations;

namespace LegionSociety.Contacts.Data.Migrations
{
    public partial class AddPasswordAndRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "RoleId",
                table: "Contact",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)1);

            migrationBuilder.Sql("update Contact set password = 'new'");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Contact");
        }
    }
}
