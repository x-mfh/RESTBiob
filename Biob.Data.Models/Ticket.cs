﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Biob.Data.Models
{
    [Table("Tickets")]
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("Showtime")]
        public int ShowtimeId { get; set; }
        [ForeignKey("HallSeatId")]
        public int HallSeatId { get; set; }
        // discuss
        public bool Reserved { get; set; }
        // discuss
        public bool Paid { get; set; }
        public int Price { get; set; }
    }
}
