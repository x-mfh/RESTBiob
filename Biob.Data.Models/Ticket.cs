using Biob.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Biob.Data.Models
{
    [Table("Tickets")]
    public class Ticket : DeleteableModelBase<Guid>
    {
        [ForeignKey("Customer")] //I wonder why EF doesn't cry about this
        public Guid CustomerId { get; set; }
        [ForeignKey("Showtime")]
        public Guid ShowtimeId { get; set; }
        [ForeignKey("Seat")]
        public Guid SeatId { get; set; }
        public bool Paid { get; set; }
        public decimal Price { get; set; }

        //Foreign key fields for EF database generation:
        //public Customer Customer { get; set; } //Can't do this before Customer.cs is made, and when it's made it should have a "public ICollection<Ticket> Tickets { get; set; }" property
        public Showtime Showtime { get; set; }
        public Seat Seat { get; set; }
    }
}
