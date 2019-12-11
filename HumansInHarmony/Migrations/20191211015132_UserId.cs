using Microsoft.EntityFrameworkCore.Migrations;

namespace HumansInHarmony.Migrations
{
    public partial class UserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikedSongs_User_UserId",
                table: "LikedSongs");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "LikedSongs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LikedSongs_User_UserId",
                table: "LikedSongs",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikedSongs_User_UserId",
                table: "LikedSongs");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "LikedSongs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_LikedSongs_User_UserId",
                table: "LikedSongs",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
