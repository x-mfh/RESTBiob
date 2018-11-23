using Biob.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Biob.Data.Models
{
    [Table("Showtimes")]
    public class Showtime : DeleteableModelBase<Guid>
    {
        [ForeignKey("Movie")]
        public Guid MovieId { get; set; }
        [ForeignKey("Hall")]
        public int HallId { get; set; }
        public DateTimeOffset TimeOfPlaying { get; set; }
        public bool ThreeDee { get; set; }
    }
}
