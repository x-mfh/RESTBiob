using System;

namespace Biob.Services.Data.DtoModels.TicketDtos
{
    public class TicketToUpdateDto
    {
        
        //Instead of updating the id's of SeatId, CustomerId and ShowtimeId, the ticket should be deleted and a new be created.
        
        //Nullable to prevent them getting their default value 0 when not provided. Not entirely sure if necessary. 
        public bool? Paid { get; set; }
        public int? Price { get; set; }
    }
}
