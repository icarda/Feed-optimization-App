using DataLibrary.DTOs;
using DataLibrary.Models;
using DataLibrary.Models.Enums;
using FeedOptimizationApp.Services.Interfaces;
using FeedOptimizationApp.Shared.Wrapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedOptimizationApp.Services;

public class EnumEntitiesService : IEnumEntitiesService
{
    public async Task<Result<List<LookupDTO>>> GetLanguagesAsync()
    {
        try
        {
            var languages = await Task.Run(() =>
            {
                return LanguageEntity.List()
                    .Select(e => new LookupDTO { Id = e.Id.ToString(), Name = e.Name })
                    .ToList();
            });
            return Result<List<LookupDTO>>.Success(languages);
        }
        catch (Exception ex)
        {
            return Result<List<LookupDTO>>.Fail(ex.Message);
        }
    }

    public async Task<Result<List<LookupDTO>>> GetCountriesAsync()
    {
        try
        {
            var countries = await Task.Run(() =>
            {
                return CountryEntity.List()
                    .Select(e => new LookupDTO { Id = e.Id.ToString(), Name = e.Name })
                    .ToList();
            });
            return Result<List<LookupDTO>>.Success(countries);
        }
        catch (Exception ex)
        {
            return Result<List<LookupDTO>>.Fail(ex.Message);
        }
    }

    public async Task<Result<List<LookupDTO>>> GetSpeciesAsync()
    {
        try
        {
            var species = await Task.Run(() =>
            {
                return SpeciesEntity.List()
                    .Select(e => new LookupDTO { Id = e.Id.ToString(), Name = e.Name })
                    .ToList();
            });
            return Result<List<LookupDTO>>.Success(species);
        }
        catch (Exception ex)
        {
            return Result<List<LookupDTO>>.Fail(ex.Message);
        }
    }

    public async Task<Result<List<LookupDTO>>> GetGrazingsAsync()
    {
        try
        {
            var grazing = await Task.Run(() =>
            {
                return GrazingEntity.List()
                    .Select(e => new LookupDTO { Id = e.Id.ToString(), Name = e.Name })
                    .ToList();
            });
            return Result<List<LookupDTO>>.Success(grazing);
        }
        catch (Exception ex)
        {
            return Result<List<LookupDTO>>.Fail(ex.Message);
        }
    }

    public async Task<Result<List<LookupDTO>>> GetBodyWeightsAsync()
    {
        try
        {
            var bodyWeights = await Task.Run(() =>
            {
                return BodyWeightEntity.List()
                    .Select(e => new LookupDTO { Id = e.Id.ToString(), Name = e.Name })
                    .ToList();
            });
            return Result<List<LookupDTO>>.Success(bodyWeights);
        }
        catch (Exception ex)
        {
            return Result<List<LookupDTO>>.Fail(ex.Message);
        }
    }

    public async Task<Result<List<LookupDTO>>> GetGoatTypesAsync()
    {
        try
        {
            var goatTypes = await Task.Run(() =>
            {
                return GoatTypeEntity.List()
                    .Select(e => new LookupDTO { Id = e.Id.ToString(), Name = e.Name })
                    .ToList();
            });
            return Result<List<LookupDTO>>.Success(goatTypes);
        }
        catch (Exception ex)
        {
            return Result<List<LookupDTO>>.Fail(ex.Message);
        }
    }

    public async Task<Result<List<LookupDTO>>> GetKidsLambsAsync()
    {
        try
        {
            var kidsLambs = await Task.Run(() =>
            {
                return KidsLambsEntity.List()
                    .Select(e => new LookupDTO { Id = e.Id.ToString(), Name = e.Name })
                    .ToList();
            });
            return Result<List<LookupDTO>>.Success(kidsLambs);
        }
        catch (Exception ex)
        {
            return Result<List<LookupDTO>>.Fail(ex.Message);
        }
    }

    public async Task<Result<List<LookupDTO>>> GetSheepTypesAsync()
    {
        try
        {
            var sheepTypes = await Task.Run(() =>
            {
                return SheepTypeEntity.List()
                    .Select(e => new LookupDTO { Id = e.Id.ToString(), Name = e.Name })
                    .ToList();
            });
            return Result<List<LookupDTO>>.Success(sheepTypes);
        }
        catch (Exception ex)
        {
            return Result<List<LookupDTO>>.Fail(ex.Message);
        }
    }

    public async Task<Result<List<LookupDTO>>> GetDietQualityEstimatesAsync()
    {
        try
        {
            var dietQualityEstimates = await Task.Run(() =>
            {
                return DietQualityEstimateEntity.List()
                    .Select(e => new LookupDTO { Id = e.Id.ToString(), Name = e.Name })
                    .ToList();
            });
            return Result<List<LookupDTO>>.Success(dietQualityEstimates);
        }
        catch (Exception ex)
        {
            return Result<List<LookupDTO>>.Fail(ex.Message);
        }
    }
}