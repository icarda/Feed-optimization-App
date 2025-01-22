using DataLibrary.Models.Enums;
using DataLibrary.Models;

namespace DataLibrary.DTOs;

public static class Mappers
{
    public static UserEntity MapToUserEntity(UserDTO userDto)
    {
        if (userDto == null)
        {
            throw new ArgumentNullException(nameof(userDto), "UserDTO cannot be null.");
        }

        return new UserEntity(
            id: userDto.Id,
            countryId: userDto.CountryId,
            languageId: userDto.LanguageId,
            speciesId: userDto.SpeciesId,
            termsAndConditions: userDto.TermsAndConditions,
            createdAt: userDto.CreatedAt,
            deviceManufacturer: userDto.DeviceManufacturer,
            deviceModel: userDto.DeviceModel,
            deviceName: userDto.DeviceName,
            deviceVersionString: userDto.DeviceVersionString,
            devicePlatform: userDto.DevicePlatform,
            deviceIdiom: userDto.DeviceIdiom,
            deviceType: userDto.DeviceType
        );
    }

    public static CalculationEntity MapToCalculationEntity(CalculationDTO calculationDto)
    {
        if (calculationDto == null)
        {
            throw new ArgumentNullException(nameof(calculationDto), "CalculationDTO cannot be null.");
        }

        return new CalculationEntity(
            id: calculationDto.Id ?? 0,
            speciesId: calculationDto.SpeciesId ?? 0,
            name: calculationDto.Name ?? string.Empty,
            description: calculationDto.Description ?? string.Empty,
            type: calculationDto.Type ?? string.Empty,
            grazingId: calculationDto.GrazingId ?? 0,
            bodyWeightId: calculationDto.BodyWeightId ?? 0,
            adg: calculationDto.ADG,
            gestation: calculationDto.Gestation ?? false,
            milkYield: calculationDto.MilkYield,
            fatContent: calculationDto.FatContent,
            dietQualityEstimateId: calculationDto.DietQualityEstimateId ?? 0,
            kidsLambsId: calculationDto.KidsLambsId ?? 0
        );
    }

    public static CalculationHasFeedEntity MapToCalculationHasFeedEntity(CalculationHasFeedDTO dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto), "CalculationHasFeedDTO cannot be null.");
        }

        return new CalculationHasFeedEntity(
            calculationId: dto.CalculationId,
            feedId: dto.FeedId,
            dm: dto.DM,
            cpdm: dto.CPDM,
            memjkgdm: dto.MEMJKGDM,
            price: dto.Price,
            intake: dto.Intake,
            minLimit: dto.MinLimit,
            maxLimit: dto.MaxLimit
        );
    }

    public static CalculationHasResultEntity MapToCalculationHasResultEntity(CalculationHasResultDTO dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto), "CalculationHasResultDTO cannot be null.");
        }
        return new CalculationHasResultEntity(
            id: dto.Id,
            calculationId: dto.CalculationId,
            gFresh: dto.GFresh,
            percentFresh: dto.PercentFresh,
            percentDryMatter: dto.PercentDryMatter,
            totalRation: dto.TotalRation
        );
    }

    public static FeedEntity MapToFeedEntity(FeedDTO dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto), "FeedDTO cannot be null.");
        }

        return new FeedEntity(
            id: dto.Id,
            name: dto.Name,
            dryMatterPercentage: dto.DryMatterPercentage,
            memcalKg: dto.MEMcalKg,
            memjKg: dto.MEMJKg,
            tdnPercentage: dto.TDNPercentage,
            cpPercentage: dto.CPPercentage,
            dcpPercentage: dto.DCPPercentage,
            countryId: dto.CountryId,
            languageId: dto.LanguageId
        );
    }
}