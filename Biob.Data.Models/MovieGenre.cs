using Biob.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biob.Data.Models
{
    [Table("MovieGenres")]
    public class MovieGenre : DeleteableModelBase<Guid>
    {
        [ForeignKey("Movie")]
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        [ForeignKey("Genre")]
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
