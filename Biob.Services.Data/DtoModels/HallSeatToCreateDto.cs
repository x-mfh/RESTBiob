using System.ComponentModel.DataAnnotations;

namespace Biob.Services.Data.DtoModels
{
    public class HallSeatToCreateDto
    {
        public int SeatId { get; set; }
        [Required]
        public int RowNo { get; set; }
        [Required]
        public int SeatNo { get; set; }
    }
}
