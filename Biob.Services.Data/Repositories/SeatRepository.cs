using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biob.Data.Data;
using Biob.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Biob.Services.Data.Repositories
{
    public class SeatRepository : Repository, ISeatRepository
    {
        public SeatRepository(BiobDataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Seat>> GetAllSeatsAsync(Guid hallId)
        {
            return await _context.Seats.Where(seat => !seat.IsDeleted && seat.HallId == hallId).ToListAsync();
        }

        public async Task<Seat> GetSeatAsync(Guid id)
        {
            var foundSeat = await _context.Seats.Where(seat => seat.Id == id).FirstOrDefaultAsync();
            if (foundSeat.IsDeleted)
            {
                foundSeat = null;
            }

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
            seatToDelete.IsDeleted = true;
            seatToDelete.DeletedOn = DateTimeOffset.Now;
        }

    }
}
