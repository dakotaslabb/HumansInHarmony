using Microsoft.EntityFrameworkCore.Migrations;

namespace HumansInHarmony.Migrations
{
    public partial class UserSocialMedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Snapchat",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Snapchat",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "User");
        }
    }
}
