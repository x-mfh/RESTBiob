using System;

namespace Biob.Services.Data.DtoModels.ShowtimeDtos
{
    public class ShowtimeToCreateDto
    {
        public Guid HallId { get; set; }
        public DateTimeOffset TimeOfPlaying { get; set; }
        public bool ThreeDee { get; set; }
    }
}
