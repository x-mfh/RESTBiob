using Microsoft.EntityFrameworkCore.Migrations;

namespace Biob.Data.Data.Migrations
{
    public partial class RenamedClassIdsToIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_HallSeats_HallSeatId",
                table: "HallSeats");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "Tickets",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ShowtimeId",
                table: "Showtimes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SeatId",
                table: "Seats",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "HallSeatId",
                table: "HallSeats",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "HallId",
                table: "Halls",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Genres",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MovieGenres",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MovieGenres_Id",
                table: "MovieGenres",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_HallSeats_Id",
                table: "HallSeats",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_MovieGenres_Id",
                table: "MovieGenres");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_HallSeats_Id",
                table: "HallSeats");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MovieGenres");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tickets",
                newName: "TicketId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Showtimes",
                newName: "ShowtimeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Seats",
                newName: "SeatId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "HallSeats",
                newName: "HallSeatId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Halls",
                newName: "HallId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Genres",
                newName: "GenreId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_HallSeats_HallSeatId",
                table: "HallSeats",
                column: "HallSeatId");
        }
    }
}
