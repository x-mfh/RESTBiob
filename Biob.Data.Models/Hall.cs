using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Biob.Data.Models
{
    [Table("Halls")]
    public class Hall
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int HallNo { get; set; }
        public int NoOfSeats { get; set; }
        public bool ThreeDee { get; set; }
        public IList<HallSeat> HallSeats { get; set; }
    }
}
