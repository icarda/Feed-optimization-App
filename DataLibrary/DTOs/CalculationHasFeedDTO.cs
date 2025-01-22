namespace DataLibrary.DTOs;

public class CalculationHasFeedDTO
{
    public int CalculationId { get; set; }
    public int FeedId { get; set; }
    public decimal DM { get; set; }
    public decimal CPDM { get; set; }
    public decimal MEMJKGDM { get; set; }
    public decimal Price { get; set; }
    public decimal Intake { get; set; }
    public decimal MinLimit { get; set; }
    public decimal MaxLimit { get; set; }
}