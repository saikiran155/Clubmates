using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubMates.Web.Migrations
{
    /// <inheritdoc />
    public partial class ClubTableInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clubs",
                columns: table => new
                {
                    ClubId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClubDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CLUBCATEGORY = table.Column<int>(type: "int", nullable: false),
                    CLUBTYPE = table.Column<int>(type: "int", nullable: false),
                    ClubRules = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClubManagerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClubContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClubEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clubs", x => x.ClubId);
                    table.ForeignKey(
                        name: "FK_clubs_AspNetUsers_ClubManagerId",
                        column: x => x.ClubManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_clubs_ClubManagerId",
                table: "clubs",
                column: "ClubManagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clubs");
        }
    }
}
