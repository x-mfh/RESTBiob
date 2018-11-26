using System;

namespace Biob.Services.Data.DtoModels
{
    public class MovieGenreToUpdateDto
    {
        public Guid GenreId { get; set; }
        public string GenreName { get; set; }
    }
}
