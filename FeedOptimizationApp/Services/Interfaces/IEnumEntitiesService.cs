using DataLibrary.Models;
using DataLibrary.Models.Enums;
using FeedOptimizationApp.Shared.Wrapper;

namespace FeedOptimizationApp.Services.Interfaces
{
    /// <summary>
    /// Interface for the EnumEntitiesService.
    /// Provides methods for managing various enumeration entities.
    /// </summary>
    public interface IEnumEntitiesService
    {
        /// <summary>
        /// Retrieves all LanguageEntity records.
        /// </summary>
        /// <returns>A Result containing a list of LanguageEntity records.</returns>
        Task<Result<List<LanguageEntity>>> GetLanguagesAsync();

        /// <summary>
        /// Retrieves a LanguageEntity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the language.</param>
        /// <returns>A Result containing the LanguageEntity if found.</returns>
        Task<Result<LanguageEntity>> GetLanguageByIdAsync(int id);

        /// <summary>
        /// Retrieves all SpeciesEntity records.
        /// </summary>
        /// <returns>A Result containing a list of SpeciesEntity records.</returns>
        Task<Result<List<SpeciesEntity>>> GetSpeciesAsync();

        /// <summary>
        /// Retrieves a SpeciesEntity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the species.</param>
        /// <returns>A Result containing the SpeciesEntity if found.</returns>
        Task<Result<SpeciesEntity>> GetSpeciesByIdAsync(int id);

        /// <summary>
        /// Retrieves all CountryEntity records.
        /// </summary>
        /// <returns>A Result containing a list of CountryEntity records.</returns>
        Task<Result<List<CountryEntity>>> GetCountriesAsync();

        /// <summary>
        /// Retrieves a CountryEntity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the country.</param>
        /// <returns>A Result containing the CountryEntity if found.</returns>
        Task<Result<CountryEntity>> GetCountryByIdAsync(int id);

        /// <summary>
        /// Retrieves all BodyWeightEntity records.
        /// </summary>
        /// <returns>A Result containing a list of BodyWeightEntity records.</returns>
        Task<Result<List<BodyWeightEntity>>> GetBodyWeightsAsync();

        /// <summary>
        /// Retrieves a BodyWeightEntity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the body weight.</param>
        /// <returns>A Result containing the BodyWeightEntity if found.</returns>
        Task<Result<BodyWeightEntity>> GetBodyWeightByIdAsync(int id);

        /// <summary>
        /// Retrieves all GrazingEntity records.
        /// </summary>
        /// <returns>A Result containing a list of GrazingEntity records.</returns>
        Task<Result<List<GrazingEntity>>> GetGrazingsAsync();

        /// <summary>
        /// Retrieves a GrazingEntity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the grazing type.</param>
        /// <returns>A Result containing the GrazingEntity if found.</returns>
        Task<Result<GrazingEntity>> GetGrazingByIdAsync(int id);

        /// <summary>
        /// Retrieves all DietQualityEstimateEntity records.
        /// </summary>
        /// <returns>A Result containing a list of DietQualityEstimateEntity records.</returns>
        Task<Result<List<DietQualityEstimateEntity>>> GetDietQualityEstimatesAsync();

        /// <summary>
        /// Retrieves a DietQualityEstimateEntity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the diet quality estimate.</param>
        /// <returns>A Result containing the DietQualityEstimateEntity if found.</returns>
        Task<Result<DietQualityEstimateEntity>> GetDietQualityEstimateByIdAsync(int id);

        /// <summary>
        /// Retrieves all SheepTypeEntity records.
        /// </summary>
        /// <returns>A Result containing a list of SheepTypeEntity records.</returns>
        Task<Result<List<SheepTypeEntity>>> GetSheepTypesAsync();

        /// <summary>
        /// Retrieves a SheepTypeEntity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the sheep type.</param>
        /// <returns>A Result containing the SheepTypeEntity if found.</returns>
        Task<Result<SheepTypeEntity>> GetSheepTypeByIdAsync(int id);

        /// <summary>
        /// Retrieves all GoatTypeEntity records.
        /// </summary>
        /// <returns>A Result containing a list of GoatTypeEntity records.</returns>
        Task<Result<List<GoatTypeEntity>>> GetGoatTypesAsync();

        /// <summary>
        /// Retrieves a GoatTypeEntity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the goat type.</param>
        /// <returns>A Result containing the GoatTypeEntity if found.</returns>
        Task<Result<GoatTypeEntity>> GetGoatTypeByIdAsync(int id);

        /// <summary>
        /// Retrieves all KidsLambsEntity records.
        /// </summary>
        /// <returns>A Result containing a list of KidsLambsEntity records.</returns>
        Task<Result<List<KidsLambsEntity>>> GetKidsLambsAsync();

        /// <summary>
        /// Retrieves a KidsLambsEntity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the kids/lambs entity.</param>
        /// <returns>A Result containing the KidsLambsEntity if found.</returns>
        Task<Result<KidsLambsEntity>> GetKidsLambsByIdAsync(int id);
    }
}