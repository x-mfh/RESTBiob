using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Biob.Data.Data.Migrations
{
    public partial class AddedGenericWrapperToAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "Tickets",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedOn",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tickets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedOn",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "Showtimes",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedOn",
                table: "Showtimes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Showtimes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedOn",
                table: "Showtimes",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "Seats",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedOn",
                table: "Seats",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Seats",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedOn",
                table: "Seats",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "MovieGenres",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedOn",
                table: "MovieGenres",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MovieGenres",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedOn",
                table: "MovieGenres",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "HallSeats",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedOn",
                table: "HallSeats",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HallSeats",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedOn",
                table: "HallSeats",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "Halls",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedOn",
                table: "Halls",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Halls",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedOn",
                table: "Halls",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "Genres",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedOn",
                table: "Genres",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Genres",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedOn",
                table: "Genres",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_IsDeleted",
                table: "Tickets",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Showtimes_IsDeleted",
                table: "Showtimes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_IsDeleted",
                table: "Seats",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_IsDeleted",
                table: "MovieGenres",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_HallSeats_IsDeleted",
                table: "HallSeats",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Halls_IsDeleted",
                table: "Halls",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_IsDeleted",
                table: "Genres",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tickets_IsDeleted",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Showtimes_IsDeleted",
                table: "Showtimes");

            migrationBuilder.DropIndex(
                name: "IX_Seats_IsDeleted",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_MovieGenres_IsDeleted",
                table: "MovieGenres");

            migrationBuilder.DropIndex(
                name: "IX_HallSeats_IsDeleted",
                table: "HallSeats");

            migrationBuilder.DropIndex(
                name: "IX_Halls_IsDeleted",
                table: "Halls");

            migrationBuilder.DropIndex(
                name: "IX_Genres_IsDeleted",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Showtimes");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Showtimes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Showtimes");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Showtimes");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "HallSeats");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "HallSeats");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HallSeats");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "HallSeats");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Genres");
        }
    }
}
