using System;
using System.Threading.Tasks;

namespace Biob.Services.Data.Repositories
{
    public interface IRepository
    {
        Task<bool> MovieExists(Guid id);
        Task<bool> SaveChangesAsync();
    }
}