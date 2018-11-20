using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Biob.Data.Models
{
    [Table("Showtimes")]
    public class Showtime
    {
        [Key]
        public int ShowtimeId { get; set; }
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        [ForeignKey("Hall")]
        public int HallId { get; set; }
        // discuss name
        public DateTimeOffset DateOfPlaying { get; set; }
        // discuss name
        public DateTimeOffset TimeOfPlaying { get; set; }
        public bool ThreeDee { get; set; }
    }
}
