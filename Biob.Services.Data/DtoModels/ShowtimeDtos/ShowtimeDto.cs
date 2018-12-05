using System;

namespace Biob.Services.Data.DtoModels.ShowtimeDtos
{
    public class ShowtimeDto
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public Guid HallId { get; set; }
        public DateTimeOffset TimeOfPlaying { get; set; }
        public bool ThreeDee { get; set; }
    }
}
