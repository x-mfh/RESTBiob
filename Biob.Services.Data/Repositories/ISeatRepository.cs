using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biob.Data.Models;

namespace Biob.Services.Data.Repositories
{
    public interface ISeatRepository : IRepository
    {
        Task<IEnumerable<Seat>> GetAllSeatsAsync(Guid hallId);
        Task<Seat> GetSeatAsync(Guid id);
        void AddSeat(Guid hallId, Seat seatToAdd);
        void UpdateSeat(Seat seatToUpdate);
        void DeleteSeat(Seat seatToDelete);
    }
}