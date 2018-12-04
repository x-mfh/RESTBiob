using Biob.Data.Models;
using Biob.Services.Data.Helpers;
using System;
using System.Threading.Tasks;

namespace Biob.Services.Data.Repositories
{
    public interface ITicketRepository : IRepository
    {
        Task<PagedList<Ticket>> GetAllTicketsByShowtimeIdAsync(Guid showtimeId, string orderBy, string searchQuery, int pageNumber, int pageSize);
        Task<Ticket> GetTicketAsync(Guid id);
        void AddTicket(Ticket ticketToAdd);
        void UpdateTicket(Ticket ticketToUpdate);
        void DeleteTicket(Ticket ticketToDelete);
    }
}
