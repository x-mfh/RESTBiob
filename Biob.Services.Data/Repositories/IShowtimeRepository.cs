using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biob.Data.Models;

namespace Biob.Services.Data.Repositories
{
    public interface IShowtimeRepository : IRepository
    {
        Task<IEnumerable<Showtime>> GetAllShowtimesAsync(Guid movieId);
        Task<Showtime> GetShowtimeAsync(Guid showtimeId, Guid movieId);
        void AddShowtime(Guid movieId, Showtime showtimeToAdd);
        void UpdateShowtime(Showtime showtimeToUpdate);
        void DeleteShowtime(Showtime showtimeToDelete);
    }
}
