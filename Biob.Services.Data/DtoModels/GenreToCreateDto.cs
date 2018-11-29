using System;
using System.ComponentModel.DataAnnotations;

namespace Biob.Services.Data.DtoModels
{
    public class GenreToCreateDto
    {
        //public Guid Id { get; set; }
        [Required]
        public string GenreName { get; set; }
    }
}
