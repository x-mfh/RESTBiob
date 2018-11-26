using System;
using System.Threading.Tasks;
using Biob.Data.Models;
using Biob.Services.Data.Helpers;

namespace Biob.Services.Data.Repositories
{
    public interface IShowtimeRepository : IRepository
    {
        Task<PagedList<Showtime>> GetAllShowtimesAsync(string orderBy, string searchQuery, int pageNumber, int pageSize);
        Task<Showtime> GetShowtimeAsync(Guid showtimeId, Guid movieId);
        void AddShowtime(Guid movieId, Showtime showtimeToAdd);
        void UpdateShowtime(Guid movieId, Showtime showtimeToUpdate);
        void DeleteShowtime(Showtime showtimeToDelete);
    }
}
