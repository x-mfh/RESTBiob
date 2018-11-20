using System;
using System.Collections.Generic;
using System.Text;

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
