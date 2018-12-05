using System.ComponentModel.DataAnnotations;

namespace Biob.Services.Data.DtoModels.GenreDtos
{
    public class GenreToUpdateDto
    {
        [StringLength(30, ErrorMessage = "Genre Name must not be more than 30 characters")]
        public string GenreName { get; set; }
    }
}
