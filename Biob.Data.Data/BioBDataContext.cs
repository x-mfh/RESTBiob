using Biob.Data.Common.Models;
using Biob.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        public BiobDataContext(DbContextOptions<BiobDataContext> options) : base(options)
        {

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken)) => SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            //  add audit information before saving
            AddAuditInformation();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  TODO: add many to many relationship keys
            modelBuilder.Entity<MovieGenre>().HasKey(moviegenre => new { moviegenre.MovieId, moviegenre.GenreId });

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
                new MovieGenre() { Id = Guid.Parse("5DF8A672-E37A-4EC2-8B6D-D331C84E7F8D"), MovieId = Guid.Parse("9D90A452-9547-4D04-98ED-7D617E64AE1E"), GenreId = Guid.Parse("6E0A64BC-7A50-4A6C-9125-8CCF6E54BF70") },
                new MovieGenre() { Id = Guid.Parse("C136188F-D71C-4575-8258-1AF4DB16DC0F"), MovieId = Guid.Parse("9D90A452-9547-4D04-98ED-7D617E64AE1E"), GenreId = Guid.Parse("72163C34-3D32-4A78-9701-1F708053978F") },
                // Movie 2
                new MovieGenre() { Id = Guid.Parse("BA8E9182-B327-4089-95F9-AD187C414AAD"), MovieId = Guid.Parse("45C0C223-DE18-4E6E-99EA-AED94E7469F1"), GenreId = Guid.Parse("8068CBF6-C595-4733-9C24-8104E8454B4C") },
                new MovieGenre() { Id = Guid.Parse("50D42FAC-3A09-4BBC-BE99-85D66F664104"), MovieId = Guid.Parse("45C0C223-DE18-4E6E-99EA-AED94E7469F1"), GenreId = Guid.Parse("6E0A64BC-7A50-4A6C-9125-8CCF6E54BF70") },
                new MovieGenre() { Id = Guid.Parse("FF036140-B028-4FE6-82D7-00AB55278518"), MovieId = Guid.Parse("45C0C223-DE18-4E6E-99EA-AED94E7469F1"), GenreId = Guid.Parse("01C66E3E-8C25-4F5C-A2C5-512C79D09AA6") },
                new MovieGenre() { Id = Guid.Parse("944AC338-EA45-4133-9EBB-4D0BD2A1DB3B"), MovieId = Guid.Parse("45C0C223-DE18-4E6E-99EA-AED94E7469F1"), GenreId = Guid.Parse("976A7A24-1C25-4A7F-97C6-1A019C5C148D") },
                new MovieGenre() { Id = Guid.Parse("7C3C5A3D-4566-4C04-BAB2-987EB175F3CA"), MovieId = Guid.Parse("45C0C223-DE18-4E6E-99EA-AED94E7469F1"), GenreId = Guid.Parse("72163C34-3D32-4A78-9701-1F708053978F") },
                new MovieGenre() { Id = Guid.Parse("B7364F6C-9205-46CA-B6C9-8EABFD3DB362"), MovieId = Guid.Parse("45C0C223-DE18-4E6E-99EA-AED94E7469F1"), GenreId = Guid.Parse("E143EBFF-A0BD-4107-889F-9BFF26EDA916") },
                new MovieGenre() { Id = Guid.Parse("1E7F851F-BA66-4989-AFC7-0D193B08575E"), MovieId = Guid.Parse("45C0C223-DE18-4E6E-99EA-AED94E7469F1"), GenreId = Guid.Parse("0532F0DF-C92D-4A10-9D1A-8A5935C541A2") },
                new MovieGenre() { Id = Guid.Parse("F128E5DE-CA20-4672-BE7D-E84A3FB3ED60"), MovieId = Guid.Parse("45C0C223-DE18-4E6E-99EA-AED94E7469F1"), GenreId = Guid.Parse("E48AA8E0-3EFC-4ACD-A0D5-88C82551807A") },
                // Movie 3
                new MovieGenre() { Id = Guid.Parse("8FDEAECF-FD6F-4AD5-AD3D-4B0E64380AB5"), MovieId = Guid.Parse("DE9B842D-531A-4F17-AD69-0D3E11CB911D"), GenreId = Guid.Parse("E48AA8E0-3EFC-4ACD-A0D5-88C82551807A") },
                new MovieGenre() { Id = Guid.Parse("FEA24F3A-5853-404E-ADCB-9A35BF9A0EDB"), MovieId = Guid.Parse("DE9B842D-531A-4F17-AD69-0D3E11CB911D"), GenreId = Guid.Parse("76C2FAB8-7161-49B7-88C6-F3AAF484EA64") },
                // Movie 4
                new MovieGenre() { Id = Guid.Parse("7AD0A3A0-8D31-4F50-83A1-36BF60D3AFB5"), MovieId = Guid.Parse("195C5A46-96F9-446B-B4F7-864AB2DC49DE"), GenreId = Guid.Parse("6E0A64BC-7A50-4A6C-9125-8CCF6E54BF70") },
                new MovieGenre() { Id = Guid.Parse("DE438A37-46B0-4EC4-9AB9-C7797E0733FC"), MovieId = Guid.Parse("195C5A46-96F9-446B-B4F7-864AB2DC49DE"), GenreId = Guid.Parse("0532F0DF-C92D-4A10-9D1A-8A5935C541A2") },
                new MovieGenre() { Id = Guid.Parse("677C944F-2498-4D65-B0D5-6276E6FB3261"), MovieId = Guid.Parse("195C5A46-96F9-446B-B4F7-864AB2DC49DE"), GenreId = Guid.Parse("76C2FAB8-7161-49B7-88C6-F3AAF484EA64") },
                // Movie 5
                new MovieGenre() { Id = Guid.Parse("CCEEA45F-FA85-4E1A-810F-4ECC606F91FA"), MovieId = Guid.Parse("174FD8D4-F72B-4059-A7EA-05E687026B0D"), GenreId = Guid.Parse("6E0A64BC-7A50-4A6C-9125-8CCF6E54BF70") },
                new MovieGenre() { Id = Guid.Parse("AE13C433-3272-43A0-80EC-8DECACF70976"), MovieId = Guid.Parse("174FD8D4-F72B-4059-A7EA-05E687026B0D"), GenreId = Guid.Parse("72163C34-3D32-4A78-9701-1F708053978F") },
                new MovieGenre() { Id = Guid.Parse("20565D3D-A307-40C5-A30C-229764D7B5C6"), MovieId = Guid.Parse("174FD8D4-F72B-4059-A7EA-05E687026B0D"), GenreId = Guid.Parse("E143EBFF-A0BD-4107-889F-9BFF26EDA916") },
                // Movie 6
                new MovieGenre() { Id = Guid.Parse("0CC0D877-05A6-4227-8191-A9AED4F67757"), MovieId = Guid.Parse("163C03B3-A057-426D-AFA3-1A2631A693E2"), GenreId = Guid.Parse("6E0A64BC-7A50-4A6C-9125-8CCF6E54BF70") },
                new MovieGenre() { Id = Guid.Parse("7FB799A4-7EE1-4E72-AA66-ED490BFCF682"), MovieId = Guid.Parse("163C03B3-A057-426D-AFA3-1A2631A693E2"), GenreId = Guid.Parse("01C66E3E-8C25-4F5C-A2C5-512C79D09AA6") },
                new MovieGenre() { Id = Guid.Parse("EA23961B-3379-48AC-862D-28756F2593D2"), MovieId = Guid.Parse("163C03B3-A057-426D-AFA3-1A2631A693E2"), GenreId = Guid.Parse("976A7A24-1C25-4A7F-97C6-1A019C5C148D") },
                new MovieGenre() { Id = Guid.Parse("FCC28F44-22DC-47EB-ACFA-CDCF8F6265C6"), MovieId = Guid.Parse("163C03B3-A057-426D-AFA3-1A2631A693E2"), GenreId = Guid.Parse("76C2FAB8-7161-49B7-88C6-F3AAF484EA64") }
                );

            modelBuilder.Entity<Hall>().HasData(
                new Hall() { Id = Guid.Parse("7E9A2751-F1C0-4EB6-A7EC-1319C6DAE31E"), HallNo = 1, NoOfSeats = 10, ThreeDee = true},
                new Hall() { Id = Guid.Parse("288AAD6A-F042-4B36-A5AE-F950AEA18B46"), HallNo = 2, NoOfSeats = 20 },
                new Hall() { Id = Guid.Parse("D90AC9E4-32BA-4B5C-80EA-6EDA60C0131B"), HallNo = 3, NoOfSeats = 20 }
                );

            modelBuilder.Entity<Seat>().HasData(
                new Seat() { Id = Guid.Parse("5FD7F7C4-D90F-4D60-8878-067AF214A0DC"), HallId = Guid.Parse("7E9A2751-F1C0-4EB6-A7EC-1319C6DAE31E"), RowNo = 1, SeatNo = 1 },
                new Seat() { Id = Guid.Parse("603AB124-4BE6-40FD-9A5E-49BB4A5730DB"), HallId = Guid.Parse("7E9A2751-F1C0-4EB6-A7EC-1319C6DAE31E"), RowNo = 1, SeatNo = 2 },
                new Seat() { Id = Guid.Parse("F9574335-CE2F-48BE-A275-1BB2718DED0A"), HallId = Guid.Parse("7E9A2751-F1C0-4EB6-A7EC-1319C6DAE31E"), RowNo = 1, SeatNo = 3 },
                new Seat() { Id = Guid.Parse("245E0E3E-AE50-4E80-B506-01436223F4AA"), HallId = Guid.Parse("7E9A2751-F1C0-4EB6-A7EC-1319C6DAE31E"), RowNo = 2, SeatNo = 2 },

                new Seat() { Id = Guid.Parse("173595A9-917D-4DF9-9A6D-1A5D5B46104E"), HallId = Guid.Parse("288AAD6A-F042-4B36-A5AE-F950AEA18B46"), RowNo = 1, SeatNo = 4 },
                new Seat() { Id = Guid.Parse("70FE1293-99C5-43AA-82DF-FD0BEAA8076A"), HallId = Guid.Parse("288AAD6A-F042-4B36-A5AE-F950AEA18B46"), RowNo = 1, SeatNo = 5 },
                new Seat() { Id = Guid.Parse("A66E828E-75AF-4EFE-8E2A-225694CE0BB1"), HallId = Guid.Parse("288AAD6A-F042-4B36-A5AE-F950AEA18B46"), RowNo = 1, SeatNo = 6 },
                new Seat() { Id = Guid.Parse("10CA7C8F-CA02-4FB5-B53C-90A554345471"), HallId = Guid.Parse("288AAD6A-F042-4B36-A5AE-F950AEA18B46"), RowNo = 2, SeatNo = 1 },

                new Seat() { Id = Guid.Parse("9C55195A-669B-4366-81CF-7796F014537D"), HallId = Guid.Parse("D90AC9E4-32BA-4B5C-80EA-6EDA60C0131B"), RowNo = 1, SeatNo = 7 },
                new Seat() { Id = Guid.Parse("979FA768-B42A-444C-944E-4295AE64E00D"), HallId = Guid.Parse("D90AC9E4-32BA-4B5C-80EA-6EDA60C0131B"), RowNo = 1, SeatNo = 8 },
                new Seat() { Id = Guid.Parse("CB3314DB-47A0-495A-BDDB-FF2C8F17395D"), HallId = Guid.Parse("D90AC9E4-32BA-4B5C-80EA-6EDA60C0131B"), RowNo = 1, SeatNo = 9 },
                new Seat() { Id = Guid.Parse("F1A83A75-1770-47B3-B209-40198951D4AF"), HallId = Guid.Parse("D90AC9E4-32BA-4B5C-80EA-6EDA60C0131B"), RowNo = 1, SeatNo = 10 }
                
                

                );

            modelBuilder.Entity<Showtime>().HasData(
                new Showtime()
                {
                    Id = Guid.Parse("092CA7C5-AE83-4A52-A38B-CFC7C8E40E9A"),
                    MovieId = Guid.Parse("9D90A452-9547-4D04-98ED-7D617E64AE1E"),
                    HallId = Guid.Parse("7E9A2751-F1C0-4EB6-A7EC-1319C6DAE31E"),
                    TimeOfPlaying = new DateTimeOffset(new DateTime(2018, 12, 22, 10, 0, 0))
                },
                new Showtime()
                {
                    Id = Guid.Parse("5E0D5AD3-22B0-4BDC-808C-62B8F50D0796"),
                    MovieId = Guid.Parse("9D90A452-9547-4D04-98ED-7D617E64AE1E"),
                    HallId = Guid.Parse("288AAD6A-F042-4B36-A5AE-F950AEA18B46"),
                    TimeOfPlaying = new DateTimeOffset(new DateTime(2018, 12, 23, 11, 0, 0))
                },
                new Showtime()
                {
                    Id = Guid.Parse("CF3C5F8E-94EE-494A-B0F1-4A48D9D8291F"),
                    MovieId = Guid.Parse("9D90A452-9547-4D04-98ED-7D617E64AE1E"),
                    HallId = Guid.Parse("D90AC9E4-32BA-4B5C-80EA-6EDA60C0131B"),
                    TimeOfPlaying = new DateTimeOffset(new DateTime(2018, 12, 24, 12, 0, 0))
                }
                );


            //  get all the deleteable entity types
            var deleteableEntityTypes = modelBuilder.Model.GetEntityTypes().Where(x => x.ClrType != null && typeof(IDeleteable).IsAssignableFrom(x.ClrType));

            foreach (var deletedentityType in deleteableEntityTypes)
            {
                //  add indexer on the IsDeleted property on each of them
                modelBuilder.Entity(deletedentityType.ClrType).HasIndex(nameof(IDeleteable.IsDeleted));
            }

        }

        /// <summary>
        /// Adds audit (createdon or modifiedon) information to entities that inherit IAduit
        /// </summary>
        private void AddAuditInformation()
        {
            //  get the changed entries from the changed tracker
            //  where they are of type IAduit
            //  and their state is either added or modified
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.Entity is IAudit && (x.State == EntityState.Added || x.State == EntityState.Modified));


            foreach (var entry in changedEntities)
            {
                //  type cast the entity in the entry to IAduit
                var entity = (IAudit)entry.Entity;

                //  if the entity state is added and the createdon property on the entity is default
                //  default of thet type which in this case is null
                //  then assign created on, otherwise assign modified on
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTimeOffset))
                {
                    entity.CreatedOn = DateTimeOffset.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTimeOffset.Now;
                }
            }
        }
    }
}
