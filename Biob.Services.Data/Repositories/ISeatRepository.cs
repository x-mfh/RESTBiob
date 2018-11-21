using System.Collections.Generic;
using System.Threading.Tasks;
using Biob.Data.Models;

namespace Biob.Services.Data.Repositories
{
    public interface ISeatRepository : IRepository
    {
        Task<IEnumerable<Seat>> GetAllSeatsAsync();
        Task<Seat> GetSeatAsync(int id);
        void AddSeat(Seat seatToAdd);
        void UpdateSeat(Seat seatToUpdate);
        void DeleteSeat(Seat seatToDelete);
    }
}