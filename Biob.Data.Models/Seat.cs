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
        public int Id { get; set; }
        [Required]
        public int RowNo { get; set; }
        [Required]
        public int SeatNo { get; set; }
        public IList<HallSeat> HallSeats { get; set; }
    }
}
