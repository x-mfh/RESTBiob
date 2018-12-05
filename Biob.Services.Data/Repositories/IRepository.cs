using System;
using System.Threading.Tasks;

namespace Biob.Services.Data.Repositories
{
    public interface IRepository
    {
        Task<bool> MovieExists(Guid id);
        Task<bool> HallExists(Guid id);
        Task<bool> TicketExists(Guid id);
        Task<bool> ShowtimeExists(Guid id);
        Task<bool> EntityExists<T>(Guid id) where T : class;
        Task<bool> SaveChangesAsync();
    }
}