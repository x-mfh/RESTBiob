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

        public async Task<IEnumerable<Seat>> GetAllSeatsAsync()
        {
            return await _context.Seats.ToListAsync();
        }

        public async Task<Seat> GetSeatAsync(int id)
        {
            return await _context.Seats.Where(seat => seat.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Seat> GetSeatByRowNoSeatNoAsync(int rowNo, int seatNo)
        {
            return await _context.Seats.Where(seat => seat.RowNo == rowNo && seat.SeatNo == seatNo).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Seat>> GetSeatsByRowNoAsync(int rowNo)
        {
            return await _context.Seats.Where(seat => seat.RowNo == rowNo).ToListAsync();

        }

        public async Task<IEnumerable<Seat>> GetSeatsBySeatNoAsync(int seatNo)
        {
            return await _context.Seats.Where(seat =>  seat.SeatNo == seatNo).ToListAsync();

        }

        public void AddSeat(Seat seatToAdd)
        {
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
