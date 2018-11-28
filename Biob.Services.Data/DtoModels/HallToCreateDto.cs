using System;
using System.ComponentModel.DataAnnotations;

namespace Biob.Services.Data.DtoModels
{
    public class HallToCreateDto
    {
        public Guid Id { get; set; }
        [Required]
        public int? HallNo { get; set; }
        public int NoOfSeats { get; set; }
        public bool ThreeDee { get; set; }
    }
}
