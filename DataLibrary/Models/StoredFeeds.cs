using System.ComponentModel;

namespace DataLibrary.Models
{
    /// <summary>
    /// Represents a stored feed with its associated properties.
    /// </summary>
    public class StoredFeed : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the feed entity.
        /// </summary>
        public FeedEntity? Feed { get; set; }

        /// <summary>
        /// Gets or sets the Calculation ID.
        /// </summary>
        public int? CalculationId { get; set; }

        /// <summary>
        /// Gets or sets the dry matter (DM) value.
        /// </summary>
        public decimal? DM { get; set; }

        /// <summary>
        /// Gets or sets the crude protein on a DM basis (CPDM).
        /// </summary>
        public decimal? CPDM { get; set; }

        /// <summary>
        /// Gets or sets the metabolizable energy (MEMJKGDM).
        /// </summary>
        public decimal? MEMJKGDM { get; set; }

        /// <summary>
        /// Gets or sets the price of the feed.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the minimum limit for the feed.
        /// </summary>
        public decimal? MinLimit { get; set; }

        /// <summary>
        /// Gets or sets the maximum limit for the feed.
        /// </summary>
        public decimal? MaxLimit { get; set; }

        public decimal CPi { get; set; }
        public decimal MEi { get; set; }
        public decimal Cost { get; set; }

        private decimal _intake;

        public decimal Intake
        {
            get => _intake;
            set
            {
                if (_intake != value)
                {
                    _intake = value;
                    OnPropertyChanged(nameof(Intake));
                }
            }
        }

        private decimal _dmi;

        public decimal DMi
        {
            get => _dmi;
            set
            {
                if (_dmi != value)
                {
                    _dmi = value;
                    OnPropertyChanged(nameof(DMi));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}