﻿// <auto-generated />
using System;
using Biob.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Biob.Data.Data.Migrations
{
    [DbContext(typeof(BiobDataContext))]
    [Migration("20181120104356_RenamedClassIdsToIds")]
    partial class RenamedClassIdsToIds
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Biob.Data.Models.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GenreName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new { Id = new Guid("8068cbf6-c595-4733-9c24-8104e8454b4c"), GenreName = "Horror" },
                        new { Id = new Guid("6e0a64bc-7a50-4a6c-9125-8ccf6e54bf70"), GenreName = "Drama" },
                        new { Id = new Guid("01c66e3e-8c25-4f5c-a2c5-512c79d09aa6"), GenreName = "Comedy" },
                        new { Id = new Guid("976a7a24-1c25-4a7f-97c6-1a019c5c148d"), GenreName = "Romance" },
                        new { Id = new Guid("72163c34-3d32-4a78-9701-1f708053978f"), GenreName = "Action" },
                        new { Id = new Guid("e143ebff-a0bd-4107-889f-9bff26eda916"), GenreName = "Fiction" },
                        new { Id = new Guid("0532f0df-c92d-4a10-9d1a-8a5935c541a2"), GenreName = "Biography" },
                        new { Id = new Guid("e48aa8e0-3efc-4acd-a0d5-88c82551807a"), GenreName = "Fantasy" },
                        new { Id = new Guid("76c2fab8-7161-49b7-88c6-f3aaf484ea64"), GenreName = "Family" }
                    );
                });

            modelBuilder.Entity("Biob.Data.Models.Hall", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HallNo");

                    b.Property<int>("NoOfSeats");

                    b.Property<bool>("ThreeDee");

                    b.HasKey("Id");

                    b.ToTable("Halls");

                    b.HasData(
                        new { Id = 1, HallNo = 1, NoOfSeats = 10, ThreeDee = true },
                        new { Id = 2, HallNo = 2, NoOfSeats = 20, ThreeDee = false },
                        new { Id = 3, HallNo = 3, NoOfSeats = 20, ThreeDee = false }
                    );
                });

            modelBuilder.Entity("Biob.Data.Models.HallSeat", b =>
                {
                    b.Property<int>("HallId");

                    b.Property<int>("SeatId");

                    b.Property<int>("Id");

                    b.HasKey("HallId", "SeatId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("SeatId");

                    b.ToTable("HallSeats");

                    b.HasData(
                        new { HallId = 1, SeatId = 1, Id = 1 },
                        new { HallId = 1, SeatId = 2, Id = 2 },
                        new { HallId = 1, SeatId = 3, Id = 3 },
                        new { HallId = 1, SeatId = 4, Id = 4 },
                        new { HallId = 1, SeatId = 5, Id = 5 },
                        new { HallId = 1, SeatId = 11, Id = 6 },
                        new { HallId = 1, SeatId = 12, Id = 7 },
                        new { HallId = 1, SeatId = 13, Id = 8 },
                        new { HallId = 1, SeatId = 14, Id = 9 },
                        new { HallId = 1, SeatId = 15, Id = 10 },
                        new { HallId = 2, SeatId = 1, Id = 11 },
                        new { HallId = 2, SeatId = 2, Id = 12 },
                        new { HallId = 2, SeatId = 3, Id = 13 },
                        new { HallId = 2, SeatId = 4, Id = 14 },
                        new { HallId = 2, SeatId = 5, Id = 15 },
                        new { HallId = 2, SeatId = 6, Id = 16 },
                        new { HallId = 2, SeatId = 7, Id = 17 },
                        new { HallId = 2, SeatId = 8, Id = 18 },
                        new { HallId = 2, SeatId = 9, Id = 19 },
                        new { HallId = 2, SeatId = 10, Id = 20 },
                        new { HallId = 2, SeatId = 11, Id = 21 },
                        new { HallId = 2, SeatId = 12, Id = 22 },
                        new { HallId = 2, SeatId = 13, Id = 23 },
                        new { HallId = 2, SeatId = 14, Id = 24 },
                        new { HallId = 2, SeatId = 15, Id = 25 },
                        new { HallId = 2, SeatId = 16, Id = 26 },
                        new { HallId = 2, SeatId = 17, Id = 27 },
                        new { HallId = 2, SeatId = 18, Id = 28 },
                        new { HallId = 2, SeatId = 19, Id = 29 },
                        new { HallId = 2, SeatId = 20, Id = 30 },
                        new { HallId = 3, SeatId = 1, Id = 31 },
                        new { HallId = 3, SeatId = 2, Id = 32 },
                        new { HallId = 3, SeatId = 3, Id = 33 },
                        new { HallId = 3, SeatId = 4, Id = 34 },
                        new { HallId = 3, SeatId = 5, Id = 35 },
                        new { HallId = 3, SeatId = 6, Id = 36 },
                        new { HallId = 3, SeatId = 7, Id = 37 },
                        new { HallId = 3, SeatId = 8, Id = 38 },
                        new { HallId = 3, SeatId = 9, Id = 39 },
                        new { HallId = 3, SeatId = 10, Id = 40 },
                        new { HallId = 3, SeatId = 11, Id = 41 },
                        new { HallId = 3, SeatId = 12, Id = 42 },
                        new { HallId = 3, SeatId = 13, Id = 43 },
                        new { HallId = 3, SeatId = 14, Id = 44 },
                        new { HallId = 3, SeatId = 15, Id = 45 },
                        new { HallId = 3, SeatId = 16, Id = 46 },
                        new { HallId = 3, SeatId = 17, Id = 47 },
                        new { HallId = 3, SeatId = 18, Id = 48 },
                        new { HallId = 3, SeatId = 19, Id = 49 },
                        new { HallId = 3, SeatId = 20, Id = 50 }
                    );
                });

            modelBuilder.Entity("Biob.Data.Models.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Actors");

                    b.Property<int>("AgeRestriction");

                    b.Property<string>("Description")
                        .HasMaxLength(5000);

                    b.Property<int>("LengthInSeconds");

                    b.Property<string>("Poster");

                    b.Property<string>("Producer")
                        .HasMaxLength(255);

                    b.Property<DateTimeOffset>("Released");

                    b.Property<bool>("ThreeDee");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new { Id = new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"), Actors = "Christopher Nolan", AgeRestriction = 16, Description = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham. The Dark Knight must accept one of the greatest psychological and physical tests of his ability to fight injustice.", LengthInSeconds = 9120, Poster = "", Producer = "Kevin De La Noy", Released = new DateTimeOffset(new DateTime(2005, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), ThreeDee = false, Title = "The Dark Knight" },
                        new { Id = new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), Actors = "Balen, Jason, Mikkel", AgeRestriction = 16, Description = "Nightmare on earth where students fight to survive on a daily basis.", LengthInSeconds = 5400, Poster = "", Producer = "Jan Eg", Released = new DateTimeOffset(new DateTime(2016, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), ThreeDee = false, Title = "SKP" },
                        new { Id = new Guid("de9b842d-531a-4f17-ad69-0d3e11cb911d"), Actors = "Daniel Radcliffe, Emma Watson, Rupert Grint", AgeRestriction = 11, Description = "Albus Dumbledore, Minerva McGonagall, and Rubeus Hagrid, professors of Hogwarts School of Witchcraft and Wizardry, deliver a recently orphaned infant named Harry Potter to his only remaining relatives, the Dursleys.", LengthInSeconds = 9120, Poster = "", Producer = "David Heyman", Released = new DateTimeOffset(new DateTime(2001, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), ThreeDee = true, Title = "Harry Potter and the Sorcerer's Stone" },
                        new { Id = new Guid("195c5a46-96f9-446b-b4f7-864ab2dc49de"), Actors = "Rami Malek, Lucy Boynton, Joseph Mazzello", AgeRestriction = 11, Description = "A chronicle of the years leading up to Queen's legendary appearance at the Live Aid (1985) concert.", LengthInSeconds = 8040, Poster = "", Producer = "Bryan Singer", Released = new DateTimeOffset(new DateTime(2018, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), ThreeDee = false, Title = "Bohemian Rhapsody" },
                        new { Id = new Guid("174fd8d4-f72b-4059-a7ea-05e687026b0d"), Actors = "Tom Hardy, Michelle Williams, Riz Ahmed", AgeRestriction = 18, Description = "When Eddie Brock acquires the powers of a symbiote, he will have to release his alter-ego 'Venom' to save his life.", LengthInSeconds = 6720, Poster = "", Producer = "Ruben Fleischer", Released = new DateTimeOffset(new DateTime(2018, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), ThreeDee = true, Title = "Venom" },
                        new { Id = new Guid("163c03b3-a057-426d-afa3-1a2631a693e2"), Actors = "Lady Gaga, Bradley Cooper, Sam Elliott", AgeRestriction = 16, Description = "A musician helps a young singer find fame, even as age and alcoholism send his own career into a downward spiral.", LengthInSeconds = 8160, Poster = "", Producer = "Bradley Cooper", Released = new DateTimeOffset(new DateTime(2018, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), ThreeDee = false, Title = "A Star Is Born" }
                    );
                });

            modelBuilder.Entity("Biob.Data.Models.MovieGenre", b =>
                {
                    b.Property<Guid>("MovieId");

                    b.Property<Guid>("GenreId");

                    b.Property<int>("Id");

                    b.HasKey("MovieId", "GenreId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("GenreId");

                    b.ToTable("MovieGenres");

                    b.HasData(
                        new { MovieId = new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"), GenreId = new Guid("6e0a64bc-7a50-4a6c-9125-8ccf6e54bf70"), Id = 1 },
                        new { MovieId = new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"), GenreId = new Guid("72163c34-3d32-4a78-9701-1f708053978f"), Id = 2 },
                        new { MovieId = new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), GenreId = new Guid("8068cbf6-c595-4733-9c24-8104e8454b4c"), Id = 3 },
                        new { MovieId = new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), GenreId = new Guid("6e0a64bc-7a50-4a6c-9125-8ccf6e54bf70"), Id = 4 },
                        new { MovieId = new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), GenreId = new Guid("01c66e3e-8c25-4f5c-a2c5-512c79d09aa6"), Id = 5 },
                        new { MovieId = new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), GenreId = new Guid("976a7a24-1c25-4a7f-97c6-1a019c5c148d"), Id = 6 },
                        new { MovieId = new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), GenreId = new Guid("72163c34-3d32-4a78-9701-1f708053978f"), Id = 7 },
                        new { MovieId = new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), GenreId = new Guid("e143ebff-a0bd-4107-889f-9bff26eda916"), Id = 8 },
                        new { MovieId = new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), GenreId = new Guid("0532f0df-c92d-4a10-9d1a-8a5935c541a2"), Id = 9 },
                        new { MovieId = new Guid("45c0c223-de18-4e6e-99ea-aed94e7469f1"), GenreId = new Guid("e48aa8e0-3efc-4acd-a0d5-88c82551807a"), Id = 10 },
                        new { MovieId = new Guid("de9b842d-531a-4f17-ad69-0d3e11cb911d"), GenreId = new Guid("e48aa8e0-3efc-4acd-a0d5-88c82551807a"), Id = 11 },
                        new { MovieId = new Guid("de9b842d-531a-4f17-ad69-0d3e11cb911d"), GenreId = new Guid("76c2fab8-7161-49b7-88c6-f3aaf484ea64"), Id = 12 },
                        new { MovieId = new Guid("195c5a46-96f9-446b-b4f7-864ab2dc49de"), GenreId = new Guid("6e0a64bc-7a50-4a6c-9125-8ccf6e54bf70"), Id = 13 },
                        new { MovieId = new Guid("195c5a46-96f9-446b-b4f7-864ab2dc49de"), GenreId = new Guid("0532f0df-c92d-4a10-9d1a-8a5935c541a2"), Id = 14 },
                        new { MovieId = new Guid("195c5a46-96f9-446b-b4f7-864ab2dc49de"), GenreId = new Guid("76c2fab8-7161-49b7-88c6-f3aaf484ea64"), Id = 15 },
                        new { MovieId = new Guid("174fd8d4-f72b-4059-a7ea-05e687026b0d"), GenreId = new Guid("6e0a64bc-7a50-4a6c-9125-8ccf6e54bf70"), Id = 16 },
                        new { MovieId = new Guid("174fd8d4-f72b-4059-a7ea-05e687026b0d"), GenreId = new Guid("72163c34-3d32-4a78-9701-1f708053978f"), Id = 17 },
                        new { MovieId = new Guid("174fd8d4-f72b-4059-a7ea-05e687026b0d"), GenreId = new Guid("e143ebff-a0bd-4107-889f-9bff26eda916"), Id = 18 },
                        new { MovieId = new Guid("163c03b3-a057-426d-afa3-1a2631a693e2"), GenreId = new Guid("6e0a64bc-7a50-4a6c-9125-8ccf6e54bf70"), Id = 19 },
                        new { MovieId = new Guid("163c03b3-a057-426d-afa3-1a2631a693e2"), GenreId = new Guid("01c66e3e-8c25-4f5c-a2c5-512c79d09aa6"), Id = 20 },
                        new { MovieId = new Guid("163c03b3-a057-426d-afa3-1a2631a693e2"), GenreId = new Guid("976a7a24-1c25-4a7f-97c6-1a019c5c148d"), Id = 21 },
                        new { MovieId = new Guid("163c03b3-a057-426d-afa3-1a2631a693e2"), GenreId = new Guid("76c2fab8-7161-49b7-88c6-f3aaf484ea64"), Id = 22 }
                    );
                });

            modelBuilder.Entity("Biob.Data.Models.Seat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RowNo");

                    b.Property<int>("SeatNo");

                    b.HasKey("Id");

                    b.ToTable("Seats");

                    b.HasData(
                        new { Id = 1, RowNo = 1, SeatNo = 1 },
                        new { Id = 2, RowNo = 1, SeatNo = 2 },
                        new { Id = 3, RowNo = 1, SeatNo = 3 },
                        new { Id = 4, RowNo = 1, SeatNo = 4 },
                        new { Id = 5, RowNo = 1, SeatNo = 5 },
                        new { Id = 6, RowNo = 1, SeatNo = 6 },
                        new { Id = 7, RowNo = 1, SeatNo = 7 },
                        new { Id = 8, RowNo = 1, SeatNo = 8 },
                        new { Id = 9, RowNo = 1, SeatNo = 9 },
                        new { Id = 10, RowNo = 1, SeatNo = 10 },
                        new { Id = 11, RowNo = 2, SeatNo = 1 },
                        new { Id = 12, RowNo = 2, SeatNo = 2 },
                        new { Id = 13, RowNo = 2, SeatNo = 3 },
                        new { Id = 14, RowNo = 2, SeatNo = 4 },
                        new { Id = 15, RowNo = 2, SeatNo = 5 },
                        new { Id = 16, RowNo = 2, SeatNo = 6 },
                        new { Id = 17, RowNo = 2, SeatNo = 7 },
                        new { Id = 18, RowNo = 2, SeatNo = 8 },
                        new { Id = 19, RowNo = 2, SeatNo = 9 },
                        new { Id = 20, RowNo = 2, SeatNo = 10 }
                    );
                });

            modelBuilder.Entity("Biob.Data.Models.Showtime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HallId");

                    b.Property<Guid>("MovieId");

                    b.Property<bool>("ThreeDee");

                    b.Property<DateTimeOffset>("TimeOfPlaying");

                    b.HasKey("Id");

                    b.ToTable("Showtimes");

                    b.HasData(
                        new { Id = new Guid("092ca7c5-ae83-4a52-a38b-cfc7c8e40e9a"), HallId = 1, MovieId = new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"), ThreeDee = false, TimeOfPlaying = new DateTimeOffset(new DateTime(2018, 12, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) },
                        new { Id = new Guid("5e0d5ad3-22b0-4bdc-808c-62b8f50d0796"), HallId = 2, MovieId = new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"), ThreeDee = false, TimeOfPlaying = new DateTimeOffset(new DateTime(2018, 12, 23, 11, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) },
                        new { Id = new Guid("cf3c5f8e-94ee-494a-b0f1-4a48d9d8291f"), HallId = 3, MovieId = new Guid("9d90a452-9547-4d04-98ed-7d617e64ae1e"), ThreeDee = false, TimeOfPlaying = new DateTimeOffset(new DateTime(2018, 12, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) }
                    );
                });

            modelBuilder.Entity("Biob.Data.Models.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CustomerId");

                    b.Property<int>("HallSeatId");

                    b.Property<bool>("Paid");

                    b.Property<int>("Price");

                    b.Property<bool>("Reserved");

                    b.Property<Guid>("ShowtimeId");

                    b.HasKey("Id");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Biob.Data.Models.HallSeat", b =>
                {
                    b.HasOne("Biob.Data.Models.Hall", "Hall")
                        .WithMany("HallSeats")
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Biob.Data.Models.Seat", "Seat")
                        .WithMany("HallSeats")
                        .HasForeignKey("SeatId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Biob.Data.Models.MovieGenre", b =>
                {
                    b.HasOne("Biob.Data.Models.Genre", "Genre")
                        .WithMany("MovieGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Biob.Data.Models.Movie", "Movie")
                        .WithMany("MovieGenres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
