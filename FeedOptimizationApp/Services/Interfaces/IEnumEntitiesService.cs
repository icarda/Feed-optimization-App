using DataLibrary.DTOs;
using FeedOptimizationApp.Shared.Wrapper;

namespace FeedOptimizationApp.Services.Interfaces;

public interface IEnumEntitiesService
{
    Task<Result<List<LookupDTO>>> GetLanguagesAsync();

    Task<Result<List<LookupDTO>>> GetCountriesAsync();

    Task<Result<List<LookupDTO>>> GetSpeciesAsync();

    Task<Result<List<LookupDTO>>> GetGrazingsAsync();

    Task<Result<List<LookupDTO>>> GetBodyWeightsAsync();

    Task<Result<List<LookupDTO>>> GetGoatTypesAsync();

    Task<Result<List<LookupDTO>>> GetKidsLambsAsync();

    Task<Result<List<LookupDTO>>> GetSheepTypesAsync();

    Task<Result<List<LookupDTO>>> GetDietQualityEstimatesAsync();
}