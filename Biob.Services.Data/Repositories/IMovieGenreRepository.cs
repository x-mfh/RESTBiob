using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biob.Data.Models;

namespace Biob.Services.Data.Repositories
{
    public interface IMovieGenreRepository : IRepository
    {
        Task<IEnumerable<MovieGenre>> GetAllMovieGenresAsync();
        Task<IEnumerable<MovieGenre>> GetAllMovieGenresByGenreIdAsync(Guid genreId);
        Task<ICollection<MovieGenre>> GetAllMovieGenresByMovieIdAsync(Guid movieId);
        Task<MovieGenre> GetMovieGenreByIdAsync(Guid movieGenreId);
        Task<MovieGenre> GetMovieGenreByMovieIdGenreIdAsync(Guid movieId, Guid genreId);
        void AddMovieGenre(MovieGenre movieGenreToAdd);
        void DeleteMovieGenre(MovieGenre movieGenreToDelete);
        void UpdateMovieGenre(MovieGenre movieGenreToUpdate);
    }
}