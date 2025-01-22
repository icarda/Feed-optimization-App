using DataLibrary.Models;
using FeedOptimizationApp.Shared.Wrapper;

namespace FeedOptimizationApp.Services.Interfaces;

public interface IFeedService
{
    Task<Result<List<FeedEntity>>> GetAllAsync();

    Task<Result<FeedEntity>> GetById(int id);

    Task<Result<FeedEntity>> GetByName(string name);

    Task<Result<int>> SaveAsync(FeedEntity request);

    Task<Result<int>> UpdateAsync(FeedEntity request);
}