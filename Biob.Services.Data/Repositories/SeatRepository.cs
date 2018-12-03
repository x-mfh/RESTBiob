using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biob.Data.Data;
using Biob.Data.Models;
using Biob.Services.Data.DtoModels;
using Biob.Services.Data.Helpers;
using Biob.Services.Web.PropertyMapping;
using Microsoft.EntityFrameworkCore;

namespace Biob.Services.Data.Repositories
{
    public class SeatRepository : Repository, ISeatRepository
    {
        private IPropertyMappingService _propertyMappingService;

        public SeatRepository(IPropertyMappingService propertyMappingService, BiobDataContext context) : base(context)
        {
            _propertyMappingService = propertyMappingService;
            _propertyMappingService.AddPropertyMapping<SeatDto, Seat>(new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>() { "Id" })},
                { "RowNo", new PropertyMappingValue(new List<string>() { "RowNo" })},
                { "SeatNo", new PropertyMappingValue(new List<string>() { "SeatNo" })},
            });

        }

        public async Task<PagedList<Seat>> GetAllSeatsAsync(int pageNumber, int pageSize)
        {
            var collectionBeforePaging = _context.Seats.Where(seat => !seat.IsDeleted);
            var listToPage = await collectionBeforePaging.ToListAsync();
            return PagedList<Seat>.Create(listToPage, pageNumber, pageSize);
        }

        public async Task<Seat> GetSeatAsync(Guid id)
        {
            var foundSeat = await _context.Seats.Where(seat => seat.Id == id).FirstOrDefaultAsync();
            if (foundSeat.IsDeleted)
            {
                foundSeat = null;
            }

            return foundSeat;
        }

        public void AddSeat(Guid hallId, Seat seatToAdd)
        {

            seatToAdd.HallId = hallId;
            _context.Seats.Add(seatToAdd);
        }

        public void UpdateSeat(Seat seatToUpdate)
        {
            _context.Seats.Update(seatToUpdate);
        }

        public void DeleteSeat(Seat seatToDelete)
        {
            seatToDelete.IsDeleted = true;
            seatToDelete.DeletedOn = DateTimeOffset.Now;
        }

    }
}
