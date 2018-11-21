using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biob.Data.Models;
using Biob.Services.Data.Helpers;

namespace Biob.Services.Data.Repositories
{
    public interface IMovieRepository : IRepository
    {
        Task<PagedList<Movie>> GetAllMoviesAsync(string orderBy, string searchQuery, int pageNumber, int pageSize);
        Task<Movie> GetMovieAsync(Guid id);
        void AddMovie(Movie movieToAdd);
        void UpdateMovie(Movie movieToUpdate);
        void DeleteMovie(Movie movieToDelete);
    }
}
