using System;

namespace Biob.Services.Data.DtoModels.TicketDtos
{
    public class TicketToCreateDto
    {
        //CustomerId
        public Guid SeatId { get; set; }
        public bool Paid { get; set; }
        //If default prices per movie is made, this should be nullable and use that if null in controller
        public int Price { get; set; }
    }
}
