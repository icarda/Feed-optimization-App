using System;

namespace FeedOptimizationApp.Services.Interfaces
{
    /// <summary>
    /// Interface for the ResetPickerService.
    /// Provides functionality to manage picker reset events and notify subscribers.
    /// </summary>
    public interface IResetPickerService
    {
        /// <summary>
        /// Event triggered to notify subscribers when a picker reset is required.
        /// Subscribers can handle this event to perform specific reset actions, such as clearing UI components.
        /// </summary>
        event Action? OnResetPicker;

        /// <summary>
        /// Triggers the reset event to notify all subscribers.
        /// This method is typically called when a picker needs to be reset across the application.
        /// </summary>
        void ResetPicker();
    }
}