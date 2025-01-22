namespace DataLibrary.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    public string CountryId { get; set; }
    public string LanguageId { get; set; }
    public string SpeciesId { get; set; }
    public bool TermsAndConditions { get; set; }
    public DateTime CreatedAt { get; set; }
    public string DeviceManufacturer { get; set; }
    public string DeviceModel { get; set; }
    public string DeviceName { get; set; }
    public string DeviceVersionString { get; set; }
    public string DevicePlatform { get; set; }
    public string DeviceIdiom { get; set; }
    public string DeviceType { get; set; }
}