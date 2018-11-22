using System.Collections.Generic;
using System.Threading.Tasks;
using Biob.Data.Models;

namespace Biob.Services.Data.Repositories
{
    public interface IHallSeatRepository : IRepository
    {
        Task<IEnumerable<HallSeat>> GetAllHallSeatsAsync();
        Task<HallSeat> GetHallSeatAsync(int hallSeatId);
        Task<IEnumerable<HallSeat>> GetAllByHallId(int hallId);
        Task<IEnumerable<HallSeat>> GetAllBySeatId(int seatId);
        void AddHallSeat(HallSeat hallSeatToAdd);
        void UpdateHallSeat(HallSeat hallSeat);
        void DeleteHallSeat(HallSeat hallSeat);
    }
}