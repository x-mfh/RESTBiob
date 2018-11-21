using System;

namespace Biob.Services.Data.DtoModels
{
    public class ShowtimeToUpdateDto
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public int HallId { get; set; }
        public DateTimeOffset TimeOfPlaying { get; set; }
        public bool ThreeDee { get; set; }
    }
}
