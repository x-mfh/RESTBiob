using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biob.Data.Data;
using Biob.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Biob.Services.Data.Repositories
{
    public class MovieGenreRepository : Repository, IMovieGenreRepository
    {
        public MovieGenreRepository(BiobDataContext context) : base(context)
        {
        }

        // remove ?
        public async Task<IEnumerable<MovieGenre>> GetAllMovieGenresAsync()
        {
            return await _context.MovieGenres.ToListAsync();
        }

        public async Task<MovieGenre> GetMovieGenreByIdAsync(Guid movieGenreId)
        {
            return await _context.MovieGenres.Where(moviegenre => moviegenre.Id == movieGenreId).FirstOrDefaultAsync();
        }

        // uses ICollection to get count property
        public async Task<ICollection<MovieGenre>> GetAllMovieGenresByMovieIdAsync(Guid movieId)
        {
            return await _context.MovieGenres.Include(moviegenre => moviegenre.Genre)
                                             .Where(movie => movie.MovieId == movieId).ToListAsync();
        }

        // include movie ?
        public async Task<IEnumerable<MovieGenre>> GetAllMovieGenresByGenreIdAsync(Guid genreId)
        {
            return await _context.MovieGenres.Where(genre => genre.GenreId == genreId).ToListAsync();
        }

        public async Task<MovieGenre> GetMovieGenreByMovieIdGenreIdAsync(Guid movieId, Guid genreId)
        {
            return await _context.MovieGenres.Include(moviegenre => moviegenre.Genre)
                                             .Where(moviegenre => moviegenre.MovieId == movieId && moviegenre.GenreId == genreId).FirstOrDefaultAsync();
        }

        public void AddMovieGenre(MovieGenre movieGenreToAdd)
        {
            _context.MovieGenres.Add(movieGenreToAdd);
        }

        public void UpdateMovieGenre(MovieGenre movieGenreToUpdate)
        {
            //_context.Attach(movieGenre).State = EntityState.Modified;
            _context.MovieGenres.Update(movieGenreToUpdate);
        }

        public void DeleteMovieGenre(MovieGenre movieGenreToDelete)
        {
            movieGenreToDelete.IsDeleted = true;
            movieGenreToDelete.DeletedOn = DateTimeOffset.Now;
        }
    }
}