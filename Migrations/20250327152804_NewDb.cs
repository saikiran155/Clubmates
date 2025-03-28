using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubMates.Web.Migrations
{
    /// <inheritdoc />
    public partial class NewDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClubEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubId = table.Column<int>(type: "int", nullable: true),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventStartDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EventEndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubEvents_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "ClubId");
                });

            migrationBuilder.CreateTable(
                name: "Polls",
                columns: table => new
                {
                    PollId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubId = table.Column<int>(type: "int", nullable: true),
                    ClubEventId = table.Column<int>(type: "int", nullable: true),
                    PollQuestion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PollDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PollStartDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PollEndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsMultipleChoice = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polls", x => x.PollId);
                    table.ForeignKey(
                        name: "FK_Polls_ClubEvents_ClubEventId",
                        column: x => x.ClubEventId,
                        principalTable: "ClubEvents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Polls_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "ClubId");
                });

            migrationBuilder.CreateTable(
                name: "PollResponses",
                columns: table => new
                {
                    ResponseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PollId = table.Column<int>(type: "int", nullable: true),
                    ClubmatesUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollResponses", x => x.ResponseId);
                    table.ForeignKey(
                        name: "FK_PollResponses_AspNetUsers_ClubmatesUserId",
                        column: x => x.ClubmatesUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PollResponses_Polls_PollId",
                        column: x => x.PollId,
                        principalTable: "Polls",
                        principalColumn: "PollId");
                });

            migrationBuilder.CreateTable(
                name: "PollOptions",
                columns: table => new
                {
                    PollOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PollOptionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PollId = table.Column<int>(type: "int", nullable: true),
                    PollResponseResponseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollOptions", x => x.PollOptionId);
                    table.ForeignKey(
                        name: "FK_PollOptions_PollResponses_PollResponseResponseId",
                        column: x => x.PollResponseResponseId,
                        principalTable: "PollResponses",
                        principalColumn: "ResponseId");
                    table.ForeignKey(
                        name: "FK_PollOptions_Polls_PollId",
                        column: x => x.PollId,
                        principalTable: "Polls",
                        principalColumn: "PollId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClubEvents_ClubId",
                table: "ClubEvents",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_PollOptions_PollId",
                table: "PollOptions",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_PollOptions_PollResponseResponseId",
                table: "PollOptions",
                column: "PollResponseResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_PollResponses_ClubmatesUserId",
                table: "PollResponses",
                column: "ClubmatesUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PollResponses_PollId",
                table: "PollResponses",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_Polls_ClubEventId",
                table: "Polls",
                column: "ClubEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Polls_ClubId",
                table: "Polls",
                column: "ClubId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PollOptions");

            migrationBuilder.DropTable(
                name: "PollResponses");

            migrationBuilder.DropTable(
                name: "Polls");

            migrationBuilder.DropTable(
                name: "ClubEvents");
        }
    }
}
