using DataLibrary.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models;

public class UserEntity
{
    public UserEntity()
    {
    }

    public UserEntity(string id, CountryEntity country, LanguageEntity language, SpeciesEntity species, bool termsAndConditions, DateTime createdAt, string deviceManufacturer, string deviceModel, string deviceName, string deviceVersionString, string devicePlatform, string deviceIdiom, string deviceType)
    {
        _id = id;
        _countryId = country.Id.ToString();
        _languageId = language.Id.ToString();
        _speciesId = species.Id.ToString();
        _termsAndConditions = termsAndConditions;
        _createdAt = createdAt;
        _deviceManufacturer = deviceManufacturer;
        _deviceModel = deviceModel;
        _deviceName = deviceName;
        _deviceVersionString = deviceVersionString;
        _devicePlatform = devicePlatform;
        _deviceIdiom = deviceIdiom;
        _deviceType = deviceType;
    }

    public void Set(CountryEntity country, LanguageEntity language, SpeciesEntity species, bool termsAndConditions, DateTime createdAt, string deviceManufacturer, string deviceModel, string deviceName, string deviceVersionString, string devicePlatform, string deviceIdiom, string deviceType)
    {
        _countryId = country.Id.ToString();
        _languageId = language.Id.ToString();
        _speciesId = species.Id.ToString();
        _termsAndConditions = termsAndConditions;
        _createdAt = createdAt;
        _deviceManufacturer = deviceManufacturer;
        _deviceModel = deviceModel;
        _deviceName = deviceName;
        _deviceVersionString = deviceVersionString;
        _devicePlatform = devicePlatform;
        _deviceIdiom = deviceIdiom;
        _deviceType = deviceType;
    }

    private string _id;
    public string Id => _id; // Primary key

    private string _countryId;
    public string CountryId => _countryId; // Reference to Ref_Country.Id

    private string _languageId;
    public string LanguageId => _languageId; // Reference to Ref_Language.Id

    private string _speciesId;
    public string SpeciesId => _speciesId; // Reference to Ref_Species.Id

    private bool _termsAndConditions;
    public bool TermsAndConditions => _termsAndConditions; // NOT NULL

    private DateTime _createdAt;
    public DateTime CreatedAt => _createdAt; // NOT NULL

    // New properties for device details
    private string _deviceManufacturer;

    public string DeviceManufacturer => _deviceManufacturer;

    private string _deviceModel;
    public string DeviceModel => _deviceModel;

    private string _deviceName;
    public string DeviceName => _deviceName;

    private string _deviceVersionString;
    public string DeviceVersionString => _deviceVersionString;

    private string _devicePlatform;
    public string DevicePlatform => _devicePlatform;

    private string _deviceIdiom;
    public string DeviceIdiom => _deviceIdiom;

    private string _deviceType;
    public string DeviceType => _deviceType;

    public LanguageEntity LanguageEntity { get; set; }
    public CountryEntity CountryEntity { get; set; }
    public SpeciesEntity SpeciesEntity { get; set; }
}