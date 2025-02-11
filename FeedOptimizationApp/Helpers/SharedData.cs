using DataLibrary.Models.Enums;

namespace FeedOptimizationApp.Helpers
{
    /// <summary>
    /// Class to hold shared data across the application.
    /// </summary>
    public class SharedData
    {
        /// <summary>
        /// Gets or sets the selected species.
        /// </summary>
        public SpeciesEntity? SelectedSpecies { get; set; }

        /// <summary>
        /// Gets or sets the selected language.
        /// </summary>
        public LanguageEntity? SelectedLanguage { get; set; }

        /// <summary>
        /// Gets or sets the selected country.
        /// </summary>
        public CountryEntity? SelectedCountry { get; set; }

        /// <summary>
        /// Gets or sets the calculation ID.
        /// </summary>
        public int? CalculationId { get; set; }
    }
}