using DataLibrary.DTOs;

namespace FeedOptimizationApp.Helpers;

public class SharedData
{
    public LookupDTO? SelectedSpecies { get; set; }
    public LookupDTO? SelectedLanguage { get; set; }
    public LookupDTO? SelectedCountry { get; set; }
}