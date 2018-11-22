using System;
using System.Collections.Generic;
using System.Text;

namespace Biob.Services.Data.DtoModels
{
    public class HallToUpdateDto
    {
        public int Id { get; set; }
        public int HallNo { get; set; }
        public int NoOfSeats { get; set; }
        public bool ThreeDee { get; set; }
    }
}
