using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumansInHarmony.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SongInfo",
                columns: table => new
                {
                    TrackId = table.Column<string>(nullable: false),
                    ArtistName = table.Column<string>(nullable: true),
                    CollectionName = table.Column<string>(nullable: true),
                    TrackName = table.Column<string>(nullable: true),
                    PreviewUrl = table.Column<string>(nullable: true),
                    ArtworkUrl100 = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    UserId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongInfo", x => x.TrackId);
                    table.ForeignKey(
                        name: "FK_SongInfo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SongInfo_User_UserId1",
                        column: x => x.UserId1,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongInfo_UserId",
                table: "SongInfo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SongInfo_UserId1",
                table: "SongInfo",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SongInfo");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
