using Microsoft.EntityFrameworkCore.Migrations;

namespace HumansInHarmony.Migrations
{
    public partial class UserID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DislikedSongs_User_UserId",
                table: "DislikedSongs");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "DislikedSongs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DislikedSongs_User_UserId",
                table: "DislikedSongs",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DislikedSongs_User_UserId",
                table: "DislikedSongs");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "DislikedSongs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_DislikedSongs_User_UserId",
                table: "DislikedSongs",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
