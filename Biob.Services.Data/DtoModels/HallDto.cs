using System;

namespace Biob.Services.Data.DtoModels
{
    public class HallDto
    {
        public Guid Id { get; set; }
        public int HallNo { get; set; }
        public int NoOfSeats { get; set; }
        public bool ThreeDee { get; set; }
    }
}
