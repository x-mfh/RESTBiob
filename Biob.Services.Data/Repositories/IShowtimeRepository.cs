using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biob.Data.Models;

namespace Biob.Services.Data.Repositories
{
    public interface IShowtimeRepository : IRepository
    {
        Task<IEnumerable<Showtime>> GetShowtimesForMovieAsync(Guid movieId);
        Task<IEnumerable<Showtime>> GetAllShowtimesAsync();
        Task<Showtime> GetShowtimeAsync(Guid showtimeId);
        void AddShowtime(Showtime showtimeToAdd);
        void UpdateShowtime(Showtime showtimeToUpdate);
        void DeleteShowtime(Showtime showtimeToDelete);
    }
}
