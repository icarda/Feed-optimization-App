using DataLibrary.Models;
using FeedOptimizationApp.Shared.Wrapper;

namespace FeedOptimizationApp.Services.Interfaces;

public interface IUserService
{
    Task<Result<List<UserEntity>>> GetAllAsync();

    Task<Result<UserEntity>> GetById(int id);

    Task<Result<int>> SaveAsync(UserEntity request);

    Task<Result<int>> UpdateAsync(UserEntity request);
}