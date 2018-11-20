using System;
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
        public Guid TicketId { get; set; }
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        [ForeignKey("Showtime")]
        public Guid ShowtimeId { get; set; }
        [ForeignKey("HallSeatId")]
        public int HallSeatId { get; set; }
        public bool Reserved { get; set; }
        public bool Paid { get; set; }
        public int Price { get; set; }
    }
}
