using DataLibrary.Models.Enums;
using DataLibrary.Models;
using FeedOptimizationApp.Shared.DTOs;

namespace FeedOptimizationApp.Helpers;

public static class Mappers
{
    public static UserEntity MapToUserEntity(UserDTO userDto)
    {
        var country = CountryEntity.From(int.Parse(userDto.CountryId));
        var language = LanguageEntity.From(int.Parse(userDto.LanguageId));
        var species = SpeciesEntity.From(int.Parse(userDto.SpeciesId));

        return new UserEntity(
            userDto.Id,
            country,
            language,
            species,
            userDto.TermsAndConditions,
            userDto.CreatedAt,
            userDto.DeviceManufacturer,
            userDto.DeviceModel,
            userDto.DeviceName,
            userDto.DeviceVersionString,
            userDto.DevicePlatform,
            userDto.DeviceIdiom,
            userDto.DeviceType
        );
    }
}