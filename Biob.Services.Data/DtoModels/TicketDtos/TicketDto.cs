using System;

namespace Biob.Services.Data.DtoModels.TicketDtos
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ShowtimeId { get; set; }
        public Guid SeatId { get; set; }
        public bool Paid { get; set; }
        public int Price { get; set; }
    }
}
