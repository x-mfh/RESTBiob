using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biob.Data.Data;
using Biob.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Biob.Services.Data.Repositories
{
    public class GenreRepository : Repository, IGenreRepository
    {
        public GenreRepository(BiobDataContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }
        public async Task<Genre> GetGenreByIdAsync(Guid id)
        {
            return await _context.Genres.Where(genre => genre.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Genre> GetGenreByGenreNameAsync(string genreName)
        {
            return await _context.Genres.Where(genre => genre.GenreName == genreName).FirstOrDefaultAsync();
        }
        public void AddGenre(Genre genreToAdd)
        {
            _context.Genres.Add(genreToAdd);
        }
        public void UpdateGenre(Genre genreToUpdate)
        {
            _context.Genres.Update(genreToUpdate);
        }
        public void DeleteGenre(Genre genreToDelete)
        {
            _context.Genres.Remove(genreToDelete);
        }
    }
}
