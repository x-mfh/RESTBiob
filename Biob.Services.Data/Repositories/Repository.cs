using Biob.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Biob.Services.Data.Repositories
{
    public class Repository : IRepository
    {
        protected readonly BiobDataContext _context;

        public Repository(BiobDataContext context)
        {
            _context = context;
        }

        public async Task<bool> EntityExists<T>(Guid id) where T : class
        {
            var foundEntity = await _context.Set<T>().FirstOrDefaultAsync();
            return foundEntity == null ? false : true;
        }

        public async Task<bool> TicketExists(Guid id)
        {
            var ticketFromDb = await _context.Tickets.Where(ticket => ticket.Id == id).FirstOrDefaultAsync();
            return ticketFromDb == null ? false : true;
        }

        public async Task<bool> ShowtimeExists(Guid id)
        {
            var showtimeFromDb = await _context.Showtimes.Where(showtime => showtime.Id == id).FirstOrDefaultAsync();
            return showtimeFromDb == null ? false : true;
        }

        public async Task<bool> HallExists(Guid id)
        {
            var hallFromDb = await _context.Halls.Where(hall => hall.Id == id).FirstOrDefaultAsync();
            return hallFromDb == null ? false : true;
        }

        public async Task<bool> MovieExists(Guid id)
        {
            var movieFromDb = await _context.Movies.Where(movie => movie.Id == id).FirstOrDefaultAsync();
            return movieFromDb == null ? false : true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return ( await _context.SaveChangesAsync() > 0);
        }
    }
}
