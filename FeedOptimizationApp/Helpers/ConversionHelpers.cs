using DataLibrary.DTOs;
using DataLibrary.Models.Enums;
using DataLibrary.Seedwork;

namespace FeedOptimizationApp.Helpers;

public static class ConversionHelpers
{
    public static LookupDTO ConvertToLookupDTO(Enumeration entity)
    {
        return new LookupDTO
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public static LookupDTO ConvertToLookupDTO(int id)
    {
        return new LookupDTO
        {
            Id = id
        };
    }

    public static CountryEntity ConvertToCountryEntity(int id)
    {
        return CountryEntity.From(id);
    }

    public static LanguageEntity ConvertToLanguageEntity(int id)
    {
        return LanguageEntity.From(id);
    }

    public static SpeciesEntity ConvertToSpeciesEntity(int id)
    {
        return SpeciesEntity.From(id);
    }
}