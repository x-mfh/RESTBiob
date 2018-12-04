using System;
using System.Linq;
using System.Threading.Tasks;
using Biob.Data.Data;
using Biob.Data.Models;
using Biob.Services.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Biob.Services.Data.Repositories
{
    public class SeatRepository : Repository, ISeatRepository
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IShowtimeRepository _showtimeRepository;

        public SeatRepository(BiobDataContext context, ITicketRepository ticketRepository, IShowtimeRepository showtimeRepository) : base(context)
        {
            _ticketRepository = ticketRepository;
            _showtimeRepository = showtimeRepository;
        }

        public async Task<PagedList<Seat>> GetAllSeatsByHallIdAsync(Guid hallId ,int pageNumber, int pageSize)
        {
            var collectionBeforePaging = _context.Seats.Where(seat => seat.HallId == hallId);
            var listToPage = await collectionBeforePaging.ToListAsync();
            return PagedList<Seat>.Create(listToPage, pageNumber, pageSize);
        }

        public async Task<Seat> GetSeatAsync(Guid id)
        {
            var foundSeat = await _context.Seats.Where(seat => seat.Id == id).FirstOrDefaultAsync();
            return foundSeat;
        }

        public void AddSeat(Guid hallId, Seat seatToAdd)
        {

            seatToAdd.HallId = hallId;
            _context.Seats.Add(seatToAdd);
        }

        public void UpdateSeat(Seat seatToUpdate)
        {
            _context.Seats.Update(seatToUpdate);
        }

        public void DeleteSeat(Seat seatToDelete)
        {
            _context.Seats.Remove(seatToDelete);
        }

        public async Task<Seat> GetFirstAvailableSeatByShowtimeIdAsync(Guid showtimeId)
        {
            //all taken seatids for showtime
            var tickets = await _ticketRepository.GetAllTicketsByShowtimeIdAsync(showtimeId, null, null, 1, 500);
            var takenOrReservedSeats = tickets.Where(ticket => !ticket.IsDeleted).Select(ticket => ticket.SeatId);
            //existing seats
            var showtime = await _showtimeRepository.GetShowtimeAsync(showtimeId);
            var seatsToSearch = await GetAllSeatsByHallIdAsync(showtime.HallId, 1, 500);
            //remove reserved seats
            var firstAvailableSeatId = seatsToSearch.Select(seat => seat.Id).Except(takenOrReservedSeats).FirstOrDefault();

            //return as seat object
            return await GetSeatAsync(firstAvailableSeatId);
        }

    }
}
