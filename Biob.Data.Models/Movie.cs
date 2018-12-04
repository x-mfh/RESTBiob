using Biob.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biob.Data.Models
{
    [Table("Movies")]
    public class Movie : DeleteableModelBase<Guid>
    {
        public Movie()
        {
            Showtimes = new List<Showtime>();
        }
        [Required(ErrorMessage = "Movie must have a title")]
        [MaxLength(100, ErrorMessage = "Movie title must not be longer than 100 characters")]
        public string Title { get; set; }
        [MaxLength(5000, ErrorMessage = "Movie Description must not be longer than 5000 characters")]
        public string Description { get; set; }
        [Range(0, 36000, ErrorMessage = "Movie length in seconds must not be longer than 36000")]
        public int LengthInSeconds { get; set; }
        public string Poster { get; set; }
        [MaxLength(100, ErrorMessage = "Movie producer must not be longer than 100 characters")]
        public string Producer { get; set; }
        public ICollection<Showtime> Showtimes { get; set; }
        [MaxLength(255, ErrorMessage = "Movie actors must not be longer than 255 characters")]
        public string Actors { get; set; }
        public DateTimeOffset Released { get; set; }
        public bool  ThreeDee { get; set; }
        [Range(1, 18, ErrorMessage = "Movie age restriction must be between 0 and 18")]
        public int AgeRestriction { get; set; }
        public IList<MovieGenre> MovieGenres { get; set; }
    }
}
