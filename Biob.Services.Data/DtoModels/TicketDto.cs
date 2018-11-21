using System;
using System.Collections.Generic;
using System.Text;

namespace Biob.Services.Data.DtoModels
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ShowtimeId { get; set; }
        //Todo: consider changing integer to guid if not already done (all places)
        public int HallSeatId { get; set; }
        public bool Reserved { get; set; }
        public bool Paid { get; set; }
        public int Price { get; set; }
    }
}
