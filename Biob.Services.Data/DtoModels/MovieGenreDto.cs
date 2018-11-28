using System;
using System.Collections.Generic;
using System.Text;

namespace Biob.Services.Data.DtoModels
{
    public class MovieGenreDto
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public Guid GenreId { get; set; }
        public string GenreName { get; set; }
    }
}
