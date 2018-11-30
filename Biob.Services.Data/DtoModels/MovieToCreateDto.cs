using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biob.Services.Data.DtoModels
{
    public class MovieToCreateDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Length { get; set; }
        public string Poster { get; set; }
        public string Producer { get; set; }
        public string Actors { get; set; }
        public List<Guid> GenreIds { get; set; }
        public DateTimeOffset Released { get; set; }
        public bool ThreeDee { get; set; }
        public int AgeRestriction { get; set; }
    }
}
