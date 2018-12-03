using System;
using System.Collections.Generic;
using System.Text;

namespace Biob.Services.Data.DtoModels
{
    public class TicketToCreateDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ShowtimeId { get; set; }
        public Guid SeatId { get; set; }
        public bool Paid { get; set; }
        public int Price { get; set; }
    }
}
