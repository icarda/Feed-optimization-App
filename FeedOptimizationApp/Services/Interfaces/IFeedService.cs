using DataLibrary.Models;
using FeedOptimizationApp.Shared.Wrapper;

namespace FeedOptimizationApp.Services.Interfaces
{
    /// <summary>
    /// Interface for the FeedService.
    /// Provides methods for managing feed entities.
    /// </summary>
    public interface IFeedService
    {
        /// <summary>
        /// Retrieves all FeedEntity records.
        /// </summary>
        /// <returns>A Result containing a list of FeedEntity records.</returns>
        Task<Result<List<FeedEntity>>> GetAllAsync();

        /// <summary>
        /// Retrieves a FeedEntity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the feed.</param>
        /// <returns>A Result containing the FeedEntity if found.</returns>
        Task<Result<FeedEntity>> GetById(int id);

        /// <summary>
        /// Retrieves a FeedEntity by its name.
        /// </summary>
        /// <param name="name">The name of the feed.</param>
        /// <returns>A Result containing the FeedEntity if found.</returns>
        Task<Result<FeedEntity>> GetByName(string name);

        /// <summary>
        /// Saves a new FeedEntity.
        /// </summary>
        /// <param name="request">The FeedEntity to save.</param>
        /// <returns>A Result containing the generated feed ID.</returns>
        Task<Result<int>> SaveAsync(FeedEntity request);

        /// <summary>
        /// Updates an existing FeedEntity.
        /// </summary>
        /// <param name="request">The FeedEntity with updated information.</param>
        /// <returns>A Result containing the number of records affected.</returns>
        Task<Result<int>> UpdateAsync(FeedEntity request);
    }
}