using System;
using System.ComponentModel.DataAnnotations;

namespace Biob.Services.Data.DtoModels
{
    public class SeatToCreateDto
    {
        public Guid Id { get; set; }
        [Required]
        public int RowNo { get; set; }
        [Required]
        public int SeatNo { get; set; }
    }
}
