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

        public void AddShowtime(Showtime showtimeToAdd)
        {
            if (showtimeToAdd.Id == Guid.Empty)
            {
                showtimeToAdd.Id = Guid.NewGuid();
            }
            _context.Showtimes.Add(showtimeToAdd);
        }

        public void DeleteShowtime(Showtime showtimeToDelete)
        {
            _context.Showtimes.Remove(showtimeToDelete);
        }

        public async Task<IEnumerable<Showtime>> GetAllShowtimesAsync()
        {
            return await _context.Showtimes.ToListAsync();
        }

        public async Task<Showtime> GetShowtimeAsync(Guid id)
        {
            return await _context.Showtimes.Where(showtime => showtime.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Showtime>> GetShowtimesForMovieAsync(Guid movieId)
        {
            return await _context.Showtimes.Where(showtime => showtime.MovieId == movieId).ToListAsync();
        }

        public void UpdateShowtime(Showtime showtimeToUpdate)
        {
            _context.Showtimes.Update(showtimeToUpdate);
        }
    }
}
