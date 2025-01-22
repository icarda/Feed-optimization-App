using DataLibrary.Models.Enums;
using DataLibrary.Models;

namespace DataLibrary.DTOs;

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

    public static FeedEntity MapToFeedEntity(FeedDTO feedDto)
    {
        return new FeedEntity(
            feedDto.Id,
            feedDto.Name,
            feedDto.DryMatterPercentage,
            feedDto.MEMcalKg,
            feedDto.MEMJKg,
            feedDto.TDNPercentage,
            feedDto.CPPercentage,
            feedDto.DCPPercentage
        );
    }

    public static CalculationEntity MapToCalculationEntity(CalculationDTO calculationDto)
    {
        var species = SpeciesEntity.From(int.Parse(calculationDto.SpeciesId ?? "0"));
        var sheepType = SheepTypeEntity.From(int.Parse(calculationDto.Type ?? "0"));
        var goatType = GoatTypeEntity.From(int.Parse(calculationDto.Type ?? "0"));
        var grazing = GrazingEntity.From(int.Parse(calculationDto.Grazing ?? "0"));
        var bodyWeight = BodyWeightEntity.From(int.Parse(calculationDto.BodyWeight ?? "0"));
        var dietQualityEstimate = DietQualityEstimateEntity.From(int.Parse(calculationDto.DietQualityEstimate ?? "0"));
        var kidsLambs = KidsLambsEntity.From(int.Parse(calculationDto.KidsLambs ?? "0"));

        return new CalculationEntity(
            calculationDto.Id ?? 0,
            species,
            calculationDto.Name ?? string.Empty,
            calculationDto.Description ?? string.Empty,
            sheepType,
            goatType,
            grazing,
            bodyWeight,
            calculationDto.ADG,
            calculationDto.Gestation ?? false,
            calculationDto.MilkYield,
            calculationDto.FatContent,
            dietQualityEstimate,
            kidsLambs
        );
    }

    public static CalculationHasFeedEntity MapToCalculationHasFeedEntity(CalculationHasFeedDTO calculationHasFeedDto)
    {
        var calculation = new CalculationEntity(int.Parse(calculationHasFeedDto.CalculationId), null, null, null, null, null, null, null, null, false, null, null, null, null);
        var feed = new FeedEntity(int.Parse(calculationHasFeedDto.FeedId), null, 0, 0, 0, 0, 0, 0);

        return new CalculationHasFeedEntity(
            calculation,
            feed,
            calculationHasFeedDto.DM,
            calculationHasFeedDto.CPDM,
            calculationHasFeedDto.MEMJKGDM,
            calculationHasFeedDto.Price,
            calculationHasFeedDto.Intake,
            calculationHasFeedDto.MinLimit,
            calculationHasFeedDto.MaxLimit
        );
    }

    public static CalculationHasResultEntity MapToCalculationHasResultEntity(CalculationHasResultDTO calculationHasResultDto)
    {
        var calculation = new CalculationEntity(int.Parse(calculationHasResultDto.CalculationId), null, null, null, null, null, null, null, null, false, null, null, null, null);

        return new CalculationHasResultEntity(
            calculationHasResultDto.Id,
            calculation,
            calculationHasResultDto.GFresh,
            calculationHasResultDto.PercentFresh,
            calculationHasResultDto.PercentDryMatter,
            calculationHasResultDto.TotalRation
        );
    }
}