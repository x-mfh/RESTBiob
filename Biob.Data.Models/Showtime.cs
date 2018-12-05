using Biob.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biob.Data.Models
{
    [Table("Showtimes")]
    public class Showtime : DeleteableModelBase<Guid>
    {
        [ForeignKey("Movie")]
        public Guid MovieId { get; set; }
        [ForeignKey("Hall")]
        public Guid HallId { get; set; }
        [Required(ErrorMessage = "Showtime time of playing is required")]
        public DateTimeOffset TimeOfPlaying { get; set; }
        public bool ThreeDee { get; set; }
        public Movie Movie { get; set; }
        public Hall Hall { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
