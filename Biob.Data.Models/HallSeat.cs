using Biob.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biob.Data.Models
{
    [Table("HallSeats")]
    public class HallSeat : DeleteableModelBase<Guid>
    {
        [ForeignKey("Hall")]
        public Guid HallId { get; set; }
        public Hall Hall { get; set; }
        [ForeignKey("Seat")]
        public Guid SeatId { get; set; }
        public Seat Seat { get; set; }
    }
}
