using System;
using System.ComponentModel.DataAnnotations;

namespace Biob.Services.Data.DtoModels.SeatDtos
{
    public class SeatToCreateDto
    {
        [Required]
        public int? RowNo { get; set; }
        [Required]
        public int? SeatNo { get; set; }
    }
}
