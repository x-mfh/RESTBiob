using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Biob.Data.Data;
using Biob.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Biob.Services.Data.Helpers;
using Biob.Services.Data.DtoModels;
using Biob.Services.Web.PropertyMapping;

namespace Biob.Services.Data.Repositories
{
    public class TicketRepository : Repository, ITicketRepository
    {
        private IPropertyMappingService _propertyMappingService;

        public TicketRepository(IPropertyMappingService propertyMappingService, BiobDataContext context) : base(context)
        {
            _propertyMappingService = propertyMappingService;
            _propertyMappingService.AddPropertyMapping<TicketDto, Ticket>(new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>() { "Id" })},
                { "CustomerId", new PropertyMappingValue(new List<string>() { "CustomerId" })},
                { "ShowtimeId", new PropertyMappingValue(new List<string>() { "ShowtimeId" })},
                { "HallseatId", new PropertyMappingValue(new List<string>() { "HallseatId" })},
                { "Reserved", new PropertyMappingValue(new List<string>() { "Reserved" })},
                { "Paid", new PropertyMappingValue(new List<string>() { "Paid" })},
                { "Price", new PropertyMappingValue(new List<string>() { "Price" })},
            });
        }

        //public TicketRepository(BiobDataContext context) : base(context)
        //{

        //}
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

        public async Task<PagedList<Ticket>> GetTicketsByCustomerIdAsync(Guid customerId, string orderBy, string searchQuery, int pageNumber, int pageSize)
        {
            var collectionsBeforePaging = await _context.Tickets.Applysort(orderBy, _propertyMappingService.GetPropertyMapping<TicketDto, Ticket>()).ToListAsync();

            //TODO: Make this search thing be able to have a list of names on movies that the showtime is for. 

            //Could perhaps be done with a entity framework equivalent for the select query: 
            //  SELECT fieldsNeeded 
            //  FROM Tickets t 
            //  INNER JOIN ShowTimes st 
            //      ON st.Id = t.ShowTimeId 
            //  INNER JOIN Movies m 
            //      ON m.Id = st.MovieId 
            //  WHERE m.Title LIKE '% <searchString> %' 
            //      AND t.CustomerId = <customerId>

            //if (!string.IsNullOrWhiteSpace(searchQuery))
            //{
            //    string searchQueryForWhere = searchQuery.Trim().ToLowerInvariant();
            //    collectionsBeforePaging = collectionsBeforePaging
            //          <perhaps add the join here to a list?>
            //        .Where(ticket => ticket.Title.ToLowerInvariant().Contains(searchQueryForWhere)).ToList();
            //}

            return PagedList<Ticket>.Create(collectionsBeforePaging, pageNumber, pageSize);
        }


        public void UpdateTicket(Ticket ticketToUpdate)
        {
            //  TODO: consider changing update to attach
            //  if things dont work as expected
            _context.Tickets.Update(ticketToUpdate);
        }
    }
}
