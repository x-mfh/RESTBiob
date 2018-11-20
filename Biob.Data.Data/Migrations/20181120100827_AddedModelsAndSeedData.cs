using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Biob.Data.Data.Migrations
{
    public partial class AddedModelsAndSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Movies");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<Guid>(nullable: false),
                    GenreName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Halls",
                columns: table => new
                {
                    HallId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HallNo = table.Column<int>(nullable: false),
                    NoOfSeats = table.Column<int>(nullable: false),
                    ThreeDee = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Halls", x => x.HallId);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    SeatId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RowNo = table.Column<int>(nullable: false),
                    SeatNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.SeatId);
                });

            migrationBuilder.CreateTable(
                name: "Showtimes",
                columns: table => new
                {
                    ShowtimeId = table.Column<Guid>(nullable: false),
                    MovieId = table.Column<Guid>(nullable: false),
                    HallId = table.Column<int>(nullable: false),
                    TimeOfPlaying = table.Column<DateTimeOffset>(nullable: false),
                    ThreeDee = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showtimes", x => x.ShowtimeId);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    ShowtimeId = table.Column<Guid>(nullable: false),
                    HallSeatId = table.Column<int>(nullable: false),
                    Reserved = table.Column<bool>(nullable: false),
                    Paid = table.Column<bool>(nullable: false),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    MovieId = table.Column<Guid>(nullable: false),
                    GenreId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => new { x.MovieId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HallSeats",
                columns: table => new
                {
                    HallSeatId = table.Column<int>(nullable: false),
                    HallId = table.Column<int>(nullable: false),
                    SeatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HallSeats", x => new { x.HallId, x.SeatId });
                    table.UniqueConstraint("AK_HallSeats_HallSeatId", x => x.HallSeatId);
                    table.ForeignKey(
                        name: "FK_HallSeats_Halls_HallId",
                        column: x => x.HallId,
                        principalTable: "Halls",
                        principalColumn: "HallId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HallSeats_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "SeatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "GenreName" },
                values: new object[,]
                {
                    { new Guid("8068cbf6-c595-4733-9c24-8104e8454b4c"), "Horror" },
                    { new Guid("6e0a64bc-7a50-4a6c-9125-8ccf6e54bf70"), "Drama" },
                    { new Guid("01c66e3e-8c25-4f5c-a2c5-512c79d09aa6"), "Comedy" },
                    { new Guid("976a7a24-1c25-4a7f-97c6-1a019c5c148d"), "Romance" },
                    { new Guid("72163c34-3d32-4a78-9701-1f708053978f"), "Action" },
                    { new Guid("e143ebff-a0bd-4107-889f-9bff26eda916"), "Fiction" },
                    { new Guid("0532f0df-c92d-4a10-9d1a-8a5935c541a2"), "Biography" },
                    { new Guid("e48aa8e0-3efc-4acd-a0d5-88c82551807a"), "Fantasy" },
                    { new Guid("76c2fab8-7161-49b7-88c6-f3aaf484ea64"), "Family" }
                });

            migrationBuilder.InsertData(
                table: "Halls",
                columns: new[] { "HallId", "HallNo", "NoOfSeats", "ThreeDee" },
                values: new object[,]
                {
                    { 2, 2, 20, false },
                    { 3, 3, 20, false },
                    { 1, 1, 10, true }
                });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("de9b842d-531a-4f17-ad69-0d3e11cb911d"),
                columns: new[] { "Released", "Title" },
                values: new object[] { new DateTimeOffset(new DateTime(2001, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Harry Potter and the Sorcerer's Stone" });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "SeatId", "RowNo", "SeatNo" },
                values: new object[,]
                {
                    { 12, 2, 2 },
                    { 20, 2, 10 },
                    { 19, 2, 9 },
                    { 18, 2, 8 },
                    { 17, 2, 7 },
                    { 16, 2, 6 },
                    { 15, 2, 5 },
                    { 14, 2, 4 },
                    { 13, 2, 3 },
                    { 11, 2, 1 },
                    { 5, 1, 5 },
                    { 9, 1, 9 },
                    { 8, 1, 8 },
                    { 7, 1, 7 },
                    { 6, 1, 6 },
                    { 4, 1, 4 },
                    { 3, 1, 3 },
                    { 2, 1, 2 },
                    { 1, 1, 1 },
                    { 10, 1, 10 }
                });

            migrationBuilder.InsertData(
                table: "Showtimes",
                columns: new[] { "ShowtimeId", "HallId", "MovieId", "ThreeDee", "TimeOfPlaying" },
                values: new object[,]
                {
                    { new Guid("5e0d5ad3-22b0-4bdc-808c-62b8f50d0796"), 2, new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"), false, new DateTimeOffset(new DateTime(2018, 12, 23, 11, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("092ca7c5-ae83-4a52-a38b-cfc7c8e40e9a"), 1, new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"), false, new DateTimeOffset(new DateTime(2018, 12, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("cf3c5f8e-94ee-494a-b0f1-4a48d9d8291f"), 3, new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"), false, new DateTimeOffset(new DateTime(2018, 12, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "HallSeats",
                columns: new[] { "HallId", "SeatId", "HallSeatId" },
                values: new object[,]
                {
                    { 2, 5, 15 },
                    { 2, 12, 22 },
                    { 1, 12, 7 },
                    { 3, 11, 41 },
                    { 2, 11, 21 },
                    { 1, 11, 6 },
                    { 3, 10, 40 },
                    { 3, 12, 42 },
                    { 2, 10, 20 },
                    { 2, 9, 19 },
                    { 3, 8, 38 },
                    { 2, 8, 18 },
                    { 3, 7, 37 },
                    { 2, 7, 17 },
                    { 3, 6, 36 },
                    { 3, 9, 39 },
                    { 2, 6, 16 },
                    { 1, 13, 8 },
                    { 3, 13, 43 },
                    { 3, 19, 49 },
                    { 2, 19, 29 },
                    { 3, 18, 48 },
                    { 2, 18, 28 },
                    { 3, 17, 47 },
                    { 2, 17, 27 },
                    { 2, 13, 23 },
                    { 3, 16, 46 },
                    { 3, 15, 45 },
                    { 2, 15, 25 },
                    { 1, 15, 10 },
                    { 3, 14, 44 },
                    { 2, 14, 24 },
                    { 1, 14, 9 },
                    { 2, 16, 26 },
                    { 3, 5, 35 },
                    { 3, 20, 50 },
                    { 1, 5, 5 },
                    { 2, 20, 30 },
                    { 1, 1, 1 },
                    { 3, 1, 31 },
                    { 1, 2, 2 },
                    { 2, 2, 12 },
                    { 3, 2, 32 },
                    { 2, 1, 11 },
                    { 2, 3, 13 },
                    { 3, 3, 33 },
                    { 1, 4, 4 },
                    { 2, 4, 14 },
                    { 3, 4, 34 },
                    { 1, 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "MovieGenres",
                columns: new[] { "MovieId", "GenreId" },
                values: new object[,]
                {
                    { new Guid("163c03b3-a057-426d-afa3-1a2631a693e2"), new Guid("976a7a24-1c25-4a7f-97c6-1a019c5c148d") },
                    { new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"), new Guid("6e0a64bc-7a50-4a6c-9125-8ccf6e54bf70") },
                    { new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), new Guid("6e0a64bc-7a50-4a6c-9125-8ccf6e54bf70") },
                    { new Guid("195c5a46-96f9-446b-b4f7-864ab2dc49de"), new Guid("6e0a64bc-7a50-4a6c-9125-8ccf6e54bf70") },
                    { new Guid("174fd8d4-f72b-4059-a7ea-05e687026b0d"), new Guid("6e0a64bc-7a50-4a6c-9125-8ccf6e54bf70") },
                    { new Guid("163c03b3-a057-426d-afa3-1a2631a693e2"), new Guid("6e0a64bc-7a50-4a6c-9125-8ccf6e54bf70") },
                    { new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), new Guid("01c66e3e-8c25-4f5c-a2c5-512c79d09aa6") },
                    { new Guid("163c03b3-a057-426d-afa3-1a2631a693e2"), new Guid("01c66e3e-8c25-4f5c-a2c5-512c79d09aa6") },
                    { new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), new Guid("976a7a24-1c25-4a7f-97c6-1a019c5c148d") },
                    { new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"), new Guid("72163c34-3d32-4a78-9701-1f708053978f") },
                    { new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), new Guid("e48aa8e0-3efc-4acd-a0d5-88c82551807a") },
                    { new Guid("174fd8d4-f72b-4059-a7ea-05e687026b0d"), new Guid("72163c34-3d32-4a78-9701-1f708053978f") },
                    { new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), new Guid("e143ebff-a0bd-4107-889f-9bff26eda916") },
                    { new Guid("174fd8d4-f72b-4059-a7ea-05e687026b0d"), new Guid("e143ebff-a0bd-4107-889f-9bff26eda916") },
                    { new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), new Guid("0532f0df-c92d-4a10-9d1a-8a5935c541a2") },
                    { new Guid("195c5a46-96f9-446b-b4f7-864ab2dc49de"), new Guid("0532f0df-c92d-4a10-9d1a-8a5935c541a2") },
                    { new Guid("de9b842d-531a-4f17-ad69-0d3e11cb911d"), new Guid("e48aa8e0-3efc-4acd-a0d5-88c82551807a") },
                    { new Guid("de9b842d-531a-4f17-ad69-0d3e11cb911d"), new Guid("76c2fab8-7161-49b7-88c6-f3aaf484ea64") },
                    { new Guid("195c5a46-96f9-446b-b4f7-864ab2dc49de"), new Guid("76c2fab8-7161-49b7-88c6-f3aaf484ea64") },
                    { new Guid("163c03b3-a057-426d-afa3-1a2631a693e2"), new Guid("76c2fab8-7161-49b7-88c6-f3aaf484ea64") },
                    { new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), new Guid("72163c34-3d32-4a78-9701-1f708053978f") },
                    { new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), new Guid("8068cbf6-c595-4733-9c24-8104e8454b4c") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HallSeats_SeatId",
                table: "HallSeats",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_GenreId",
                table: "MovieGenres",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HallSeats");

            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "Showtimes");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Halls");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Movies",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("163c03b3-a057-426d-afa3-1a2631a693e2"),
                column: "Genre",
                value: "Drama");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("174fd8d4-f72b-4059-a7ea-05e687026b0d"),
                column: "Genre",
                value: "Action");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("195c5a46-96f9-446b-b4f7-864ab2dc49de"),
                column: "Genre",
                value: "Biography");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"),
                column: "Genre",
                value: "Horror");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"),
                column: "Genre",
                value: "Action");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("de9b842d-531a-4f17-ad69-0d3e11cb911d"),
                columns: new[] { "Genre", "Released", "Title" },
                values: new object[] { "Fantasy", new DateTimeOffset(new DateTime(2001, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Harry Potter and the Philosopher's Stone" });
        }
    }
}
