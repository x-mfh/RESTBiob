using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Biob.Data.Data.Migrations
{
    public partial class AddedMovieSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Actors", "AgeRestriction", "Description", "Genre", "LengthInSeconds", "Poster", "Producer", "Released", "ThreeDee", "Title" },
                values: new object[,]
                {
                    { new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"), "Christopher Nolan", 16, "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham. The Dark Knight must accept one of the greatest psychological and physical tests of his ability to fight injustice.", "Action", 9120, "", "Kevin De La Noy", new DateTimeOffset(new DateTime(2005, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), false, "The Dark Knight" },
                    { new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), "Balen, Jason, Mikkel", 16, "Nightmare on earth where students fight to survive on a daily basis.", "Horror", 5400, "", "Jan Eg", new DateTimeOffset(new DateTime(2016, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), false, "SKP" },
                    { new Guid("de9b842d-531a-4f17-ad69-0d3e11cb911d"), "Daniel Radcliffe, Emma Watson, Rupert Grint", 11, "Albus Dumbledore, Minerva McGonagall, and Rubeus Hagrid, professors of Hogwarts School of Witchcraft and Wizardry, deliver a recently orphaned infant named Harry Potter to his only remaining relatives, the Dursleys.", "Fantasy", 9120, "", "David Heyman", new DateTimeOffset(new DateTime(2001, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), true, "Harry Potter and the Philosopher's Stone" },
                    { new Guid("195c5a46-96f9-446b-b4f7-864ab2dc49de"), "Rami Malek, Lucy Boynton, Joseph Mazzello", 11, "A chronicle of the years leading up to Queen's legendary appearance at the Live Aid (1985) concert.", "Biography", 8040, "", "Bryan Singer", new DateTimeOffset(new DateTime(2018, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), false, "Bohemian Rhapsody" },
                    { new Guid("174fd8d4-f72b-4059-a7ea-05e687026b0d"), "Tom Hardy, Michelle Williams, Riz Ahmed", 18, "When Eddie Brock acquires the powers of a symbiote, he will have to release his alter-ego 'Venom' to save his life.", "Action", 6720, "", "Ruben Fleischer", new DateTimeOffset(new DateTime(2018, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), true, "Venom" },
                    { new Guid("163c03b3-a057-426d-afa3-1a2631a693e2"), "Lady Gaga, Bradley Cooper, Sam Elliott", 16, "A musician helps a young singer find fame, even as age and alcoholism send his own career into a downward spiral.", "Drama", 8160, "", "Bradley Cooper", new DateTimeOffset(new DateTime(2018, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), false, "A Star Is Born" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("163c03b3-a057-426d-afa3-1a2631a693e2"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("174fd8d4-f72b-4059-a7ea-05e687026b0d"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("195c5a46-96f9-446b-b4f7-864ab2dc49de"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("de9b842d-531a-4f17-ad69-0d3e11cb911d"));
        }
    }
}
