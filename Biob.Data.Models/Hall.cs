using Biob.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biob.Data.Models
{
    [Table("Halls")]
    public class Hall : DeleteableModelBase<Guid>
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int HallNo { get; set; }
        public int NoOfSeats { get; set; }
        public bool ThreeDee { get; set; }
        public IList<Seat> Seats { get; set; }
    }
}
