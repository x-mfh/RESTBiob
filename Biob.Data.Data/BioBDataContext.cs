using Biob.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Biob.Data.Data
{
    public class BiobDataContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Showtime> Showtimes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<HallSeat> HallSeats { get; set; }

        public BiobDataContext(DbContextOptions<BiobDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  TODO: add many to many relationship keys
            modelBuilder.Entity<MovieGenre>().HasKey(moviegenre => new { moviegenre.MovieId, moviegenre.GenreId });
            modelBuilder.Entity<HallSeat>().HasKey(hallseat => new { hallseat.HallId, hallseat.SeatId });

            // constraints
            //modelBuilder.Entity<Hall>().HasIndex(hall => hall.HallNo).IsUnique();

            // default values
            //modelBuilder.Entity<Movie>()
            //    .Property(movie => movie.ThreeDee)
            //    .HasDefaultValue(false);
            //modelBuilder.Entity<Hall>()
            //    .Property(hall => hall.ThreeDee)
            //    .HasDefaultValue(false);
            //modelBuilder.Entity<Showtime>()
            //    .Property(showtime => showtime.ThreeDee)
            //    .HasDefaultValue(false);
                

            //  TODO: add seed data
            modelBuilder.Entity<Genre>().HasData(
                new Genre() { Id = Guid.Parse("8068CBF6-C595-4733-9C24-8104E8454B4C"), GenreName = "Horror" },
                new Genre() { Id = Guid.Parse("6E0A64BC-7A50-4A6C-9125-8CCF6E54BF70"), GenreName = "Drama" },
                new Genre() { Id = Guid.Parse("01C66E3E-8C25-4F5C-A2C5-512C79D09AA6"), GenreName = "Comedy" },
                new Genre() { Id = Guid.Parse("976A7A24-1C25-4A7F-97C6-1A019C5C148D"), GenreName = "Romance" },
                new Genre() { Id = Guid.Parse("72163C34-3D32-4A78-9701-1F708053978F"), GenreName = "Action" },
                new Genre() { Id = Guid.Parse("E143EBFF-A0BD-4107-889F-9BFF26EDA916"), GenreName = "Fiction" },
                new Genre() { Id = Guid.Parse("0532F0DF-C92D-4A10-9D1A-8A5935C541A2"), GenreName = "Biography" },
                new Genre() { Id = Guid.Parse("E48AA8E0-3EFC-4ACD-A0D5-88C82551807A"), GenreName = "Fantasy" },
                new Genre() { Id = Guid.Parse("76C2FAB8-7161-49B7-88C6-F3AAF484EA64"), GenreName = "Family" }
                );

            //  TODO: fix dates possibly?
            modelBuilder.Entity<Movie>().HasData(
                // Movie 1
                new Movie()
                {
                    Id = Guid.Parse("9D90A452-9547-4D04-98ED-7D617E64AE1E"),
                    Title = "The Dark Knight",
                    Description = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham. The Dark Knight must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                    LengthInSeconds = 9120,
                    Poster = "",
                    Producer = "Kevin De La Noy",
                    Actors = "Christopher Nolan",
                    Released = new DateTimeOffset(new DateTime(2005, 5, 12, 0, 0, 0)),
                    AgeRestriction = 16,
                },
                // Movie 2
                new Movie()
                {
                    Id = Guid.Parse("45C0C223-DE18-4E6E-99EA-AED94E7469F1"),
                    Title = "SKP",
                    Description = "Nightmare on earth where students fight to survive on a daily basis.",
                    LengthInSeconds = 5400,
                    Poster = "",
                    Producer = "Jan Eg",
                    Actors = "Balen, Jason, Mikkel",
                    Released = new DateTimeOffset(new DateTime(2016, 5, 12, 0, 0, 0)),
                    AgeRestriction = 16,
                },
                // Movie 3
                new Movie()
                {
                    Id = Guid.Parse("DE9B842D-531A-4F17-AD69-0D3E11CB911D"),
                    Title = "Harry Potter and the Sorcerer's Stone",
                    Description = "Albus Dumbledore, Minerva McGonagall, and Rubeus Hagrid, professors of Hogwarts School of Witchcraft and Wizardry, deliver a recently orphaned infant named Harry Potter to his only remaining relatives, the Dursleys.",
                    LengthInSeconds = 9120,
                    Poster = "",
                    Producer = "David Heyman",
                    Actors = "Daniel Radcliffe, Emma Watson, Rupert Grint",
                    Released = new DateTimeOffset(new DateTime(2001, 11, 23, 0, 0, 0)),
                    ThreeDee = true,
                    AgeRestriction = 11,
                },
                // Movie 4
                new Movie()
                {
                    Id = Guid.Parse("195C5A46-96F9-446B-B4F7-864AB2DC49DE"),
                    Title = "Bohemian Rhapsody",
                    Description = "A chronicle of the years leading up to Queen's legendary appearance at the Live Aid (1985) concert.",
                    LengthInSeconds = 8040,
                    Poster = "",
                    Producer = "Bryan Singer",
                    Actors = "Rami Malek, Lucy Boynton, Joseph Mazzello",
                    Released = new DateTimeOffset(new DateTime(2018, 11, 1, 0, 0, 0)),
                    AgeRestriction = 11,
                },
                // Movie 5
                new Movie()
                {
                    Id = Guid.Parse("174FD8D4-F72B-4059-A7EA-05E687026B0D"),
                    Title = "Venom",
                    Description = "When Eddie Brock acquires the powers of a symbiote, he will have to release his alter-ego 'Venom' to save his life.",
                    LengthInSeconds = 6720,
                    Poster = "",
                    Producer = "Ruben Fleischer",
                    Actors = "Tom Hardy, Michelle Williams, Riz Ahmed",
                    Released = new DateTimeOffset(new DateTime(2018, 10, 11, 0, 0, 0)),
                    ThreeDee = true,
                    AgeRestriction = 18,
                },
                // Movie 6
                new Movie()
                {
                    Id = Guid.Parse("163C03B3-A057-426D-AFA3-1A2631A693E2"),
                    Title = "A Star Is Born",
                    Description = "A musician helps a young singer find fame, even as age and alcoholism send his own career into a downward spiral.",
                    LengthInSeconds = 8160,
                    Poster = "",
                    Producer = "Bradley Cooper",
                    Actors = "Lady Gaga, Bradley Cooper, Sam Elliott",
                    Released = new DateTimeOffset(new DateTime(2018, 12, 6, 0, 0, 0)),
                    AgeRestriction = 16,
                }
                );

            modelBuilder.Entity<MovieGenre>().HasData(
                // Movie 1
                new MovieGenre() { Id = 1, MovieId = Guid.Parse("9D90A452-9547-4D04-98ED-7D617E64AE1E"), GenreId = Guid.Parse("6E0A64BC-7A50-4A6C-9125-8CCF6E54BF70") },
                new MovieGenre() { Id = 2, MovieId = Guid.Parse("9D90A452-9547-4D04-98ED-7D617E64AE1E"), GenreId = Guid.Parse("72163C34-3D32-4A78-9701-1F708053978F") },
                // Movie 2
                new MovieGenre() { Id = 3, MovieId = Guid.Parse("45C0C223-DE18-4E6E-99EA-AED94E7469F1"), GenreId = Guid.Parse("8068CBF6-C595-4733-9C24-8104E8454B4C") },
                new MovieGenre() { Id = 4, MovieId = Guid.Parse("45C0C223-DE18-4E6E-99EA-AED94E7469F1"), GenreId = Guid.Parse("6E0A64BC-7A50-4A6C-9125-8CCF6E54BF70") },
                new MovieGenre() { Id = 5, MovieId = Guid.Parse("45C0C223-DE18-4E6E-99EA-AED94E7469F1"), GenreId = Guid.Parse("01C66E3E-8C25-4F5C-A2C5-512C79D09AA6") },
                new MovieGenre() { Id = 6, MovieId = Guid.Parse("45C0C223-DE18-4E6E-99EA-AED94E7469F1"), GenreId = Guid.Parse("976A7A24-1C25-4A7F-97C6-1A019C5C148D") },
                new MovieGenre() { Id = 7, MovieId = Guid.Parse("45C0C223-DE18-4E6E-99EA-AED94E7469F1"), GenreId = Guid.Parse("72163C34-3D32-4A78-9701-1F708053978F") },
                new MovieGenre() { Id = 8, MovieId = Guid.Parse("45C0C223-DE18-4E6E-99EA-AED94E7469F1"), GenreId = Guid.Parse("E143EBFF-A0BD-4107-889F-9BFF26EDA916") },
                new MovieGenre() { Id = 9, MovieId = Guid.Parse("45C0C223-DE18-4E6E-99EA-AED94E7469F1"), GenreId = Guid.Parse("0532F0DF-C92D-4A10-9D1A-8A5935C541A2") },
                new MovieGenre() { Id = 10, MovieId = Guid.Parse("45C0C223-DE18-4E6E-99EA-AED94E7469F1"), GenreId = Guid.Parse("E48AA8E0-3EFC-4ACD-A0D5-88C82551807A") },
                // Movie 3
                new MovieGenre() { Id = 11, MovieId = Guid.Parse("DE9B842D-531A-4F17-AD69-0D3E11CB911D"), GenreId = Guid.Parse("E48AA8E0-3EFC-4ACD-A0D5-88C82551807A") },
                new MovieGenre() { Id = 12, MovieId = Guid.Parse("DE9B842D-531A-4F17-AD69-0D3E11CB911D"), GenreId = Guid.Parse("76C2FAB8-7161-49B7-88C6-F3AAF484EA64") },
                // Movie 4
                new MovieGenre() { Id = 13, MovieId = Guid.Parse("195C5A46-96F9-446B-B4F7-864AB2DC49DE"), GenreId = Guid.Parse("6E0A64BC-7A50-4A6C-9125-8CCF6E54BF70") },
                new MovieGenre() { Id = 14, MovieId = Guid.Parse("195C5A46-96F9-446B-B4F7-864AB2DC49DE"), GenreId = Guid.Parse("0532F0DF-C92D-4A10-9D1A-8A5935C541A2") },
                new MovieGenre() { Id = 15, MovieId = Guid.Parse("195C5A46-96F9-446B-B4F7-864AB2DC49DE"), GenreId = Guid.Parse("76C2FAB8-7161-49B7-88C6-F3AAF484EA64") },
                // Movie 5
                new MovieGenre() { Id = 16, MovieId = Guid.Parse("174FD8D4-F72B-4059-A7EA-05E687026B0D"), GenreId = Guid.Parse("6E0A64BC-7A50-4A6C-9125-8CCF6E54BF70") },
                new MovieGenre() { Id = 17, MovieId = Guid.Parse("174FD8D4-F72B-4059-A7EA-05E687026B0D"), GenreId = Guid.Parse("72163C34-3D32-4A78-9701-1F708053978F") },
                new MovieGenre() { Id = 18, MovieId = Guid.Parse("174FD8D4-F72B-4059-A7EA-05E687026B0D"), GenreId = Guid.Parse("E143EBFF-A0BD-4107-889F-9BFF26EDA916") },
                // Movie 6
                new MovieGenre() { Id = 19, MovieId = Guid.Parse("163C03B3-A057-426D-AFA3-1A2631A693E2"), GenreId = Guid.Parse("6E0A64BC-7A50-4A6C-9125-8CCF6E54BF70") },
                new MovieGenre() { Id = 20, MovieId = Guid.Parse("163C03B3-A057-426D-AFA3-1A2631A693E2"), GenreId = Guid.Parse("01C66E3E-8C25-4F5C-A2C5-512C79D09AA6") },
                new MovieGenre() { Id = 21, MovieId = Guid.Parse("163C03B3-A057-426D-AFA3-1A2631A693E2"), GenreId = Guid.Parse("976A7A24-1C25-4A7F-97C6-1A019C5C148D") },
                new MovieGenre() { Id = 22, MovieId = Guid.Parse("163C03B3-A057-426D-AFA3-1A2631A693E2"), GenreId = Guid.Parse("76C2FAB8-7161-49B7-88C6-F3AAF484EA64") }
                );

            modelBuilder.Entity<Hall>().HasData(
                new Hall() { Id = 1, HallNo = 1, NoOfSeats = 10, ThreeDee = true},
                new Hall() { Id = 2, HallNo = 2, NoOfSeats = 20 },
                new Hall() { Id = 3, HallNo = 3, NoOfSeats = 20 }
                );

            modelBuilder.Entity<Seat>().HasData(
                new Seat() { Id = 1, RowNo = 1, SeatNo = 1 },
                new Seat() { Id = 2, RowNo = 1, SeatNo = 2 },
                new Seat() { Id = 3, RowNo = 1, SeatNo = 3 },
                new Seat() { Id = 4, RowNo = 1, SeatNo = 4 },
                new Seat() { Id = 5, RowNo = 1, SeatNo = 5 },
                new Seat() { Id = 6, RowNo = 1, SeatNo = 6 },
                new Seat() { Id = 7, RowNo = 1, SeatNo = 7 },
                new Seat() { Id = 8, RowNo = 1, SeatNo = 8 },
                new Seat() { Id = 9, RowNo = 1, SeatNo = 9 },
                new Seat() { Id = 10, RowNo = 1, SeatNo = 10 },
                new Seat() { Id = 11, RowNo = 2, SeatNo = 1 },
                new Seat() { Id = 12, RowNo = 2, SeatNo = 2 },
                new Seat() { Id = 13, RowNo = 2, SeatNo = 3 },
                new Seat() { Id = 14, RowNo = 2, SeatNo = 4 },
                new Seat() { Id = 15, RowNo = 2, SeatNo = 5 },
                new Seat() { Id = 16, RowNo = 2, SeatNo = 6 },
                new Seat() { Id = 17, RowNo = 2, SeatNo = 7 },
                new Seat() { Id = 18, RowNo = 2, SeatNo = 8 },
                new Seat() { Id = 19, RowNo = 2, SeatNo = 9 },
                new Seat() { Id = 20, RowNo = 2, SeatNo = 10 }
                );

            modelBuilder.Entity<HallSeat>().HasData(
                // Hall 1
                new HallSeat() { Id = 1, HallId = 1, SeatId = 1 },
                new HallSeat() { Id = 2, HallId = 1, SeatId = 2 },
                new HallSeat() { Id = 3, HallId = 1, SeatId = 3 },
                new HallSeat() { Id = 4, HallId = 1, SeatId = 4 },
                new HallSeat() { Id = 5, HallId = 1, SeatId = 5 },
                new HallSeat() { Id = 6, HallId = 1, SeatId = 11 },
                new HallSeat() { Id = 7, HallId = 1, SeatId = 12 },
                new HallSeat() { Id = 8, HallId = 1, SeatId = 13 },
                new HallSeat() { Id = 9, HallId = 1, SeatId = 14 },
                new HallSeat() { Id = 10, HallId = 1, SeatId = 15 },

                // Hall 2
                new HallSeat() { Id =  11, HallId = 2, SeatId = 1 },
                new HallSeat() { Id =  12, HallId = 2, SeatId = 2 },
                new HallSeat() { Id =  13, HallId = 2, SeatId = 3 },
                new HallSeat() { Id =  14, HallId = 2, SeatId = 4 },
                new HallSeat() { Id =  15, HallId = 2, SeatId = 5 },
                new HallSeat() { Id =  16, HallId = 2, SeatId = 6 },
                new HallSeat() { Id =  17, HallId = 2, SeatId = 7 },
                new HallSeat() { Id =  18, HallId = 2, SeatId = 8 },
                new HallSeat() { Id =  19, HallId = 2, SeatId = 9 },
                new HallSeat() { Id =  20, HallId = 2, SeatId = 10 },
                new HallSeat() { Id =  21, HallId = 2, SeatId = 11 },
                new HallSeat() { Id =  22, HallId = 2, SeatId = 12 },
                new HallSeat() { Id =  23, HallId = 2, SeatId = 13 },
                new HallSeat() { Id =  24, HallId = 2, SeatId = 14 },
                new HallSeat() { Id =  25, HallId = 2, SeatId = 15 },
                new HallSeat() { Id =  26, HallId = 2, SeatId = 16 },
                new HallSeat() { Id =  27, HallId = 2, SeatId = 17 },
                new HallSeat() { Id =  28, HallId = 2, SeatId = 18 },
                new HallSeat() { Id =  29, HallId = 2, SeatId = 19 },
                new HallSeat() { Id =  30, HallId = 2, SeatId = 20 },

                // Hall 3
                new HallSeat() { Id =  31, HallId = 3, SeatId = 1 },
                new HallSeat() { Id =  32, HallId = 3, SeatId = 2 },
                new HallSeat() { Id =  33, HallId = 3, SeatId = 3 },
                new HallSeat() { Id =  34, HallId = 3, SeatId = 4 },
                new HallSeat() { Id =  35, HallId = 3, SeatId = 5 },
                new HallSeat() { Id =  36, HallId = 3, SeatId = 6 },
                new HallSeat() { Id =  37, HallId = 3, SeatId = 7 },
                new HallSeat() { Id =  38, HallId = 3, SeatId = 8 },
                new HallSeat() { Id =  39, HallId = 3, SeatId = 9 },
                new HallSeat() { Id =  40, HallId = 3, SeatId = 10 },
                new HallSeat() { Id =  41, HallId = 3, SeatId = 11 },
                new HallSeat() { Id =  42, HallId = 3, SeatId = 12 },
                new HallSeat() { Id =  43, HallId = 3, SeatId = 13 },
                new HallSeat() { Id =  44, HallId = 3, SeatId = 14 },
                new HallSeat() { Id =  45, HallId = 3, SeatId = 15 },
                new HallSeat() { Id =  46, HallId = 3, SeatId = 16 },
                new HallSeat() { Id =  47, HallId = 3, SeatId = 17 },
                new HallSeat() { Id =  48, HallId = 3, SeatId = 18 },
                new HallSeat() { Id =  49, HallId = 3, SeatId = 19 },
                new HallSeat() { Id =  50, HallId = 3, SeatId = 20 }
                );

            modelBuilder.Entity<Showtime>().HasData(
                new Showtime()
                {
                    Id = Guid.Parse("092CA7C5-AE83-4A52-A38B-CFC7C8E40E9A"),
                    MovieId = Guid.Parse("9D90A452-9547-4D04-98ED-7D617E64AE1E"),
                    HallId = 1,
                    TimeOfPlaying = new DateTimeOffset(new DateTime(2018, 12, 22, 10, 0, 0))
                },
                new Showtime()
                {
                    Id = Guid.Parse("5E0D5AD3-22B0-4BDC-808C-62B8F50D0796"),
                    MovieId = Guid.Parse("9D90A452-9547-4D04-98ED-7D617E64AE1E"),
                    HallId = 2,
                    TimeOfPlaying = new DateTimeOffset(new DateTime(2018, 12, 23, 11, 0, 0))
                },
                new Showtime()
                {
                    Id = Guid.Parse("CF3C5F8E-94EE-494A-B0F1-4A48D9D8291F"),
                    MovieId = Guid.Parse("9D90A452-9547-4D04-98ED-7D617E64AE1E"),
                    HallId = 3,
                    TimeOfPlaying = new DateTimeOffset(new DateTime(2018, 12, 24, 12, 0, 0))
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
