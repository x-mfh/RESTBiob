using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biob.Data.Models;

namespace Biob.Services.Data.Repositories
{
    public interface IMovieRepository : IRepository
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMovieAsync(Guid id);
        void AddMovie(Movie movieToAdd);
        void UpdateMovie(Movie movieToUpdate);
        void DeleteMovie(Movie movieToDelete);
    }
}
