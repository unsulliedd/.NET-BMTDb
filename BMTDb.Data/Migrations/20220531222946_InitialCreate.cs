using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMTDb.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    EpisodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Episode_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Episode_Number = table.Column<int>(type: "int", nullable: true),
                    Episode_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.EpisodeId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MovieInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trailer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoviePoster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieBackdrop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieTagline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieRatings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RunTime = table.Column<int>(type: "int", nullable: true),
                    Budget = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IMDBId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TMDbId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "Networks",
                columns: table => new
                {
                    NetworkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Network_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Netwotk_logo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Networks", x => x.NetworkId);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imdb_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    SeasonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Air_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Season_poster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Season_Number = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.SeasonId);
                });

            migrationBuilder.CreateTable(
                name: "Studios",
                columns: table => new
                {
                    StudioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Studio_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Studio_logo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studios", x => x.StudioId);
                });

            migrationBuilder.CreateTable(
                name: "TvShows",
                columns: table => new
                {
                    TvShowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Release_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TvShow_Info = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number_of_Seasons = table.Column<int>(type: "int", nullable: true),
                    Number_of_Episodes = table.Column<int>(type: "int", nullable: true),
                    TvShow_Trailer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TvShow_Poster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TvShow_Backdrop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TvShow_Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TvShow_Tagline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TvShow_Ratings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Episode_RunTime = table.Column<int>(type: "int", nullable: true),
                    TvShow_Budget = table.Column<int>(type: "int", nullable: true),
                    TvShow_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IMDBId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TMDbId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvShows", x => x.TvShowId);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenre",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenre", x => new { x.MovieId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_MovieGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenre_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeasonEpisode",
                columns: table => new
                {
                    SeasonId = table.Column<int>(type: "int", nullable: false),
                    EpisodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonEpisode", x => new { x.SeasonId, x.EpisodeId });
                    table.ForeignKey(
                        name: "FK_SeasonEpisode_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "EpisodeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeasonEpisode_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieStudio",
                columns: table => new
                {
                    StudioId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieStudio", x => new { x.MovieId, x.StudioId });
                    table.ForeignKey(
                        name: "FK_MovieStudio_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieStudio_Studios_StudioId",
                        column: x => x.StudioId,
                        principalTable: "Studios",
                        principalColumn: "StudioId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "TvShowGenre",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    TvShowId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvShowGenre", x => new { x.TvShowId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_TvShowGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TvShowGenre_TvShows_TvShowId",
                        column: x => x.TvShowId,
                        principalTable: "TvShows",
                        principalColumn: "TvShowId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TvShowNetwork",
                columns: table => new
                {
                    NetworkId = table.Column<int>(type: "int", nullable: false),
                    TvShowId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvShowNetwork", x => new { x.TvShowId, x.NetworkId });
                    table.ForeignKey(
                        name: "FK_TvShowNetwork_Networks_NetworkId",
                        column: x => x.NetworkId,
                        principalTable: "Networks",
                        principalColumn: "NetworkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TvShowNetwork_TvShows_TvShowId",
                        column: x => x.TvShowId,
                        principalTable: "TvShows",
                        principalColumn: "TvShowId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TvShowSeason",
                columns: table => new
                {
                    SeasonId = table.Column<int>(type: "int", nullable: false),
                    TvShowId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvShowSeason", x => new { x.TvShowId, x.SeasonId });
                    table.ForeignKey(
                        name: "FK_TvShowSeason_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TvShowSeason_TvShows_TvShowId",
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

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenre_GenreId",
                table: "MovieGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieStudio_StudioId",
                table: "MovieStudio",
                column: "StudioId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonEpisode_EpisodeId",
                table: "SeasonEpisode",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_TvShowGenre_GenreId",
                table: "TvShowGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_TvShowNetwork_NetworkId",
                table: "TvShowNetwork",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_TvShowSeason_SeasonId",
                table: "TvShowSeason",
                column: "SeasonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Crews");

            migrationBuilder.DropTable(
                name: "MovieGenre");

            migrationBuilder.DropTable(
                name: "MovieStudio");

            migrationBuilder.DropTable(
                name: "SeasonEpisode");

            migrationBuilder.DropTable(
                name: "TvShowGenre");

            migrationBuilder.DropTable(
                name: "TvShowNetwork");

            migrationBuilder.DropTable(
                name: "TvShowSeason");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Studios");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Networks");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "TvShows");
        }
    }
}
