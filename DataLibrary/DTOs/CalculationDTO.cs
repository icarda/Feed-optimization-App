namespace DataLibrary.DTOs;

public class CalculationDTO
{
    public int? Id { get; set; }
    public int? SpeciesId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }
    public int? GrazingId { get; set; }
    public int? BodyWeightId { get; set; }
    public decimal? ADG { get; set; }
    public int? DietQualityEstimateId { get; set; }
    public bool? Gestation { get; set; }
    public decimal? MilkYield { get; set; }
    public decimal? FatContent { get; set; }
    public int? KidsLambsId { get; set; }
}