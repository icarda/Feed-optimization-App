using DataLibrary.Models;
using FeedOptimizationApp.Shared.Wrapper;

namespace FeedOptimizationApp.Services.Interfaces;

public interface ICalculationService
{
    Task<CalculationHasResultEntity> CalculateResult(CalculationEntity animalInformation, List<CalculationHasFeedEntity> feedInformation);

    //calculation
    Task<Result<CalculationEntity>> GetCalculationById(string id);

    Task<Result<int>> SaveCalculationAsync(CalculationEntity request);

    Task<Result<int>> UpdateCalculationAsync(CalculationEntity request);

    //calculation has feed
    Task<Result<CalculationHasFeedEntity>> GetCalculationHasFeedById(string calculationId);

    Task<Result<int>> SaveCalculationHasFeedAsync(CalculationHasFeedEntity request);

    Task<Result<int>> UpdateCalculationHasFeedAsync(CalculationHasFeedEntity request);

    //calculation has result
    Task<Result<CalculationHasResultEntity>> GetCalculationHasResultById(string id);

    Task<Result<int>> SaveCalculationHasResultAsync(CalculationHasResultEntity request);

    Task<Result<int>> UpdateCalculationHasResultAsync(CalculationHasResultEntity request);
}