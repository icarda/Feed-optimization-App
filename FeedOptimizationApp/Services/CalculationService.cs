using DataLibrary;
using DataLibrary.Models;
using FeedOptimizationApp.Services.Interfaces;
using FeedOptimizationApp.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace FeedOptimizationApp.Services;

public class CalculationService : ICalculationService
{
    private readonly ApplicationDbContext _context;

    public CalculationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CalculationHasResultEntity> CalculateResult(CalculationEntity animalInformation, List<CalculationHasFeedEntity> feedInformation)
    {
        decimal _totalcost = 0;
        // Calculate the result
        foreach (var feed in feedInformation)
        {
            var dmig = feed.Intake * feed.DM / 100;
            var cpig = dmig * feed.CPDM / 100;
            var meimjday = dmig * feed.MEMJKGDM / 1000;
            var cost = feed.Intake * feed.Price / 1000;

            _totalcost += cost;
        }

        /*var result = new CalculationHasResultEntity
        {
            Id = Guid.NewGuid(),
            CalculationId = Guid.NewGuid(),
            GFresh = 0,
            PercentFresh = 0,
            PercentDryMatter = 0,
            TotalRation = _totalcost
        };

        return result;*/
        return new CalculationHasResultEntity(Guid.NewGuid().ToString(), animalInformation, 0, 0, 0, _totalcost);
    }

    public async Task<Result<CalculationEntity>> GetCalculationById(string id)
    {
        try
        {
            var calculation = await _context.Calculations
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (calculation == null)
                throw new Exception($"Unable to return calculation with id {id}.");

            return await Result<CalculationEntity>.SuccessAsync(calculation);
        }
        catch (Exception ex)
        {
            return await Result<CalculationEntity>.FailAsync(new List<string> { ex.Message });
        }
    }

    public async Task<Result<int>> SaveCalculationAsync(CalculationEntity request)
    {
        try
        {
            var existingCalculation = await _context.Calculations.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Name.ToLower() == request.Name.ToLower());
            if (existingCalculation != null)
                throw new Exception("Calculation already exists. Please edit existing entry.");
            var calculation = new CalculationEntity(
                Guid.NewGuid().ToString(),
                request.SpeciesEntity,
                request.Name,
                request.Description,
                request.SheepTypeEntity,
                request.GoatTypeEntity,
                request.GrazingEntity,
                request.BodyWeightEntity,
                request.ADG,
                request.Gestation,
                request.MilkYield,
                request.FatContent,
                request.DietQualityEstimateEntity,
                request.KidsLambsEntity
            );
            await _context.Calculations.AddAsync(calculation);
            await _context.SaveChangesAsync();
            return await Result<int>.SuccessAsync(calculation.Id);
        }
        catch (Exception ex)
        {
            return await Result<int>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<int>> UpdateCalculationAsync(CalculationEntity request)
    {
        try
        {
            var calculation = await _context.Calculations.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Name.ToLower() == request.Name.ToLower());
            if (calculation == null)
                throw new Exception("Calculation does not exist.");
            calculation.Set(
                request.SpeciesEntity,
                request.Name,
                request.Description,
                request.SheepTypeEntity,
                request.GoatTypeEntity,
                request.GrazingEntity,
                request.BodyWeightEntity,
                request.ADG,
                request.Gestation,
                request.MilkYield,
                request.FatContent,
                request.DietQualityEstimateEntity,
                request.KidsLambsEntity
            );
            _context.Calculations.Update(calculation);
            await _context.SaveChangesAsync();
            return await Result<int>.SuccessAsync(calculation.Id);
        }
        catch (Exception ex)
        {
            return await Result<int>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<CalculationHasFeedEntity>> GetCalculationHasFeedById(string calculationId)
    {
        try
        {
            var calculationHasFeed = await _context.CalculationHasFeeds
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.CalculationId == calculationId);
            if (calculationHasFeed == null)
                throw new Exception($"Unable to return calculation has feed with calculation Id {calculationId}.");
            return await Result<CalculationHasFeedEntity>.SuccessAsync(calculationHasFeed);
        }
        catch (Exception ex)
        {
            return await Result<CalculationHasFeedEntity>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<int>> SaveCalculationHasFeedAsync(CalculationHasFeedEntity request)
    {
        try
        {
            var existingCalculationHasFeed = await _context.CalculationHasFeeds.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Calculation.Name == request.Calculation.Name);
            if (existingCalculationHasFeed != null)
                throw new Exception("Calculation has feed already exists. Please edit existing entry.");
            var calculationHasFeed = new CalculationHasFeedEntity(
                request.Calculation,
                request.Feed,
                request.DM,
                request.CPDM,
                request.MEMJKGDM,
                request.Price,
                request.Intake,
                request.MinLimit,
                request.MaxLimit
            );
            await _context.CalculationHasFeeds.AddAsync(calculationHasFeed);
            await _context.SaveChangesAsync();
            return await Result<int>.SuccessAsync(calculationHasFeed.CalculationId);
        }
        catch (Exception ex)
        {
            return await Result<int>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<int>> UpdateCalculationHasFeedAsync(CalculationHasFeedEntity request)
    {
        try
        {
            var calculationHasFeed = await _context.CalculationHasFeeds.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Calculation.Name == request.Calculation.Name);
            if (calculationHasFeed == null)
                throw new Exception("Calculation has feed does not exist.");
            calculationHasFeed.Set(
                request.Calculation,
                request.Feed,
                request.DM,
                request.CPDM,
                request.MEMJKGDM,
                request.Price,
                request.Intake,
                request.MinLimit,
                request.MaxLimit
            );
            _context.CalculationHasFeeds.Update(calculationHasFeed);
            await _context.SaveChangesAsync();
            return await Result<int>.SuccessAsync(calculationHasFeed.CalculationId);
        }
        catch (Exception ex)
        {
            return await Result<int>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<CalculationHasResultEntity>> GetCalculationHasResultById(string id)
    {
        try
        {
            var calculationHasResult = await _context.CalculationHasResults
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
            if (calculationHasResult == null)
                throw new Exception($"Unable to return calculation has result with id {id}.");
            return await Result<CalculationHasResultEntity>.SuccessAsync(calculationHasResult);
        }
        catch (Exception ex)
        {
            return await Result<CalculationHasResultEntity>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<int>> SaveCalculationHasResultAsync(CalculationHasResultEntity request)
    {
        try
        {
            var existingCalculationHasResult = await _context.CalculationHasResults.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Calculation.Name.ToLower() == request.Calculation.Name.ToLower());
            if (existingCalculationHasResult != null)
                throw new Exception("Calculation has result already exists. Please edit existing entry.");
            var calculationHasResult = new CalculationHasResultEntity(
                Guid.NewGuid().ToString(),
                request.Calculation,
                request.GFresh,
                request.PercentFresh,
                request.PercentDryMatter,
                request.TotalRation
            );
            await _context.CalculationHasResults.AddAsync(calculationHasResult);
            await _context.SaveChangesAsync();
            return await Result<int>.SuccessAsync(calculationHasResult.Id);
        }
        catch (Exception ex)
        {
            return await Result<int>.FailAsync(ex.Message);
        }
    }

    public async Task<Result<int>> UpdateCalculationHasResultAsync(CalculationHasResultEntity request)
    {
        try
        {
            var calculationHasResult = await _context.CalculationHasResults.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Calculation.Name.ToLower() == request.Calculation.Name.ToLower());
            if (calculationHasResult == null)
                throw new Exception("Calculation has result does not exist.");
            calculationHasResult.Set(
                request.Calculation,
                request.GFresh,
                request.PercentFresh,
                request.PercentDryMatter,
                request.TotalRation
            );
            _context.CalculationHasResults.Update(calculationHasResult);
            await _context.SaveChangesAsync();
            return await Result<int>.SuccessAsync(calculationHasResult.Id);
        }
        catch (Exception ex)
        {
            return await Result<int>.FailAsync(ex.Message);
        }
    }
}