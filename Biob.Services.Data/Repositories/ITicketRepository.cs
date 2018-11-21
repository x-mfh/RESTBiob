using Biob.Data.Models;
using System;
using System.Threading.Tasks;

namespace Biob.Services.Data.Repositories
{
    public interface ITicketRepository : IRepository
    {
        Task<Ticket> GetTicketAsync(Guid id);
        void AddTicket(Ticket ticketToAdd);
        void UpdateTicket(Ticket ticketToUpdate);
        void DeleteTicket(Ticket ticketToDelete);
    }
}
