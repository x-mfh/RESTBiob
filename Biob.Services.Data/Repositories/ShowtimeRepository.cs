using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biob.Data.Data;
using Biob.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Biob.Services.Data.Repositories
{
    public class ShowtimeRepository : Repository, IShowtimeRepository
    {
        public ShowtimeRepository(BiobDataContext context) : base(context)
        {

        }

        public void AddShowtime(Guid movieId, Showtime showtimeToAdd)
        {
            if (showtimeToAdd.Id == Guid.Empty)
            {
                showtimeToAdd.Id = Guid.NewGuid();
            }
            if (showtimeToAdd.MovieId == Guid.Empty)
            {
                showtimeToAdd.MovieId = movieId;
            }
            _context.Showtimes.Add(showtimeToAdd);
        }

        public void DeleteShowtime(Showtime showtimeToDelete)
        {
            _context.Showtimes.Remove(showtimeToDelete);
        }

        public async Task<IEnumerable<Showtime>> GetAllShowtimesAsync(Guid movieId)
        {
            return await _context.Showtimes.Where(showtime => showtime.MovieId == movieId).ToListAsync();
        }

        public async Task<Showtime> GetShowtimeAsync( Guid showtimeId, Guid movieId)
        {
            return await _context.Showtimes.Where(showtime => showtime.MovieId == movieId && showtime.Id == showtime.Id).FirstOrDefaultAsync();
        }

        public void UpdateShowtime(Guid movieId, Showtime showtimeToUpdate)
        {
            if (showtimeToUpdate.Id == Guid.Empty)
            {
                showtimeToUpdate.Id = showtimeToUpdate.Id;
            }
            if (showtimeToUpdate.MovieId == Guid.Empty)
            {
                showtimeToUpdate.MovieId = movieId;
            }
            _context.Showtimes.Update(showtimeToUpdate);
        }
    }
}
