using Biob.Data.Data;
using System.Threading.Tasks;

namespace Biob.Services.Data.Repositories
{
    public class Repository : IRepository
    {
        protected readonly BiobDataContext _context;

        public Repository(BiobDataContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return ( await _context.SaveChangesAsync() > 0);
        }
    }
}
