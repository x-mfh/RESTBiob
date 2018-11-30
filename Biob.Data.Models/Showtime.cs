using Biob.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biob.Data.Models
{
    [Table("Showtimes")]
    public class Showtime : DeleteableModelBase<Guid>
    {
        //Fields
        [ForeignKey("Movie")]
        public Guid MovieId { get; set; }
        [ForeignKey("Hall")]
        public Guid HallId { get; set; }
        public DateTimeOffset TimeOfPlaying { get; set; }
        public bool ThreeDee { get; set; }

        //Foreign key
        public Movie Movie { get; set; }
        public Hall Hall { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
