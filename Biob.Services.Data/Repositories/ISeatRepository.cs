using System;
using System.Threading.Tasks;
using Biob.Data.Models;
using Biob.Services.Data.Helpers;

namespace Biob.Services.Data.Repositories
{
    public interface ISeatRepository : IRepository
    {
        Task<PagedList<Seat>> GetAllSeatsByHallIdAsync(Guid hallId,int PageNumer, int PageSize);
        Task<Seat> GetSeatAsync(Guid id);
        void AddSeat(Guid hallId, Seat seatToAdd);
        void UpdateSeat(Seat seatToUpdate);
        void DeleteSeat(Seat seatToDelete);
        Task<Seat> GetFirstAvailableSeatByShowtimeIdAsync(Guid showtimeId);
    }
}