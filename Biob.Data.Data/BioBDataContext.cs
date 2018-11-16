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
                    Id = new Guid("{9D90A452-9547-4D04-98ED-7D617E64AE1E}"),
                    Title = "The Dark Knight",
                    Description = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham. The Dark Knight must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                    LengthInSeconds = 9120,
                    Poster = "",
                    Producer = "Kevin De La Noy",
                    Actors = "Christopher Nolan",
                    Genre = "Action",
                    Released = new DateTime(2005,5,12,0,0,0),
                    ThreeDee = false,
                    AgeRestriction = 16,
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
