using Biob.Data.Models;
using Microsoft.EntityFrameworkCore;

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
            base.OnModelCreating(modelBuilder);
        }
    }
}
