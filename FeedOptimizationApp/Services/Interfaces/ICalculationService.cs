using DataLibrary.Models;
using FeedOptimizationApp.Shared.Wrapper;

namespace FeedOptimizationApp.Services.Interfaces
{
    /// <summary>
    /// Interface for the CalculationService.
    /// Provides methods for managing calculations, feeds, and calculation results.
    /// </summary>
    public interface ICalculationService
    {
        #region Calculation Methods

        /// <summary>
        /// Retrieves a CalculationEntity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the calculation.</param>
        /// <returns>A Result containing the CalculationEntity if found.</returns>
        Task<Result<CalculationEntity>> GetCalculationById(int id);

        /// <summary>
        /// Saves a new CalculationEntity.
        /// </summary>
        /// <param name="request">The CalculationEntity to save.</param>
        /// <returns>A Result containing the generated calculation ID.</returns>
        Task<Result<int>> SaveCalculationAsync(CalculationEntity request);

        /// <summary>
        /// Updates an existing CalculationEntity.
        /// </summary>
        /// <param name="request">The CalculationEntity with updated information.</param>
        /// <returns>A Result containing the number of records affected.</returns>
        Task<Result<int>> UpdateCalculationAsync(CalculationEntity request);

        #endregion Calculation Methods

        #region Calculation Has Feed Methods

        /// <summary>
        /// Retrieves a CalculationHasFeedEntity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the calculation-feed record.</param>
        /// <returns>A Result containing the CalculationHasFeedEntity if found.</returns>
        Task<Result<CalculationHasFeedEntity>> GetCalculationHasFeedById(int id);

        /// <summary>
        /// Retrieves all CalculationHasFeedEntity records for a given calculation ID.
        /// </summary>
        /// <param name="calculationId">The calculation ID to filter feeds by.</param>
        /// <returns>A Result containing a list of CalculationHasFeedEntity records.</returns>
        Task<Result<List<CalculationHasFeedEntity>>> GetCalculationHasFeedsByCalculationId(int calculationId);

        /// <summary>
        /// Retrieves the number of feed entries associated with a given calculation.
        /// </summary>
        /// <param name="calculationId">The calculation ID for which to count feeds.</param>
        /// <returns>A Result containing the count of feed entries.</returns>
        Task<Result<int>> GetNumberOfFeedsInCalculationHasFeedByCalculationId(int calculationId);

        /// <summary>
        /// Saves a new CalculationHasFeedEntity record.
        /// </summary>
        /// <param name="request">The CalculationHasFeedEntity to save.</param>
        /// <returns>A Result containing the generated record ID.</returns>
        Task<Result<int>> SaveCalculationHasFeedAsync(CalculationHasFeedEntity request);

        /// <summary>
        /// Updates an existing CalculationHasFeedEntity record.
        /// </summary>
        /// <param name="request">The CalculationHasFeedEntity with updated information.</param>
        /// <returns>A Result containing the number of records affected.</returns>
        Task<Result<int>> UpdateCalculationHasFeedAsync(CalculationHasFeedEntity request);

        #endregion Calculation Has Feed Methods

        #region Calculation Has Result Methods

        /// <summary>
        /// Retrieves all CalculationHasResultEntity records.
        /// </summary>
        /// <returns>A Result containing a list of all CalculationHasResultEntity records.</returns>
        Task<Result<List<CalculationHasResultEntity>>> GetAllCalculationHasResults();

        /// <summary>
        /// Retrieves a CalculationHasResultEntity record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the calculation-result record.</param>
        /// <returns>A Result containing the CalculationHasResultEntity if found.</returns>
        Task<Result<CalculationHasResultEntity>> GetCalculationHasResultById(int id);

        /// <summary>
        /// Retrieves all CalculationHasResultEntity records for a given calculation ID.
        /// </summary>
        /// <param name="calculationId">The calculation ID to filter results by.</param>
        /// <returns>A Result containing a list of CalculationHasResultEntity records.</returns>
        Task<Result<List<CalculationHasResultEntity>>> GetCalculationHasResultByCalculationId(int calculationId);

        /// <summary>
        /// Saves a new CalculationHasResultEntity record.
        /// </summary>
        /// <param name="request">The CalculationHasResultEntity to save.</param>
        /// <returns>A Result containing the generated record ID.</returns>
        Task<Result<int>> SaveCalculationHasResultAsync(CalculationHasResultEntity request);

        /// <summary>
        /// Updates an existing CalculationHasResultEntity record.
        /// </summary>
        /// <param name="request">The CalculationHasResultEntity with updated information.</param>
        /// <returns>A Result containing the number of records affected.</returns>
        Task<Result<int>> UpdateCalculationHasResultAsync(CalculationHasResultEntity request);

        #endregion Calculation Has Result Methods
    }
}