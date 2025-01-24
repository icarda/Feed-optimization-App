using DataLibrary.Models.Enums;

namespace DataLibrary.Models;

public class UserEntity
{
    public UserEntity()
    {
    }

    public UserEntity(int countryId, int languageId, int speciesId, bool termsAndConditions, DateTime createdAt, string deviceManufacturer, string deviceModel, string deviceName, string deviceVersionString, string devicePlatform, string deviceIdiom, string deviceType)
    {
        CountryId = countryId;
        LanguageId = languageId;
        SpeciesId = speciesId;
        TermsAndConditions = termsAndConditions;
        CreatedAt = createdAt;
        DeviceManufacturer = deviceManufacturer;
        DeviceModel = deviceModel;
        DeviceName = deviceName;
        DeviceVersionString = deviceVersionString;
        DevicePlatform = devicePlatform;
        DeviceIdiom = deviceIdiom;
        DeviceType = deviceType;
    }

    public void Set(int countryId, int languageId, int speciesId, bool termsAndConditions, DateTime createdAt, string deviceManufacturer, string deviceModel, string deviceName, string deviceVersionString, string devicePlatform, string deviceIdiom, string deviceType)
    {
        CountryId = countryId;
        LanguageId = languageId;
        SpeciesId = speciesId;
        TermsAndConditions = termsAndConditions;
        CreatedAt = createdAt;
        DeviceManufacturer = deviceManufacturer;
        DeviceModel = deviceModel;
        DeviceName = deviceName;
        DeviceVersionString = deviceVersionString;
        DevicePlatform = devicePlatform;
        DeviceIdiom = deviceIdiom;
        DeviceType = deviceType;
    }

    public int Id { get; private set; } // Primary key

    public int CountryId { get; private set; } // Reference to Ref_Country.Id

    public int LanguageId { get; private set; } // Reference to Ref_Language.Id

    public int SpeciesId { get; private set; } // Reference to Ref_Species.Id

    public bool TermsAndConditions { get; private set; } // NOT NULL

    public DateTime CreatedAt { get; private set; } // NOT NULL

    // New properties for device details
    public string DeviceManufacturer { get; private set; }

    public string DeviceModel { get; private set; }

    public string DeviceName { get; private set; }

    public string DeviceVersionString { get; private set; }

    public string DevicePlatform { get; private set; }

    public string DeviceIdiom { get; private set; }

    public string DeviceType { get; private set; }

    public LanguageEntity LanguageEntity { get; set; }
    public CountryEntity CountryEntity { get; set; }
    public SpeciesEntity SpeciesEntity { get; set; }
}