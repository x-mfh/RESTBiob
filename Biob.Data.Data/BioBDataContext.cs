using Biob.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Biob.Data.Data
{
    public class BiobDataContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public BiobDataContext(DbContextOptions<BiobDataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  TODO: add many to many relationship keys

            //  TODO: add seed data
            //  TODO: fix dates possibly?
            modelBuilder.Entity<Movie>().HasData(
                new Movie()
                {
                    Id = new Guid("9D90A452-9547-4D04-98ED-7D617E64AE1E"),
                    Title = "The Dark Knight",
                    Description = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham. The Dark Knight must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                    LengthInSeconds = 9120,
                    Poster = "",
                    Producer = "Kevin De La Noy",
                    Actors = "Christopher Nolan",
                    Genre = "Action",
                    Released = new DateTimeOffset(new DateTime(2005, 5, 12, 0, 0, 0)),
                    ThreeDee = false,
                    AgeRestriction = 16,
                },
                new Movie()
                {
                    Id = new Guid("45C0C223-DE18-4E6E-99EA-AED94E7469F1"),
                    Title = "SKP",
                    Description = "Nightmare on earth where students fight to survive on a daily basis.",
                    LengthInSeconds = 5400,
                    Poster = "",
                    Producer = "Jan Eg",
                    Actors = "Balen, Jason, Mikkel",
                    Genre = "Horror",
                    Released = new DateTimeOffset(new DateTime(2016, 5, 12, 0, 0, 0)),
                    ThreeDee = false,
                    AgeRestriction = 16,
                },
                new Movie()
                {
                    Id = new Guid("DE9B842D-531A-4F17-AD69-0D3E11CB911D"),
                    Title = "Harry Potter and the Philosopher's Stone",
                    Description = "Albus Dumbledore, Minerva McGonagall, and Rubeus Hagrid, professors of Hogwarts School of Witchcraft and Wizardry, deliver a recently orphaned infant named Harry Potter to his only remaining relatives, the Dursleys.",
                    LengthInSeconds = 9120,
                    Poster = "",
                    Producer = "David Heyman",
                    Actors = "Daniel Radcliffe, Emma Watson, Rupert Grint",
                    Genre = "Fantasy",
                    Released = new DateTimeOffset(new DateTime(2001, 11, 4, 0, 0, 0)),
                    ThreeDee = true,
                    AgeRestriction = 11,
                },
                new Movie()
                {
                    Id = new Guid("195C5A46-96F9-446B-B4F7-864AB2DC49DE"),
                    Title = "Bohemian Rhapsody",
                    Description = "A chronicle of the years leading up to Queen's legendary appearance at the Live Aid (1985) concert.",
                    LengthInSeconds = 8040,
                    Poster = "",
                    Producer = "Bryan Singer",
                    Actors = "Rami Malek, Lucy Boynton, Joseph Mazzello",
                    Genre = "Biography",
                    Released = new DateTimeOffset(new DateTime(2018, 11, 1, 0, 0, 0)),
                    ThreeDee = false,
                    AgeRestriction = 11,
                },
                new Movie()
                {
                    Id = new Guid("174FD8D4-F72B-4059-A7EA-05E687026B0D"),
                    Title = "Venom",
                    Description = "When Eddie Brock acquires the powers of a symbiote, he will have to release his alter-ego 'Venom' to save his life.",
                    LengthInSeconds = 6720,
                    Poster = "",
                    Producer = "Ruben Fleischer",
                    Actors = "Tom Hardy, Michelle Williams, Riz Ahmed",
                    Genre = "Action",
                    Released = new DateTimeOffset(new DateTime(2018, 10, 11, 0, 0, 0)),
                    ThreeDee = true,
                    AgeRestriction = 18,
                },
                new Movie()
                {
                    Id = new Guid("163C03B3-A057-426D-AFA3-1A2631A693E2"),
                    Title = "A Star Is Born",
                    Description = "A musician helps a young singer find fame, even as age and alcoholism send his own career into a downward spiral.",
                    LengthInSeconds = 8160,
                    Poster = "",
                    Producer = "Bradley Cooper",
                    Actors = "Lady Gaga, Bradley Cooper, Sam Elliott",
                    Genre = "Drama",
                    Released = new DateTimeOffset(new DateTime(2018, 12, 6, 0, 0, 0)),
                    ThreeDee = false,
                    AgeRestriction = 16,
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
