using System.Threading.Tasks;

namespace Biob.Services.Data.Repositories
{
    public interface IRepository
    {
        Task<bool> SaveChangesAsync();
    }
}