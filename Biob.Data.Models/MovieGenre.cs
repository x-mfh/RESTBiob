using Biob.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Biob.Data.Models
{
    [Table("MovieGenres")]
    public class MovieGenre : DeleteableModelBase<int>
    {
        [ForeignKey("Movie")]
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        [ForeignKey("Genre")]
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
