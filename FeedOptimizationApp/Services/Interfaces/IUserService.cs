using DataLibrary.Models;
using FeedOptimizationApp.Shared.Wrapper;

namespace FeedOptimizationApp.Services.Interfaces
{
    /// <summary>
    /// Interface for the UserService.
    /// Provides methods for managing user entities.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieves all UserEntity records.
        /// </summary>
        /// <returns>A Result containing a list of UserEntity records.</returns>
        Task<Result<List<UserEntity>>> GetAllAsync();

        /// <summary>
        /// Retrieves a UserEntity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>A Result containing the UserEntity if found.</returns>
        Task<Result<UserEntity>> GetById(int id);

        /// <summary>
        /// Saves a new UserEntity.
        /// </summary>
        /// <param name="request">The UserEntity to save.</param>
        /// <returns>A Result containing the generated user ID.</returns>
        Task<Result<int>> SaveAsync(UserEntity request);

        /// <summary>
        /// Updates an existing UserEntity.
        /// </summary>
        /// <param name="request">The UserEntity with updated information.</param>
        /// <returns>A Result containing the number of records affected.</returns>
        Task<Result<int>> UpdateAsync(UserEntity request);
    }
}