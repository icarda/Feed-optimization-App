using DataLibrary.Models;
using FeedOptimizationApp.Shared.Wrapper;

namespace FeedOptimizationApp.Services.Interfaces;

public interface IFeedService
{
    Task<Result<FeedEntity>> GetById(string id);

    Task<Result<FeedEntity>> GetByName(string name);

    Task<Result<int>> SaveAsync(FeedEntity request);

    Task<Result<int>> UpdateAsync(FeedEntity request);
}