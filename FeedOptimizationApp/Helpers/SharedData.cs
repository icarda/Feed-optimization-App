using DataLibrary.Models.Enums;

namespace FeedOptimizationApp.Helpers;

public class SharedData
{
    public SpeciesEntity? SelectedSpecies { get; set; }
    public LanguageEntity? SelectedLanguage { get; set; }
    public CountryEntity? SelectedCountry { get; set; }
}