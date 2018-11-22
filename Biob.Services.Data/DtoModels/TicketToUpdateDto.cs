using System;
using System.Collections.Generic;
using System.Text;

namespace Biob.Services.Data.DtoModels
{
    public class TicketToUpdateDto
    {
        public Guid Id { get; set; }
        //If another customer needs the ticket instead, the ticket should probably just be "deleted" and a new be created instead?
        //TODO: If tickets are deleted, there might be some need for some logging of deleted tickets, and perhaps we should also keep a transaction log for time of payment? 
        // - we are, afterall, talking about payments. It would be good to have some historic data on this if all fucks up
        //public Guid CustomerId { get; set; }

        //The same - if you bought for the wrong show, i think the ticket should rather be "deleted" and a new be created. 
        //public Guid ShowtimeId { get; set; }

        //Not sure if this should be changed either...:
        //public int HallSeatId { get; set; }

        //TODO: I'm thinking it COULD (not necessarily) make sense to either 
        //  1: change "Reserved" field to an active/inactive field 
        //  2: Remove "Reserved" field and make a log table for all tickets. This way, to check if a seat is reserved on a specific showtime, you simply just check if a ticket exists. Not whether it's "reserved" or not
            
        public bool Reserved { get; set; }

        public bool Paid { get; set; }

        //Not sure if Price should be changeable either...
        //public int Price { get; set; }
    }
}
