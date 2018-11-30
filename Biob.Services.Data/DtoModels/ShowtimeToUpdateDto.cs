using System;

namespace Biob.Services.Data.DtoModels
{
    public class ShowtimeToUpdateDto
    {
        public Guid MovieId { get; set; }
        public Guid HallId { get; set; }
        public DateTimeOffset TimeOfPlaying { get; set; }
        public bool ThreeDee { get; set; }
    }
}
