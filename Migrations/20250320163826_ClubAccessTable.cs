using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubMates.Web.Migrations
{
    /// <inheritdoc />
    public partial class ClubAccessTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClubsAccesses",
                columns: table => new
                {
                    ClubAccessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubId = table.Column<int>(type: "int", nullable: true),
                    ClubMatesUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClubAccessRole = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubsAccesses", x => x.ClubAccessId);
                    table.ForeignKey(
                        name: "FK_ClubsAccesses_AspNetUsers_ClubMatesUserId",
                        column: x => x.ClubMatesUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClubsAccesses_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "ClubId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClubsAccesses_ClubId",
                table: "ClubsAccesses",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubsAccesses_ClubMatesUserId",
                table: "ClubsAccesses",
                column: "ClubMatesUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubsAccesses");
        }
    }
}
