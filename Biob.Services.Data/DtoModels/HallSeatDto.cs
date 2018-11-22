using Biob.Data.Models;

namespace Biob.Services.Data.DtoModels
{
    public class HallSeatDto
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public int SeatId { get; set; }
        public int RowNo { get; set; }
        public int SeatNo { get; set; }
    }
}
