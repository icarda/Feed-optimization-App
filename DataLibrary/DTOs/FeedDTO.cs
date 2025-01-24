namespace DataLibrary.DTOs;

public class FeedDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal DryMatterPercentage { get; set; }
    public decimal MEMcalKg { get; set; }
    public decimal MEMJKg { get; set; }
    public decimal TDNPercentage { get; set; }
    public decimal CPPercentage { get; set; }
    public decimal DCPPercentage { get; set; }
    public int CountryId { get; set; }
    public int LanguageId { get; set; }
}