using System;

namespace Biob.Services.Data.DtoModels.MovieDtos
{
    public class MovieDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Length { get; set; }
        public string Poster { get; set; }
        public string Producer { get; set; }
        public string Actors { get; set; }
        public string Genre { get; set; }
        public string Released { get; set; }
        public bool ThreeDee { get; set; }
        public int AgeRestriction { get; set; }
    }
}
