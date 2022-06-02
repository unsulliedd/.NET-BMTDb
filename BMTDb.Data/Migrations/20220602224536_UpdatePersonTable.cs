using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMTDb.Data.Migrations
{
    public partial class UpdatePersonTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Deathday",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deathday",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Persons");
        }
    }
}
