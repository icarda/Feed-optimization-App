using FeedOptimizationApp.Services.Interfaces;
using FeedOptimizationApp.Shared.Wrapper;
using DataLibrary;
using DataLibrary.Models.Enums;
using Microsoft.EntityFrameworkCore;
using DataLibrary.Models;

namespace FeedOptimizationApp.Services;

public class EnumEntitiesService : IEnumEntitiesService
{
    private readonly ApplicationDbContext _context;

    public EnumEntitiesService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<LanguageEntity>>> GetLanguagesAsync()
    {
        var languages = await _context.Languages
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();

        if (languages == null)
            throw new Exception("No languages found.");

        return await Result<List<LanguageEntity>>.SuccessAsync(languages);
    }

    public async Task<Result<LanguageEntity>> GetLanguageByIdAsync(int id)
    {
        var language = await _context.Languages
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
        if (language == null)
            throw new Exception($"Unable to return language with id {id}.");
        return await Result<LanguageEntity>.SuccessAsync(language);
    }

    public async Task<Result<List<SpeciesEntity>>> GetSpeciesAsync()
    {
        var species = await _context.SpeciesList
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();
        if (species == null)
            throw new Exception("No species found.");
        return await Result<List<SpeciesEntity>>.SuccessAsync(species);
    }

    public async Task<Result<SpeciesEntity>> GetSpeciesByIdAsync(int id)
    {
        var species = await _context.SpeciesList
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
        if (species == null)
            throw new Exception($"Unable to return species with id {id}.");
        return await Result<SpeciesEntity>.SuccessAsync(species);
    }

    public async Task<Result<List<CountryEntity>>> GetCountriesAsync()
    {
        var countries = await _context.Countries
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();
        if (countries == null)
            throw new Exception("No countries found.");
        return await Result<List<CountryEntity>>.SuccessAsync(countries);
    }

    public async Task<Result<CountryEntity>> GetCountryByIdAsync(int id)
    {
        var country = await _context.Countries
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
        if (country == null)
            throw new Exception($"Unable to return country with id {id}.");
        return await Result<CountryEntity>.SuccessAsync(country);
    }

    // grazing
    public async Task<Result<List<GrazingEntity>>> GetGrazingsAsync()
    {
        var grazing = await _context.Grazings
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();
        if (grazing == null)
            throw new Exception("No grazing found.");
        return await Result<List<GrazingEntity>>.SuccessAsync(grazing);
    }

    // body weight

    public async Task<Result<List<BodyWeightEntity>>> GetBodyWeightsAsync()
    {
        var bodyWeight = await _context.BodyWeights
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();
        if (bodyWeight == null)
            throw new Exception("No body weight found.");
        return await Result<List<BodyWeightEntity>>.SuccessAsync(bodyWeight);
    }

    // diet quality estimate

    public async Task<Result<List<DietQualityEstimateEntity>>> GetDietQualityEstimatesAsync()
    {
        var dietQualityEstimate = await _context.DietQualityEstimates
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();
        if (dietQualityEstimate == null)
            throw new Exception("No diet quality estimate found.");
        return await Result<List<DietQualityEstimateEntity>>.SuccessAsync(dietQualityEstimate);
    }

    // kids lambs
    public async Task<Result<List<KidsLambsEntity>>> GetKidsLambsAsync()
    {
        var kidsLambs = await _context.KidsLambs
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();
        if (kidsLambs == null)
            throw new Exception("No kids lambs found.");
        return await Result<List<KidsLambsEntity>>.SuccessAsync(kidsLambs);
    }

    // sheep type
    public async Task<Result<List<SheepTypeEntity>>> GetSheepTypesAsync()
    {
        var sheepType = await _context.SheepTypes
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();
        if (sheepType == null)
            throw new Exception("No sheep type found.");
        return await Result<List<SheepTypeEntity>>.SuccessAsync(sheepType);
    }

    // goat type
    public async Task<Result<List<GoatTypeEntity>>> GetGoatTypesAsync()
    {
        var goatType = await _context.GoatTypes
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();
        if (goatType == null)
            throw new Exception("No goat type found.");
        return await Result<List<GoatTypeEntity>>.SuccessAsync(goatType);
    }

    public async Task<Result<GrazingEntity>> GetGrazingByIdAsync(int id)
    {
        var grazing = await _context.Grazings
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
        if (grazing == null)
            throw new Exception($"Unable to return grazing with id {id}.");
        return await Result<GrazingEntity>.SuccessAsync(grazing);
    }

    public async Task<Result<BodyWeightEntity>> GetBodyWeightByIdAsync(int id)
    {
        var bodyWeight = await _context.BodyWeights
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
        if (bodyWeight == null)
            throw new Exception($"Unable to return body weight with id {id}.");
        return await Result<BodyWeightEntity>.SuccessAsync(bodyWeight);
    }

    public async Task<Result<DietQualityEstimateEntity>> GetDietQualityEstimateByIdAsync(int id)
    {
        var dietQualityEstimate = await _context.DietQualityEstimates
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
        if (dietQualityEstimate == null)
            throw new Exception($"Unable to return diet quality estimate with id {id}.");
        return await Result<DietQualityEstimateEntity>.SuccessAsync(dietQualityEstimate);
    }

    public async Task<Result<SheepTypeEntity>> GetSheepTypeByIdAsync(int id)
    {
        var sheepType = await _context.SheepTypes
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
        if (sheepType == null)
            throw new Exception($"Unable to return sheep type with id {id}.");
        return await Result<SheepTypeEntity>.SuccessAsync(sheepType);
    }

    public async Task<Result<GoatTypeEntity>> GetGoatTypeByIdAsync(int id)
    {
        var goatType = await _context.GoatTypes
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
        if (goatType == null)
            throw new Exception($"Unable to return goat type with id {id}.");
        return await Result<GoatTypeEntity>.SuccessAsync(goatType);
    }

    public async Task<Result<KidsLambsEntity>> GetKidsLambsByIdAsync(int id)
    {
        var kidsLambs = await _context.KidsLambs
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
        if (kidsLambs == null)
            throw new Exception($"Unable to return kids lambs with id {id}.");
        return await Result<KidsLambsEntity>.SuccessAsync(kidsLambs);
    }
}