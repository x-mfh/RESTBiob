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

        public async Task<IEnumerable<Seat>> GetAllSeatsAsync(int hallId)
        {
            return await _context.HallSeats
                               .Where(hallseat => !hallseat.IsDeleted && hallseat.HallId == hallId)
                               .Include(y => y.Seat).Select(x => x.Seat)
                               .Where(h => !h.IsDeleted).ToListAsync();
            //return await _context.Seats.Where(seats => !seats.IsDeleted && seats.).ToListAsync();
        }

        public async Task<Seat> GetSeatAsync(int id)
        {
            var foundSeat = await _context.Seats.Where(seat => seat.Id == id).FirstOrDefaultAsync();
            if (foundSeat.IsDeleted)
            {
                foundSeat = null;
            }

            return foundSeat;
        }

        //public async Task<Seat> GetSeatByRowNoSeatNoAsync(int rowNo, int seatNo)
        //{
        //    return await _context.Seats.Where(seat => seat.RowNo == rowNo && seat.SeatNo == seatNo).FirstOrDefaultAsync();
        //}

        //public async Task<IEnumerable<Seat>> GetSeatsByRowNoAsync(int rowNo)
        //{
        //    return await _context.Seats.Where(seat => seat.RowNo == rowNo).ToListAsync();

        //}

        //public async Task<IEnumerable<Seat>> GetSeatsBySeatNoAsync(int seatNo)
        //{
        //    return await _context.Seats.Where(seat =>  seat.SeatNo == seatNo).ToListAsync();

        //}

        public void AddSeat(int hallId, Seat seatToAdd)
        {
            //  TODO: change IDs to GUID
            //  and add ID here if its null
            HallSeat newHallSeat = new HallSeat()
            {
                HallId = hallId,
                SeatId = seatToAdd.Id
            };
            _context.HallSeats.Add(newHallSeat);
            _context.Seats.Add(seatToAdd);
        }

        public void UpdateSeat(Seat seatToUpdate)
        {
            _context.Seats.Update(seatToUpdate);
        }

        public async void DeleteSeat(Seat seatToDelete)
        {
            seatToDelete.IsDeleted = true;
            seatToDelete.DeletedOn = DateTimeOffset.Now;
            var hallSeat = await _context.HallSeats.Where(x => x.SeatId == seatToDelete.Id).FirstOrDefaultAsync();
            hallSeat.IsDeleted = true;
            hallSeat.DeletedOn = DateTimeOffset.Now;
        }

    }
}
