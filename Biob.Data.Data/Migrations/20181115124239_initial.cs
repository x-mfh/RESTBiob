using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Biob.Data.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 5000, nullable: true),
                    LengthInSeconds = table.Column<int>(nullable: false),
                    Poster = table.Column<string>(nullable: true),
                    Producer = table.Column<string>(maxLength: 255, nullable: true),
                    Actors = table.Column<string>(nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    Released = table.Column<DateTimeOffset>(nullable: false),
                    ThreeDee = table.Column<bool>(nullable: false),
                    AgeRestriction = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
