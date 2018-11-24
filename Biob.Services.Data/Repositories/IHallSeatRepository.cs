using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biob.Data.Models;

namespace Biob.Services.Data.Repositories
{
    public interface IHallSeatRepository : IRepository
    {
        Task<IEnumerable<HallSeat>> GetAllHallSeatsAsync();
        Task<HallSeat> GetHallSeatAsync(Guid hallSeatId);
        Task<IEnumerable<HallSeat>> GetAllByHallId(Guid hallId);
        Task<IEnumerable<HallSeat>> GetAllBySeatId(Guid seatId);
        Task<HallSeat> GetHallSeatByHallIdSeatIdAsync(Guid hallId, Guid seatId);
        void AddHallSeat(HallSeat hallSeatToAdd);
        void UpdateHallSeat(HallSeat hallSeat);
        void DeleteHallSeat(HallSeat hallSeat);
    }
}