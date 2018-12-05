using Biob.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biob.Data.Models
{
    [Table("Seats")]
    public class Seat : DeleteableModelBase<Guid>
    {
        [Required(ErrorMessage = "Seat must have a Row Number")]
        public int RowNo { get; set; }
        [Required(ErrorMessage = "Seat must have a Seat Number")]
        public int SeatNo { get; set; }
        [ForeignKey("Hall")]
        public Guid HallId { get; set; }
        public Hall Hall { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

    }
}
