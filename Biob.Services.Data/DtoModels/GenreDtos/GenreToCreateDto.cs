using System.ComponentModel.DataAnnotations;

namespace Biob.Services.Data.DtoModels.GenreDtos
{
    public class GenreToCreateDto
    {
        [Required(ErrorMessage = "Genre Name is required to create a genre")]
        [StringLength(30, ErrorMessage = "Genre Name must not be more than 30 characters")]
        public string GenreName { get; set; }
    }
}
