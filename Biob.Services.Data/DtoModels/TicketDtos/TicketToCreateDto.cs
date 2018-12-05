using System;

namespace Biob.Services.Data.DtoModels.TicketDtos
{
    public class TicketToCreateDto
    {
        public Guid SeatId { get; set; }
        public bool Paid { get; set; }
        public int? Price { get; set; }
    }
}
