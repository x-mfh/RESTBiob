using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Biob.Data.Models
{
    [Table("Seats")]
    public class Seat
    {
        [Key]
        public int SeatId { get; set; }
        [Required]
        public int RowNr { get; set; }
        [Required]
        public int SeatNr { get; set; }
    }
}
