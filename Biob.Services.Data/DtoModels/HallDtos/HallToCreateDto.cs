using System.ComponentModel.DataAnnotations;

namespace Biob.Services.Data.DtoModels.HallDtos
{
    public class HallToCreateDto
    {
        [Required]
        public int? HallNo { get; set; }
        public int? NoOfSeats { get; set; }
        public bool ThreeDee { get; set; }
    }
}
