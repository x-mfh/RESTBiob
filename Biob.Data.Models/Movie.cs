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
        
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(5000)]
        public string Description { get; set; }
        [Range(0, 36000)]
        public int LengthInSeconds { get; set; }
        public string Poster { get; set; }
        [MaxLength(255)]
        public string Producer { get; set; }
        public string Actors { get; set; }
        public DateTimeOffset Released { get; set; }
        public bool  ThreeDee { get; set; }
        [Range(0, 18)]
        public int AgeRestriction { get; set; }
        public IList<MovieGenre> MovieGenres { get; set; }
    }
}
