using System;

namespace Biob.Services.Data.DtoModels
{
    public class SeatToUpdateDto
    {
        public Guid Id { get; set; }
        public int RowNo { get; set; }
        public int SeatNo { get; set; }
    }
}
