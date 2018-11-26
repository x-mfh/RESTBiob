using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Biob.Data.Data;
using Biob.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Biob.Services.Web.PropertyMapping;
using Biob.Services.Data.Helpers;
using Biob.Services.Data.DtoModels;

namespace Biob.Services.Data.Repositories
{
    public class MovieRepository : Repository, IMovieRepository
    {
        private IPropertyMappingService _propertyMappingService;

        public MovieRepository(IPropertyMappingService propertyMappingService ,BiobDataContext context) : base(context)
        {
            _propertyMappingService = propertyMappingService;
            _propertyMappingService.AddPropertyMapping<MovieDto, Movie>(new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "Id", new PropertyMappingValue(new List<string>() { "Id" })},
                { "Title", new PropertyMappingValue(new List<string>() { "Title" })},
                { "Description", new PropertyMappingValue(new List<string>() { "Description" })},
                { "Length", new PropertyMappingValue(new List<string>() { "LengthInSeconds" })},
                { "Poster", new PropertyMappingValue(new List<string>() { "Poster" })},
                { "Producer", new PropertyMappingValue(new List<string>() { "Producer" })},
                { "Actors", new PropertyMappingValue(new List<string>() { "Actors" })},
                { "Genre", new PropertyMappingValue(new List<string>() { "Genre" })},
                { "Released", new PropertyMappingValue(new List<string>() { "Released" })},
                { "ThreeDee", new PropertyMappingValue(new List<string>() { "ThreeDee" })},
                { "AgeRestriction", new PropertyMappingValue(new List<string>() { "AgeRestriction" })},
            });
        }
        public void AddMovie(Movie movieToAdd)
        {
            if (movieToAdd.Id ==  Guid.Empty)
            {
                movieToAdd.Id = Guid.NewGuid();
            }
            _context.Movies.Add(movieToAdd);
        }

        public void DeleteMovie(Movie movieToDelete)
        {
            movieToDelete.IsDeleted = true;
            movieToDelete.DeletedOn = DateTimeOffset.Now;
        }

        public async Task<PagedList<Movie>> GetAllMoviesAsync(string orderBy, string searchQuery, int pageNumber, int pageSize)
        {
            var collectionsBeforePaging = _context.Movies.Where(movie => !movie.IsDeleted).Applysort(orderBy, _propertyMappingService.GetPropertyMapping<MovieDto, Movie>());

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                string searchQueryForWhere = searchQuery.Trim().ToLowerInvariant();
                collectionsBeforePaging = collectionsBeforePaging
                    .Where(movie => movie.Title.ToLowerInvariant().Contains(searchQueryForWhere));
            }


            var listToPage = await collectionsBeforePaging.ToListAsync();
            return  PagedList<Movie>.Create(listToPage, pageNumber, pageSize);
        }

        public async Task<Movie> GetMovieAsync(Guid id)
        {
            var foundMovie = await _context.Movies.Where(movie => movie.Id == id).FirstOrDefaultAsync();
            if (foundMovie.IsDeleted)
            {
                foundMovie = null;
            }
            return foundMovie;
        }

        

        public void UpdateMovie(Movie movieToUpdate)
        {
            //  TODO: consider changing update to attach
            //  if things dont work as expected
            _context.Movies.Update(movieToUpdate);
        }
    }
}
