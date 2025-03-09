using DataLibrary.DTOs;
using DataLibrary.Models.Enums;
using DataLibrary.Seedwork;

namespace FeedOptimizationApp.Helpers
{
    /// <summary>
    /// Helper class for converting entities to DTOs and other types.
    /// </summary>
    public static class ConversionHelpers
    {
        /// <summary>
        /// Converts an Enumeration entity to a LookupDTO.
        /// </summary>
        /// <param name="entity">The Enumeration entity to convert.</param>
        /// <returns>A LookupDTO representing the entity.</returns>
        public static LookupDTO ConvertToLookupDTO(Enumeration entity)
        {
            return new LookupDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        /// <summary>
        /// Converts an integer ID to a LookupDTO.
        /// </summary>
        /// <param name="id">The integer ID to convert.</param>
        /// <returns>A LookupDTO with the specified ID.</returns>
        public static LookupDTO ConvertToLookupDTO(int id)
        {
            return new LookupDTO
            {
                Id = id
            };
        }

        /// <summary>
        /// Converts an integer ID to a CountryEntity.
        /// </summary>
        /// <param name="id">The integer ID to convert.</param>
        /// <returns>A CountryEntity with the specified ID.</returns>
        public static CountryEntity ConvertToCountryEntity(int id)
        {
            return CountryEntity.From(id);
        }

        /// <summary>
        /// Converts an integer ID to a LanguageEntity.
        /// </summary>
        /// <param name="id">The integer ID to convert.</param>
        /// <returns>A LanguageEntity with the specified ID.</returns>
        public static LanguageEntity ConvertToLanguageEntity(int id)
        {
            return LanguageEntity.From(id);
        }

        /// <summary>
        /// Converts an integer ID to a SpeciesEntity.
        /// </summary>
        /// <param name="id">The integer ID to convert.</param>
        /// <returns>A SpeciesEntity with the specified ID.</returns>
        public static SpeciesEntity ConvertToSpeciesEntity(int id)
        {
            return SpeciesEntity.From(id);
        }
    }
}