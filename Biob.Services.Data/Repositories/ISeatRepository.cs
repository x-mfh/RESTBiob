using System.Collections.Generic;
using System.Threading.Tasks;
using Biob.Data.Models;

namespace Biob.Services.Data.Repositories
{
    public interface ISeatRepository : IRepository
    {
        Task<IEnumerable<Seat>> GetAllSeatsAsync(int hallId);
        //Task<Seat> GetSeatByRowNoSeatNoAsync(int rowNo, int seatNo);
        //Task<IEnumerable<Seat>> GetSeatsByRowNoAsync(int rowNo);
        //Task<IEnumerable<Seat>> GetSeatsBySeatNoAsync(int seatNo);
        Task<Seat> GetSeatAsync(int id);
        void AddSeat(int hallId, Seat seatToAdd);
        void UpdateSeat(Seat seatToUpdate);
        void DeleteSeat(Seat seatToDelete);
    }
}