using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biob.Data.Models;

namespace Biob.Services.Data.Repositories
{
    public interface IGenreRepository : IRepository
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<Genre> GetGenreByGenreNameAsync(string genreName);
        Task<Genre> GetGenreByIdAsync(Guid id);
        void AddGenre(Genre genreToAdd);
        void UpdateGenre(Genre genreToUpdate);
        void DeleteGenre(Genre genreToDelete);
    }
}