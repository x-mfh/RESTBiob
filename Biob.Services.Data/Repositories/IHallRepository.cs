using System.Collections.Generic;
using System.Threading.Tasks;
using Biob.Data.Models;

namespace Biob.Services.Data.Repositories
{
    public interface IHallRepository : IRepository
    {
        void AddHall(Hall hallToAdd);
        void DeleteHall(Hall hallToDelete);
        Task<Hall> GetHallAsync(int id);
        Task<IEnumerable<Hall>> GetAllHallsAsync();
        void UpdateHall(Hall hallToUpdate);
    }
}