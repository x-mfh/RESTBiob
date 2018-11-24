using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Biob.Data.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    GenreName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Halls",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    HallNo = table.Column<int>(nullable: false),
                    NoOfSeats = table.Column<int>(nullable: false),
                    ThreeDee = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Halls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 5000, nullable: true),
                    LengthInSeconds = table.Column<int>(nullable: false),
                    Poster = table.Column<string>(nullable: true),
                    Producer = table.Column<string>(maxLength: 255, nullable: true),
                    Actors = table.Column<string>(nullable: true),
                    Released = table.Column<DateTimeOffset>(nullable: false),
                    ThreeDee = table.Column<bool>(nullable: false),
                    AgeRestriction = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: false),
                    ShowtimeId = table.Column<Guid>(nullable: false),
                    HallSeatId = table.Column<int>(nullable: false),
                    Reserved = table.Column<bool>(nullable: false),
                    Paid = table.Column<bool>(nullable: false),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    RowNo = table.Column<int>(nullable: false),
                    SeatNo = table.Column<int>(nullable: false),
                    HallId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Halls_HallId",
                        column: x => x.HallId,
                        principalTable: "Halls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    MovieId = table.Column<Guid>(nullable: false),
                    GenreId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => new { x.MovieId, x.GenreId });
                    table.UniqueConstraint("AK_MovieGenres_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Showtimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTimeOffset>(nullable: true),
                    MovieId = table.Column<Guid>(nullable: false),
                    HallId = table.Column<Guid>(nullable: false),
                    TimeOfPlaying = table.Column<DateTimeOffset>(nullable: false),
                    ThreeDee = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showtimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Showtimes_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "GenreName", "IsDeleted", "ModifiedOn" },
                values: new object[,]
                {
                    { new Guid("8068cbf6-c595-4733-9c24-8104e8454b4c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Horror", false, null },
                    { new Guid("e48aa8e0-3efc-4acd-a0d5-88c82551807a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Fantasy", false, null },
                    { new Guid("0532f0df-c92d-4a10-9d1a-8a5935c541a2"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Biography", false, null },
                    { new Guid("e143ebff-a0bd-4107-889f-9bff26eda916"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Fiction", false, null },
                    { new Guid("76c2fab8-7161-49b7-88c6-f3aaf484ea64"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Family", false, null },
                    { new Guid("976a7a24-1c25-4a7f-97c6-1a019c5c148d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Romance", false, null },
                    { new Guid("01c66e3e-8c25-4f5c-a2c5-512c79d09aa6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Comedy", false, null },
                    { new Guid("6e0a64bc-7a50-4a6c-9125-8ccf6e54bf70"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Drama", false, null },
                    { new Guid("72163c34-3d32-4a78-9701-1f708053978f"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Action", false, null }
                });

            migrationBuilder.InsertData(
                table: "Halls",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "HallNo", "IsDeleted", "ModifiedOn", "NoOfSeats", "ThreeDee" },
                values: new object[,]
                {
                    { new Guid("7e9a2751-f1c0-4eb6-a7ec-1319c6dae31e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 1, false, null, 10, true },
                    { new Guid("288aad6a-f042-4b36-a5ae-f950aea18b46"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 2, false, null, 20, false },
                    { new Guid("d90ac9e4-32ba-4b5c-80ea-6eda60c0131b"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 3, false, null, 20, false }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Actors", "AgeRestriction", "CreatedOn", "DeletedOn", "Description", "IsDeleted", "LengthInSeconds", "ModifiedOn", "Poster", "Producer", "Released", "ThreeDee", "Title" },
                values: new object[,]
                {
                    { new Guid("174fd8d4-f72b-4059-a7ea-05e687026b0d"), "Tom Hardy, Michelle Williams, Riz Ahmed", 18, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "When Eddie Brock acquires the powers of a symbiote, he will have to release his alter-ego 'Venom' to save his life.", false, 6720, null, "", "Ruben Fleischer", new DateTimeOffset(new DateTime(2018, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), true, "Venom" },
                    { new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"), "Christopher Nolan", 16, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham. The Dark Knight must accept one of the greatest psychological and physical tests of his ability to fight injustice.", false, 9120, null, "", "Kevin De La Noy", new DateTimeOffset(new DateTime(2005, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), false, "The Dark Knight" },
                    { new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), "Balen, Jason, Mikkel", 16, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Nightmare on earth where students fight to survive on a daily basis.", false, 5400, null, "", "Jan Eg", new DateTimeOffset(new DateTime(2016, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), false, "SKP" },
                    { new Guid("de9b842d-531a-4f17-ad69-0d3e11cb911d"), "Daniel Radcliffe, Emma Watson, Rupert Grint", 11, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Albus Dumbledore, Minerva McGonagall, and Rubeus Hagrid, professors of Hogwarts School of Witchcraft and Wizardry, deliver a recently orphaned infant named Harry Potter to his only remaining relatives, the Dursleys.", false, 9120, null, "", "David Heyman", new DateTimeOffset(new DateTime(2001, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), true, "Harry Potter and the Sorcerer's Stone" },
                    { new Guid("195c5a46-96f9-446b-b4f7-864ab2dc49de"), "Rami Malek, Lucy Boynton, Joseph Mazzello", 11, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "A chronicle of the years leading up to Queen's legendary appearance at the Live Aid (1985) concert.", false, 8040, null, "", "Bryan Singer", new DateTimeOffset(new DateTime(2018, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), false, "Bohemian Rhapsody" },
                    { new Guid("163c03b3-a057-426d-afa3-1a2631a693e2"), "Lady Gaga, Bradley Cooper, Sam Elliott", 16, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "A musician helps a young singer find fame, even as age and alcoholism send his own career into a downward spiral.", false, 8160, null, "", "Bradley Cooper", new DateTimeOffset(new DateTime(2018, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), false, "A Star Is Born" }
                });

            migrationBuilder.InsertData(
                table: "MovieGenres",
                columns: new[] { "MovieId", "GenreId", "CreatedOn", "DeletedOn", "Id", "IsDeleted", "ModifiedOn" },
                values: new object[,]
                {
                    { new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), new Guid("6e0a64bc-7a50-4a6c-9125-8ccf6e54bf70"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 4, false, null },
                    { new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), new Guid("976a7a24-1c25-4a7f-97c6-1a019c5c148d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 6, false, null },
                    { new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), new Guid("72163c34-3d32-4a78-9701-1f708053978f"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 7, false, null },
                    { new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), new Guid("e143ebff-a0bd-4107-889f-9bff26eda916"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 8, false, null },
                    { new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), new Guid("0532f0df-c92d-4a10-9d1a-8a5935c541a2"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 9, false, null },
                    { new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), new Guid("e48aa8e0-3efc-4acd-a0d5-88c82551807a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 10, false, null },
                    { new Guid("de9b842d-531a-4f17-ad69-0d3e11cb911d"), new Guid("e48aa8e0-3efc-4acd-a0d5-88c82551807a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 11, false, null },
                    { new Guid("de9b842d-531a-4f17-ad69-0d3e11cb911d"), new Guid("76c2fab8-7161-49b7-88c6-f3aaf484ea64"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 12, false, null },
                    { new Guid("195c5a46-96f9-446b-b4f7-864ab2dc49de"), new Guid("6e0a64bc-7a50-4a6c-9125-8ccf6e54bf70"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 13, false, null },
                    { new Guid("195c5a46-96f9-446b-b4f7-864ab2dc49de"), new Guid("0532f0df-c92d-4a10-9d1a-8a5935c541a2"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 14, false, null },
                    { new Guid("195c5a46-96f9-446b-b4f7-864ab2dc49de"), new Guid("76c2fab8-7161-49b7-88c6-f3aaf484ea64"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 15, false, null },
                    { new Guid("174fd8d4-f72b-4059-a7ea-05e687026b0d"), new Guid("6e0a64bc-7a50-4a6c-9125-8ccf6e54bf70"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 16, false, null },
                    { new Guid("174fd8d4-f72b-4059-a7ea-05e687026b0d"), new Guid("72163c34-3d32-4a78-9701-1f708053978f"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 17, false, null },
                    { new Guid("174fd8d4-f72b-4059-a7ea-05e687026b0d"), new Guid("e143ebff-a0bd-4107-889f-9bff26eda916"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 18, false, null },
                    { new Guid("163c03b3-a057-426d-afa3-1a2631a693e2"), new Guid("6e0a64bc-7a50-4a6c-9125-8ccf6e54bf70"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 19, false, null },
                    { new Guid("163c03b3-a057-426d-afa3-1a2631a693e2"), new Guid("01c66e3e-8c25-4f5c-a2c5-512c79d09aa6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 20, false, null },
                    { new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), new Guid("01c66e3e-8c25-4f5c-a2c5-512c79d09aa6"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 5, false, null },
                    { new Guid("163c03b3-a057-426d-afa3-1a2631a693e2"), new Guid("976a7a24-1c25-4a7f-97c6-1a019c5c148d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 21, false, null },
                    { new Guid("163c03b3-a057-426d-afa3-1a2631a693e2"), new Guid("76c2fab8-7161-49b7-88c6-f3aaf484ea64"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 22, false, null },
                    { new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"), new Guid("72163c34-3d32-4a78-9701-1f708053978f"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 2, false, null },
                    { new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"), new Guid("6e0a64bc-7a50-4a6c-9125-8ccf6e54bf70"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 1, false, null },
                    { new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), new Guid("8068cbf6-c595-4733-9c24-8104e8454b4c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, 3, false, null }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "HallId", "IsDeleted", "ModifiedOn", "RowNo", "SeatNo" },
                values: new object[,]
                {
                    { new Guid("9c55195a-669b-4366-81cf-7796f014537d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("d90ac9e4-32ba-4b5c-80ea-6eda60c0131b"), false, null, 1, 7 },
                    { new Guid("603ab124-4be6-40fd-9a5e-49bb4a5730db"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("7e9a2751-f1c0-4eb6-a7ec-1319c6dae31e"), false, null, 1, 2 },
                    { new Guid("f9574335-ce2f-48be-a275-1bb2718ded0a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("7e9a2751-f1c0-4eb6-a7ec-1319c6dae31e"), false, null, 1, 3 },
                    { new Guid("245e0e3e-ae50-4e80-b506-01436223f4aa"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("7e9a2751-f1c0-4eb6-a7ec-1319c6dae31e"), false, null, 2, 2 },
                    { new Guid("173595a9-917d-4df9-9a6d-1a5d5b46104e"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("288aad6a-f042-4b36-a5ae-f950aea18b46"), false, null, 1, 4 },
                    { new Guid("70fe1293-99c5-43aa-82df-fd0beaa8076a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("288aad6a-f042-4b36-a5ae-f950aea18b46"), false, null, 1, 5 },
                    { new Guid("a66e828e-75af-4efe-8e2a-225694ce0bb1"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("288aad6a-f042-4b36-a5ae-f950aea18b46"), false, null, 1, 6 },
                    { new Guid("5fd7f7c4-d90f-4d60-8878-067af214a0dc"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("7e9a2751-f1c0-4eb6-a7ec-1319c6dae31e"), false, null, 1, 1 },
                    { new Guid("979fa768-b42a-444c-944e-4295ae64e00d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("d90ac9e4-32ba-4b5c-80ea-6eda60c0131b"), false, null, 1, 8 },
                    { new Guid("cb3314db-47a0-495a-bddb-ff2c8f17395d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("d90ac9e4-32ba-4b5c-80ea-6eda60c0131b"), false, null, 1, 9 },
                    { new Guid("f1a83a75-1770-47b3-b209-40198951d4af"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("d90ac9e4-32ba-4b5c-80ea-6eda60c0131b"), false, null, 1, 10 },
                    { new Guid("10ca7c8f-ca02-4fb5-b53c-90a554345471"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("288aad6a-f042-4b36-a5ae-f950aea18b46"), false, null, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Showtimes",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "HallId", "IsDeleted", "ModifiedOn", "MovieId", "ThreeDee", "TimeOfPlaying" },
                values: new object[,]
                {
                    { new Guid("092ca7c5-ae83-4a52-a38b-cfc7c8e40e9a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("7e9a2751-f1c0-4eb6-a7ec-1319c6dae31e"), false, null, new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"), false, new DateTimeOffset(new DateTime(2018, 12, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("5e0d5ad3-22b0-4bdc-808c-62b8f50d0796"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("288aad6a-f042-4b36-a5ae-f950aea18b46"), false, null, new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"), false, new DateTimeOffset(new DateTime(2018, 12, 23, 11, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("cf3c5f8e-94ee-494a-b0f1-4a48d9d8291f"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("d90ac9e4-32ba-4b5c-80ea-6eda60c0131b"), false, null, new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"), false, new DateTimeOffset(new DateTime(2018, 12, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_IsDeleted",
                table: "Genres",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Halls_IsDeleted",
                table: "Halls",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_GenreId",
                table: "MovieGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_IsDeleted",
                table: "MovieGenres",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_IsDeleted",
                table: "Movies",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_HallId",
                table: "Seats",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_IsDeleted",
                table: "Seats",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Showtimes_IsDeleted",
                table: "Showtimes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Showtimes_MovieId",
                table: "Showtimes",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_IsDeleted",
                table: "Tickets",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Showtimes");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Halls");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
