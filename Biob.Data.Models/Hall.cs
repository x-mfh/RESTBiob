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
        public int HallId { get; set; }
        [Required]
        public int HallNr { get; set; }
        // discuss if necessary
        public int NrOfSeats { get; set; }
        // make default false?
        public bool ThreeDee { get; set; }
    }
}
