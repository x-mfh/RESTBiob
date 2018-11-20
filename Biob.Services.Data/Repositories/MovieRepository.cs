using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biob.Data.Data;
using Biob.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Biob.Services.Data.Repositories
{
    public class MovieRepository : Repository, IMovieRepository
    {
        public MovieRepository(BiobDataContext context) : base(context)
        {

        }
        public void AddMovie(Movie movieToAdd)
        {
            if (movieToAdd.Id ==  Guid.Empty)
            {
                movieToAdd.Id = Guid.NewGuid();
            }
            _context.Movies.Add(movieToAdd);
        }

        public void DeleteMovie(Movie movieToDelete)
        {
            _context.Movies.Remove(movieToDelete);
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies//.OrderBy(movie => movie.Title)
                                        .ToListAsync();
        }

        public async Task<Movie> GetMovieAsync(Guid id)
        {
            return await _context.Movies.Where(movie => movie.Id == id).FirstOrDefaultAsync();
        }


        public void UpdateMovie(Movie movieToUpdate)
        {
            //  TODO: consider changing update to attach
            //  if things dont work as expected
            _context.Movies.Update(movieToUpdate);
        }
    }
}
