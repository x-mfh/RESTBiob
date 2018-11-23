using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biob.Data.Data;
using Biob.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Biob.Services.Data.Repositories
{
    public class HallSeatRepository : Repository, IHallSeatRepository
    {
        public HallSeatRepository(BiobDataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<HallSeat>> GetAllHallSeatsAsync()
        {
            return  await _context.HallSeats.ToListAsync();
        }

        public async Task<HallSeat> GetHallSeatAsync(int hallSeatId)
        {
            return await _context.HallSeats.Where(hallseat => hallseat.Id == hallSeatId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<HallSeat>> GetAllByHallId(int hallId)
        {
            return await _context.HallSeats.Include(hallseat => hallseat.Seat)
                                           .Where(hall => hall.HallId == hallId).ToListAsync();
        }

        public async Task<IEnumerable<HallSeat>> GetAllBySeatId(int seatId)
        {
            return await _context.HallSeats.Where(seat => seat.SeatId == seatId).ToListAsync();
        }

        public async Task<HallSeat> GetHallSeatByHallIdSeatIdAsync(int hallId, int seatId)
        {
            return await _context.HallSeats.Where(hallseat => hallseat.HallId == hallId && hallseat.SeatId == seatId).FirstOrDefaultAsync();
        }

        public void AddHallSeat(HallSeat hallSeatToAdd)
        {
            _context.HallSeats.Add(hallSeatToAdd);
        }

        public void UpdateHallSeat(HallSeat hallSeatToUpdate)
        {
            //_context.Attach(hallSeat).State = EntityState.Modified;
            _context.HallSeats.Update(hallSeatToUpdate);
        }

        public void DeleteHallSeat(HallSeat hallSeatToDelete)
        {
            hallSeatToDelete.IsDeleted = true;
            hallSeatToDelete.DeletedOn = DateTimeOffset.Now;
        }
    }
}
