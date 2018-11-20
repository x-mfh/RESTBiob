using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Biob.Data.Data;
using Biob.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Biob.Services.Data.Repositories
{
    class TicketRepository : Repository, ITicketRepository
    {
        public TicketRepository(BiobDataContext context) : base(context)
        {

        }
        public void AddTicket(Ticket ticketToAdd)
        {
            if (ticketToAdd.Id == Guid.Empty)
            {
                ticketToAdd.Id = Guid.NewGuid();
            }
            _context.Tickets.Add(ticketToAdd);
        }

        public void DeleteTicket(Ticket ticketToDelete)
        {
            _context.Tickets.Remove(ticketToDelete);
        }

        public async Task<Ticket> GetTicketAsync(Guid id)
        {
            return await _context.Tickets.Where(ticket => ticket.Id == id).FirstOrDefaultAsync();
        }


        public void UpdateTicket(Ticket ticketToUpdate)
        {
            //  TODO: consider changing update to attach
            //  if things dont work as expected
            _context.Tickets.Update(ticketToUpdate);
        }
    }
}
