using System;
using System.ComponentModel.DataAnnotations;

namespace Biob.Services.Data.DtoModels.ShowtimeDtos
{
    public class ShowtimeToCreateDto
    {
        [Required( ErrorMessage = "Hall Id is required to create a showtime")]
        public Guid HallId { get; set; }
        public DateTimeOffset TimeOfPlaying { get; set; }
        public bool ThreeDee { get; set; }
    }
}
