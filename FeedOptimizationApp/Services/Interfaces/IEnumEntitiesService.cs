using DataLibrary.Models;
using DataLibrary.Models.Enums;
using FeedOptimizationApp.Shared.Wrapper;

namespace FeedOptimizationApp.Services.Interfaces;

public interface IEnumEntitiesService
{
    Task<Result<List<LanguageEntity>>> GetLanguagesAsync();

    Task<Result<LanguageEntity>> GetLanguageByIdAsync(int id);

    Task<Result<List<SpeciesEntity>>> GetSpeciesAsync();

    Task<Result<SpeciesEntity>> GetSpeciesByIdAsync(int id);

    Task<Result<List<CountryEntity>>> GetCountriesAsync();

    Task<Result<CountryEntity>> GetCountryByIdAsync(int id);

    Task<Result<List<BodyWeightEntity>>> GetBodyWeightsAsync();

    Task<Result<BodyWeightEntity>> GetBodyWeightByIdAsync(int id);

    Task<Result<List<GrazingEntity>>> GetGrazingsAsync();

    Task<Result<GrazingEntity>> GetGrazingByIdAsync(int id);

    Task<Result<List<DietQualityEstimateEntity>>> GetDietQualityEstimatesAsync();

    Task<Result<DietQualityEstimateEntity>> GetDietQualityEstimateByIdAsync(int id);

    Task<Result<List<SheepTypeEntity>>> GetSheepTypesAsync();

    Task<Result<SheepTypeEntity>> GetSheepTypeByIdAsync(int id);

    Task<Result<List<GoatTypeEntity>>> GetGoatTypesAsync();

    Task<Result<GoatTypeEntity>> GetGoatTypeByIdAsync(int id);

    Task<Result<List<KidsLambsEntity>>> GetKidsLambsAsync();

    Task<Result<KidsLambsEntity>> GetKidsLambsByIdAsync(int id);
}