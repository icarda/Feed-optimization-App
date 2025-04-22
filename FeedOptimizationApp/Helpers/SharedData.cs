using DataLibrary.Models.Enums;
using Microsoft.Maui.Storage;
using System.ComponentModel;

namespace FeedOptimizationApp.Helpers
{
    /// <summary>
    /// Class to hold shared data across the application.
    /// </summary>
    public class SharedData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event Action? ClearAnimalInfoRequested;

        public void RequestClearAnimalInfo()
        {
            ClearAnimalInfoRequested?.Invoke();
        }

        /// <summary>
        /// Gets or sets the selected species.
        /// </summary>
        private SpeciesEntity? _selectedSpecies;

        public SpeciesEntity? SelectedSpecies
        {
            get => _selectedSpecies;
            set
            {
                if (_selectedSpecies != value)
                {
                    _selectedSpecies = value;
                    OnPropertyChanged(nameof(SelectedSpecies));
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected language.
        /// </summary>
        private LanguageEntity? _selectedLanguage;

        /// <summary>
        /// Gets or sets the selected language.
        /// </summary>
        public LanguageEntity? SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    OnPropertyChanged(nameof(SelectedLanguage));
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected country.
        /// </summary>
        private CountryEntity? _selectedCountry;

        public CountryEntity? SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                if (_selectedCountry != value)
                {
                    _selectedCountry = value;
                    OnPropertyChanged(nameof(SelectedCountry));
                }
            }
        }

        /// <summary>
        /// Gets or sets the calculation ID.
        /// </summary>
        private int? _calculationId;

        public int? CalculationId
        {
            get => _calculationId;
            set
            {
                if (_calculationId != value)
                {
                    _calculationId = value;
                    OnPropertyChanged(nameof(CalculationId));
                }
            }
        }
    }
}