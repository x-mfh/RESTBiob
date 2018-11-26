using Biob.Data.Common.Models;
using System;
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
        public Hall Hall { get; set; }
        public DateTimeOffset TimeOfPlaying { get; set; }
        public bool ThreeDee { get; set; }
    }
}
