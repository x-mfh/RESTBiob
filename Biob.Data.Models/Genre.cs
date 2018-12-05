using Biob.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biob.Data.Models
{
    [Table("Genres")]
    public class Genre : DeleteableModelBase<Guid>
    {
        [Required(ErrorMessage = "Genre must have a name")]
        [MaxLength(30, ErrorMessage = "Genre name must not be longer than 30 characters")]
        public string GenreName { get; set; }
        public IList<MovieGenre> MovieGenres { get; set; }
    }
}
