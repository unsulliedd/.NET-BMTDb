using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMTDb.Data.Migrations
{
    public partial class ChangingCrewMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Crews");

            migrationBuilder.CreateTable(
                name: "MovieCrew",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCrew", x => new { x.MovieId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_MovieCrew_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieCrew_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TvShowCrew",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    TvShowId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvShowCrew", x => new { x.TvShowId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_TvShowCrew_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TvShowCrew_TvShows_TvShowId",
                        column: x => x.TvShowId,
                        principalTable: "TvShows",
                        principalColumn: "TvShowId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieCrew_PersonId",
                table: "MovieCrew",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_TvShowCrew_PersonId",
                table: "TvShowCrew",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieCrew");

            migrationBuilder.DropTable(
                name: "TvShowCrew");

            migrationBuilder.CreateTable(
                name: "Crews",
                columns: table => new
                {
                    CrewId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    TvShowId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crews", x => new { x.CrewId, x.MovieId, x.TvShowId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_Crews_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Crews_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Crews_TvShows_TvShowId",
                        column: x => x.TvShowId,
                        principalTable: "TvShows",
                        principalColumn: "TvShowId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crews_MovieId",
                table: "Crews",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Crews_PersonId",
                table: "Crews",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Crews_TvShowId",
                table: "Crews",
                column: "TvShowId");
        }
    }
}
