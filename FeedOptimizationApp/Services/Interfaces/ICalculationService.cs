using DataLibrary.Models;
using FeedOptimizationApp.Shared.Wrapper;

namespace FeedOptimizationApp.Services.Interfaces;

public interface ICalculationService
{
    //calculation
    Task<Result<CalculationEntity>> GetCalculationById(int id);

    Task<Result<int>> SaveCalculationAsync(CalculationEntity request);

    Task<Result<int>> UpdateCalculationAsync(CalculationEntity request);

    //calculation has feed
    Task<Result<CalculationHasFeedEntity>> GetCalculationHasFeedById(int calculationId);

    Task<Result<int>> SaveCalculationHasFeedAsync(CalculationHasFeedEntity request);

    Task<Result<int>> UpdateCalculationHasFeedAsync(CalculationHasFeedEntity request);

    //calculation has result
    Task<Result<CalculationHasResultEntity>> GetCalculationHasResultById(int id);

    Task<Result<int>> SaveCalculationHasResultAsync(CalculationHasResultEntity request);

    Task<Result<int>> UpdateCalculationHasResultAsync(CalculationHasResultEntity request);
}