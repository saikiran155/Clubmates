using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubMates.Web.Migrations
{
    /// <inheritdoc />
    public partial class ClubImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clubs_AspNetUsers_ClubManagerId",
                table: "clubs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clubs",
                table: "clubs");

            migrationBuilder.RenameTable(
                name: "clubs",
                newName: "Clubs");

            migrationBuilder.RenameIndex(
                name: "IX_clubs_ClubManagerId",
                table: "Clubs",
                newName: "IX_Clubs_ClubManagerId");

            migrationBuilder.AddColumn<byte[]>(
                name: "ClubBackground",
                table: "Clubs",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ClubBanner",
                table: "Clubs",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ClubLogo",
                table: "Clubs",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clubs",
                table: "Clubs",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_AspNetUsers_ClubManagerId",
                table: "Clubs",
                column: "ClubManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_AspNetUsers_ClubManagerId",
                table: "Clubs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clubs",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "ClubBackground",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "ClubBanner",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "ClubLogo",
                table: "Clubs");

            migrationBuilder.RenameTable(
                name: "Clubs",
                newName: "clubs");

            migrationBuilder.RenameIndex(
                name: "IX_Clubs_ClubManagerId",
                table: "clubs",
                newName: "IX_clubs_ClubManagerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clubs",
                table: "clubs",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_clubs_AspNetUsers_ClubManagerId",
                table: "clubs",
                column: "ClubManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
