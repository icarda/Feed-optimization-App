using FeedOptimizationApp.Services.Interfaces;
using System;

namespace FeedOptimizationApp.Services
{
    /// <summary>
    /// Service responsible for managing picker reset events.
    /// This service provides a mechanism to notify subscribers when a picker reset is required.
    /// </summary>
    public class ResetPickerService : IResetPickerService
    {
        /// <summary>
        /// Event triggered to notify subscribers that a picker reset is required.
        /// Subscribers can handle this event to perform specific reset actions.
        /// </summary>
        public event Action? OnResetPicker;

        /// <summary>
        /// Triggers the reset event to notify all subscribers.
        /// This method is typically called when a picker needs to be reset across the application.
        /// </summary>
        public void ResetPicker()
        {
            // Invoke the event to notify all subscribers.
            OnResetPicker?.Invoke();

            // Log a message to indicate that the reset event has been triggered.
            Console.WriteLine("ResetPicker event triggered.");
        }
    }
}