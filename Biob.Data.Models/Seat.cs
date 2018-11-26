using Biob.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biob.Data.Models
{
    [Table("Seats")]
    public class Seat : DeleteableModelBase<Guid>
    {
        // add unique constraint for rowno & seatno together, so you can't have two RowNo = 1; SeatNo = 1;
        [Required]
        public int RowNo { get; set; }
        [Required]
        public int SeatNo { get; set; }
        [ForeignKey("Hall")]
        public Guid HallId { get; set; }
    }
}
