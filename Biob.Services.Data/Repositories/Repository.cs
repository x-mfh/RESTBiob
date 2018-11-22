using Biob.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Biob.Services.Data.Repositories
{
    public class Repository : IRepository
    {
        protected readonly BiobDataContext _context;

        public Repository(BiobDataContext context)
        {
            _context = context;
        }

        public async Task<bool> MovieExists(Guid id)
        {
            var movieFromDb = await _context.Movies.Where(movie => movie.Id == id).FirstOrDefaultAsync();
            return movieFromDb == null ? false : true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return ( await _context.SaveChangesAsync() > 0);
        }
    }
}
